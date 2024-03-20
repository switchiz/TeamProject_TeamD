using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowEnemy : EnemyBase_
{ 
    /// <summary>
    /// 공격 거리
    /// </summary>
    public float attackDistance = 3f;

    /// <summary>
    /// 레이의 길이
    /// </summary>
    float rayLength = 3.0f;


    float attackCooldown = 2f;
    float currentCooldown = 0f;

    readonly int canAttack_Hash = Animator.StringToHash("canAttack");
    readonly int isMove_Hash = Animator.StringToHash("isMove");
    readonly int Die_Hash = Animator.StringToHash("Die");
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        hp = maxHp;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        checkNow();
    }

    private void OnDrawGizmos()
    {
        if (rb != null)
        {
            // Ray의 발사 방향과 길이를 시각화
            Gizmos.color = Color.red; // Ray의 색상을 빨간색으로 설정
            Vector2 direction = new Vector2(-CheckLR, 0); // Ray를 발사할 방향 설정
            Gizmos.DrawRay(rb.position, direction * rayLength); // DrawRay를 사용하여 Ray를 그림
        }
    }

    protected override void attackAction()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.fixedDeltaTime;
            return;
        }

        animator.SetBool(canAttack_Hash, true);

        currentCooldown = attackCooldown;
    }

    protected override void idleAction()
    {
        animator.SetBool(canAttack_Hash, false);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerCheck = true;
        }
    }

    protected override void checkNow()
    {
        if(playerCheck)
        {
            animator.SetBool(isMove_Hash, true);
            float distanceToPlayer = Vector2.Distance(transform.position, targetPos);

            if (distanceToPlayer > attackDistance)
            {
                float step = mobMoveSpeed * Time.deltaTime;
                Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
                Vector2 targetPosition = new Vector2(targetPos.x, transform.position.y);
                Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition, step);
                transform.position = newPosition;
            }
                
            Vector2 direction = new Vector2(-CheckLR, 0);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayLength, LayerMask.GetMask("Wall"));

            if (hit.collider != null)
            {
                Jump();
            }
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}
