using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy_C : EnemyBase_
{
    // �÷��̾ �þ߰� �ȿ� ������ ����� ������
    private float idle_speed = 2.0f; // �ӵ�
    private float height = 0.8f; // �պ� ����
    private Vector3 startPos; // �ʱ� ��ġ

    Vector3 playerPos;

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
        // �÷��̾� ��ġ
        startPos = player.transform.position;
        startPos.y += 5;

        // �ڱ� ��ġ
        Vector3 currentPos = transform.position;

        // �� ������ ��鸰��.
        float newX = Mathf.Lerp(currentPos.x, startPos.x + Mathf.Sin(Time.time * idle_speed) * height * 5 , mobMoveSpeed * Time.deltaTime);

        // �÷��̾� + 4 ��ǥ�� ����Ѵ�.

        currentPos.y = Mathf.Lerp(currentPos.y, startPos.y , mobMoveSpeed * Time.deltaTime);

        //transform.position = currentPos;
        transform.position = new Vector3(newX, currentPos.y, 0);
    }








}
