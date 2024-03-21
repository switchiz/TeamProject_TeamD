using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy_A : EnemyBase_
{
    // �÷��̾ �þ߰� �ȿ� ������ ����� ������
    private float idle_speed = 2.0f; // �ӵ�
    private float height = 0.8f; // �պ� ����
    private Vector3 startPos; // �ʱ� ��ġ


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
        // �߽߰� �̵�
        Vector3 playerPos = player.transform.position;
        Vector3 moveDir = (playerPos - transform.position ).normalized;

        transform.Translate(Time.deltaTime * mobMoveSpeed * moveDir);




    }






}
