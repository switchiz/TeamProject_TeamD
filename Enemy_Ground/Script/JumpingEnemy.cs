using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemy : EnemyBase_
{
    /// <summary>
    /// 공격 거리
    /// </summary>
    public float attackDistance = 2f;

    /// <summary>
    /// 레이의 길이
    /// </summary>
    float rayLength = 1.0f;

    /// <summary>
    /// 점프 쿨타임
    /// </summary>
    float jumpCool = 2f;

    bool isJumping = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        hp = maxHp;
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(JumpCoroutine());
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        checkNow();
    }

    protected override void attackAction()
    {

    }

    protected override void idleAction()
    {

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
        if (playerCheck)
        {
            float step = mobMoveSpeed * Time.deltaTime;
            Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
            Vector2 targetPosition = new Vector2(targetPos.x, transform.position.y);
            Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition, step);
            transform.position = newPosition;
            Vector2 direction = new Vector2(-CheckLR, 0);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayLength, LayerMask.GetMask("Wall"));

            if (hit.collider != null)
            {
                Jump();
            }
        }
    }

    /// <summary>
    /// 점프 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator JumpCoroutine()
    {
        while (true) // 무한 루프
        {
            if (!isJumping)
            {
                Jump();
            }
            yield return new WaitForSeconds(jumpCool); // 점프 쿨타임
            checkNow();
        }
    }

    /// <summary>
    /// 점프
    /// </summary>
    void Jump()
    {
        isJumping = true;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isJumping = false;
    }
}
