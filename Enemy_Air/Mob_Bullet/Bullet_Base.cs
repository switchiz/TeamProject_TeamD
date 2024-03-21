using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Base : MonoBehaviour
{
    /// <summary>
    /// �÷��̾� �ҷ����� 
    /// </summary>
    protected Player player;

    /// <summary>
    /// �÷��̾� ��ġ Ÿ����
    /// </summary>
    Vector2 targetPos;

    /// <summary>
    /// ź�� �̵��ӵ�
    /// </summary>
    public float mobMoveSpeed = 0;

    public uint bulletDamage = 1;

    /// <summary>
    /// ź�� ���� �Ѵ��� ���Ѵ��� ����
    /// </summary>
    public bool isThrougt;

    /// <summary>
    /// ź�� �ǰ� ���� (�и� , ����)
    /// </summary>
    public bool isParring;

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
