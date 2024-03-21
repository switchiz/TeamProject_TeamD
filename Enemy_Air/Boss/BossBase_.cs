using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossBase_ : MonoBehaviour
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
    /// ���� ��ü�� �ִ� ü��
    /// </summary>
    public int maxHp = 50;

    /// <summary>
    /// ���� ��ü�� ���� ü��
    /// </summary>
    private int hp = 50;

    /// <summary>
    /// �� ��ü�� ������ ( �ε����� ��츸 )
    /// </summary>
    public uint mobDamage = 0;

    /// <summary>
    /// �׽�Ʈ�� uint
    /// </summary>
    public uint TestPattern = 0;


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
                // ��������Ʈ ���� ��ȯ ( ������ �� �÷��̾ ���� �ʵ��� �Ѵ�. )
                // if (checkLR == 1) sprite.flipX = true; else { sprite.flipX = false; }
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

        // ������ ������ �ڷ�ƾ���� ����Ѵ�.
        AwakeAction();
    }

    protected virtual void FixedUpdate() // �̵�����
    {
        // �÷��̾��� ��ġ�� �޴´�.
        targetPos = player.transform.position;

        // �÷��̾��� ��ġ������ �¿� 
        if (targetPos.x < rb.position.x) CheckLR = 1;
        else CheckLR = -1;
    }

    /// <summary>
    /// �÷��̾, ������ ���������� ó������ �� �ൿ ( �������� or ���� )
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator AwakeAction()
    {
        // 

        yield return new WaitForSeconds(1.0f);
    }

    /// <summary>
    /// ������ ���� ��ųʸ�
    /// </summary>
    protected Dictionary<uint, Func<IEnumerator>> patternActions;

    /// <summary>
    /// ��ųʸ� ���� ���� / 1 = ȣ���� ��ȣ / �� IEnumerator = ��ȣ�� ���� ������ �ڷ�ƾ
    /// </summary>
    protected virtual void InitializePatterns()
    {
        patternActions = new Dictionary<uint, Func<IEnumerator>>()
    {
        { 1, BossPattern_1 },
            {2, BossPattern_2 }

        // �ٸ� ���ϵ鵵 �̿� ���� �ʱ�ȭ
    };
    }

    /// <summary>
    /// ������ �ൿ�� ������ �����ϴ� �ڷ�ƾ, ������ ������ ��ġ�� �� �ڷ�ƾ�� �ٽ� ȣ���Ѵ�.
    /// </summary>
    /// <param name="i">Ư�� ������ �����Ű�� ���� ����</param>
    /// <returns></returns>
    protected virtual IEnumerator bossActionSelect(uint pattern = 0)
    {
        pattern = TestPattern;

        // i�� ���� �־��ٸ�, �ش� ������ �����ϸ�, ���� �ʾҴٸ� ������ ������ �����Ѵ�.
        if (pattern == 0) pattern = (uint)Random.Range(1, patternActions.Count + 1); // ���� ������ ����






        yield return new WaitForSeconds(4.0f); // ������ �����ϱ� ��, ���� �ð� (�ִϸ��̼� ����, ��Ÿ�� �� )


        // ���� ����
        if (patternActions.TryGetValue(pattern, out var action))
        {
            StartCoroutine(action());
        }
    }




    /// <summary>
    /// ���� 1 : ���� ����
    /// ������ �������̵��Ͽ� ������� �ʰ�, �ڽĿ��� ������
    /// </summary>
    /// <returns></returns>
    IEnumerator BossPattern_1()
    {
        // ����
        yield return new WaitForSeconds(1.0f);
        //����
        yield return new WaitForSeconds(1.0f);
        // ����
        StartCoroutine(bossActionSelect());
    }

    /// <summary>
    /// �ӽ�
    /// </summary>
    /// <returns></returns>
    IEnumerator BossPattern_2()
    {
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(bossActionSelect());
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!playerCheck) // �� �߰� ���¿��� 
        {
            if (collision.gameObject.CompareTag("Player")) // �÷��̾ Trigger ���� �ȿ� ���Դٸ� ( �ν��ߴٸ� )
            {
                playerCheck = true;
            }
        }

    }

    /// <summary>
    /// �浹�� �����ϴ� �޼���
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {

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
}
