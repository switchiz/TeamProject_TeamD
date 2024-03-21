using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Base : MonoBehaviour
{
    /// <summary>
    /// 플레이어 불러오기 
    /// </summary>
    protected Player player;

    /// <summary>
    /// 플레이어 위치 타게팅
    /// </summary>
    Vector2 targetPos;

    /// <summary>
    /// 탄의 이동속도
    /// </summary>
    public float mobMoveSpeed = 0;

    public uint bulletDamage = 1;

    /// <summary>
    /// 탄이 벽을 넘는지 못넘는지 여부
    /// </summary>
    public bool isThrougt;

    /// <summary>
    /// 탄의 피격 여부 (패링 , 제거)
    /// </summary>
    public bool isParring;

    /// <summary>
    /// 충돌을 검출하는 메서드
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {

    }

    /// <summary>
    /// 사라질때 실행 될 메서드
    /// </summary>
    public void Die()
    {
        StopAllCoroutines();
    }
}
