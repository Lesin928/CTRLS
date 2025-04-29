using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f; // 점프 힘
    public int maxJumpCount = 2; // 최대 점프 횟수
    private int jumpCount = 0;
    private Rigidbody2D rb; // Rigidbody2D 변수 선언 필요




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject spawnPoint = GameObject.Find("SpawnPoint");
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
        }
    }

    void Update()    {
        // 좌우 이동
        float moveInput = 0f;
        if (Input.GetKey(KeyCode.LeftArrow))       {
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

    private void OnCollisionEnter2D(Collision2D collision)
    {        // 바닥에 닿았는지 확인
        if (collision.contacts[0].normal.y > 0.5f)
        {
            jumpCount = 0;
        }
    }
}
