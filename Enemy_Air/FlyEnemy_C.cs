using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy_C : EnemyBase_
{
    // 플레이어가 시야각 안에 없을때 사용할 변수들
    private float idle_speed = 2.0f; // 속도
    private float height = 0.8f; // 왕복 높이
    private Vector3 startPos; // 초기 위치

    /// <summary>
    /// 쏘는 탄환 ( 임시로 public , 추후 팩토리로 찾기 )
    /// </summary>
    public GameObject obj;

    Vector3 playerPos;

    protected override void Start()
    {
        base.Start();
        startPos = transform.position; // 시작 위치를 현재 위치로 설정
    }

    protected override void idleAction()
    {
        // 위아래로 흔들린다. ( checkLR 변동 x )
        float newY = Mathf.Sin(Time.time * idle_speed) * height;
        transform.position = startPos + new Vector3(0, newY, 0);
    }

    protected override void attackAction()
    {
        // 플레이어 위치
        startPos = player.transform.position;
        startPos.y += 5;

        // 자기 위치
        Vector3 currentPos = transform.position;

        // 양 옆으로 흔들린다.
        float newX = Mathf.Lerp(currentPos.x, startPos.x + Mathf.Sin(Time.time * idle_speed) * height * 5 , mobMoveSpeed * Time.deltaTime);

        // 플레이어 + 4 좌표에 대기한다.

        currentPos.y = Mathf.Lerp(currentPos.y, startPos.y , mobMoveSpeed * Time.deltaTime);

        //transform.position = currentPos;
        transform.position = new Vector3(newX, currentPos.y, 0);
    }

    protected override void checkNow()
    {
        base.checkNow();

        StartCoroutine(shootBullet());
    }

    /// <summary>
    /// 일정 주기마다 탄환을 발사하는 메서드 / 적을 발견할때만 사용한다.
    /// </summary>
    /// <returns></returns>
    IEnumerator shootBullet()
    {
        
        yield return new WaitForSeconds(2.0f);
        Instantiate(obj,this.transform.position,Quaternion.identity);
        StartCoroutine(shootBullet());
    }






}
