using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Base : MonoBehaviour
{
    /// <summary>
    /// �÷��̾� �ҷ����� 
    /// </summary>
    protected Player player;

    Vector3 playerPos;

    /// <summary>
    /// �÷��̾� ��ġ Ÿ����
    /// </summary>
    Vector2 targetPos;

    /// <summary>
    /// �̵�����
    /// </summary>
    Vector2 moveDir;


    /// <summary>
    /// ź�� �̵��ӵ�
    /// </summary>
    public float mobMoveSpeed = 5;

    public uint bulletDamage = 1;

    /// <summary>
    /// ź�� ���� �Ѵ��� ���Ѵ��� ����
    /// </summary>
    public bool isThrougt;

    /// <summary>
    /// ź�� �ǰ� ���� (�и� , ����)
    /// </summary>
    public bool isParring;

    private void Awake()
    {
        player = GameManager.Instance.Player;

        playerPos = player.transform.position;
        moveDir = (playerPos - transform.position).normalized;
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * mobMoveSpeed * moveDir);
    }

    /// <summary>
    /// �浹�� �����ϴ� �޼���
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {

    }

    /// <summary>
    /// ������� ���� �� �޼���
    /// </summary>
    public void Die()
    {
        StopAllCoroutines();
    }
}
