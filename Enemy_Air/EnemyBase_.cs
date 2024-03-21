using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase_ : MonoBehaviour
{
    //������Ʈ �ҷ�����
    Rigidbody2D rb;
    public SpriteRenderer sprite;

    /// <summary>
    /// �÷��̾� �ҷ�����
    /// </summary>
    protected Player player;

    /// <summary>
    /// �÷��̾� ��ġ Ÿ����
    /// </summary>
    Vector2 targetPos;

    /// <summary>
    /// �� ��ü�� �ִ� ü��
    /// </summary>
    public int maxHp = 10;

    /// <summary>
    /// �� ��ü�� ���� ü��
    /// </summary>
    private int hp = 10;

    /// <summary>
    /// �� ��ü�� ������ ( �ε����� ��츸 )
    /// </summary>
    public int mobDamage = 0;

    /// <summary>
    /// �� ��ü�� �̵��ӵ�
    /// </summary>
    public float mobMoveSpeed = 0;

    /// <summary>
    /// ���� �̵��ϴ��� (������) �� ���� ���� false�� ������ȯ + �̵��� ���� �ʴ´�.
    /// </summary>
    public bool IsMove = true;
    
    /// <summary>
    /// �÷��̾ �߰��ߴ��� ( true�� �߰� )
    /// </summary>
    protected bool playerCheck = false;


    /// <summary>
    /// Hp ������Ƽ
    /// </summary>
    public int Hp
    {
        get { return hp; }
        set
        {
            hp = value;
            hp = Mathf.Max(hp, 0);

            // Hp�� 0 ���ϸ� ���
            if (Hp <= 0)
            {
                Die();
            }

        }
    }

    /// <summary>
    /// �¿� Ȯ��
    /// </summary>
    public int checkLR = 1;

    /// <summary>
    /// �¿� ����� ������Ƽ
    /// </summary>
    public int CheckLR
    {
        get { return checkLR; }
        set
        {
            if (checkLR != value) // ���� ���� �Ǿ��ٸ�
            {
                checkLR = value;
                // ��������Ʈ ���� ��ȯ 
                if (checkLR == 1) sprite.flipX = false; else { sprite.flipX = true; }
            }

        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        hp = maxHp;
    }

    protected virtual void Start()
    {
        player = GameManager.Instance.Player;
    }

    protected virtual void FixedUpdate() // �̵�����
    {
        if (playerCheck) // �÷��̾� �߽߰� �ൿ
        {
            // �÷��̾��� ��ġ�� �޴´�.
            targetPos = player.transform.position;
            // �����̴� ������ ��쿡��
            if (IsMove)
            {
                // �÷��̾��� ��ġ�� ���� CheckLR �� �����Ѵ�.
                if (targetPos.x < rb.position.x) CheckLR = 1;
                else CheckLR = -1;
            }
            attackAction();
  

        }
        else // �÷��̾� �̹߽߰� �ൿ
        {
            idleAction();
        }




    }

    /// <summary>
    /// /// ������Ʈ���� ����� �ڵ� ( �÷��̾� �߰� )
    /// </summary>
    protected virtual void attackAction()
    {

    }

    /// <summary>
    /// ������Ʈ���� ����� �ڵ� ( �÷��̾� �̹߰� )
    /// </summary>
    protected virtual void idleAction()
    {

    }


    /// <summary>
    /// �浹�� �����ϴ� �޼���
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {

    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if ( !playerCheck ) // �� �߰� ���¿��� 
        {
            if (collision.gameObject.CompareTag("Player")) // �÷��̾ Trigger ���� �ȿ� ���Դٸ� ( �ν��ߴٸ� )
            {
                checkNow();
            }
        }

    }

    /// <summary>
    /// �÷��̾� �߰� ��� ������ �Լ�
    /// </summary>
    protected virtual void checkNow()
    {
        playerCheck = true;
    }





    /// <summary>
    /// ���ظ� �޾����� ������ �Լ� ����
    /// </summary>
    /// <param name="Damage">�÷��̾�� ���� ����</param>
    /// <exception cref="NotImplementedException"></exception>
    public void Damaged(int Damage)
    {
        Hp -= Damage;
    }


    /// <summary>
    /// �׾����� ���� �� �޼���
    /// </summary>
    public void Die()
    {
        StopAllCoroutines();
    }

    /// <summary>
    /// ������ ��� �޼��� / �Ϲ������� Die���� ����
    /// </summary>
    public void ItemDrop()
    {

    }
}
