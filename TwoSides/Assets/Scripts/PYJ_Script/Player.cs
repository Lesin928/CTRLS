using UnityEngine;
     
 
 
public class PlayerJump : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f; // ���� ��
    public int maxJumpCount = 2; // �ִ� ���� Ƚ��

    private Rigidbody2D rb;
    private int jumpCount = 0;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject spawnPoint = GameObject.Find("SpawnPoint");
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
        }
    }

    void Update()
    {
        // �¿� �̵�
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

        // ����
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �ٴڿ� ��Ҵ��� Ȯ��
        if (collision.contacts[0].normal.y > 0.5f)
        {
            jumpCount = 0;
        }
    }
}
