using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float moveSpeed = 5f; //이동 속도
    public float jumpForce = 5f; // 점프 힘
    public int maxJumpCount = 2; // 최대 점프 횟수
    private int jumpCount = 0;  //현재 점프 횟수
    private Rigidbody2D rb; // Rigidbody2D 변수 선언 필요

    /// <summary>
    /// 시작 시 Rigidbody를 설정하고, "SpawnPoint" 위치로 플레이어를 이동시킴
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject spawnPoint = GameObject.Find("SpawnPoint");
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
        }
    }

    /// <summary>
    /// 매 프레임마다 플레이어의 좌우 이동 및 점프 입력을 처리함
    /// </summary>
    void Update()
    {
        // 좌우 이동
        float moveInput = 0f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveInput = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveInput = 1f;
        }

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // 점프
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount++;
        }
    }

    /// <summary>
    /// 바닥과 충돌 시 점프 횟수를 초기화함
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            jumpCount = 0;
        }
    }
}
