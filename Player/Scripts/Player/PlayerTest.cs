using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerTest : MonoBehaviour
{
    public float jumpForce = 10f; // 점프 힘 조절을 위한 변수

    private Rigidbody2D rb; // 플레이어 캐릭터의 Rigidbody2D 컴포넌트를 저장하기 위한 변수
    private bool isGrounded = false; // 플레이어가 땅에 있는지 여부를 나타내는 변수

    void Start()
    {
        // Rigidbody2D 컴포넌트를 가져옴
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Space 키를 누르면 점프 함수 호출
        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        // 아래 방향으로 힘을 가해 점프
        rb.velocity = new Vector2(rb.velocity.x, -jumpForce);
        Debug.Log("Down");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 땅에 닿았을 때 isGrounded를 true로 설정
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // 땅에서 벗어났을 때 isGrounded를 false로 설정
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}

