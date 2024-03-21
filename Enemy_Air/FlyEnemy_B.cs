using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy_B : EnemyBase_
{
    // �÷��̾ �þ߰� �ȿ� ������ ����� ������
    private float idle_speed = 2.0f; // �ӵ�
    private float height = 0.8f; // �պ� ����
    private Vector3 startPos; // �ʱ� ��ġ

    Vector3 playerPos;

    /// <summary>
    /// �̵��� ����
    /// </summary>
    Vector3 moveDir;

    

    protected override void Start()
    {
        base.Start();
        startPos = transform.position; // ���� ��ġ�� ���� ��ġ�� ����
    }

    protected override void idleAction()
    {
        // ���Ʒ��� ��鸰��. ( checkLR ���� x )
        float newY = Mathf.Sin(Time.time * idle_speed) * height;
        transform.position = startPos + new Vector3(0, newY, 0);
    }

    protected override void attackAction()
    {
        if ( IsMove )
        {
            transform.Translate(Time.deltaTime * mobMoveSpeed * moveDir);
        }
    }

    protected override void checkNow()
    {
        base.checkNow();
        StartCoroutine(targetMove());
    }

    IEnumerator targetMove()
    {
        // ��� ��ġ�� �����Ѵ�. �����.
        IsMove = false;
        yield return new WaitForSeconds(1.5f);                  // 1.5�� ���� ����ѵ�
        playerPos = player.transform.position;                  
        moveDir = (playerPos - transform.position).normalized;  
        IsMove = true;                                          // �÷��̾ �ִ� ��ġ�� 1.5�ʰ� �̵��Ѵ�.
        yield return new WaitForSeconds(1.5f);                  
        StartCoroutine(targetMove());                           // ���� �ݺ�
    }






}
