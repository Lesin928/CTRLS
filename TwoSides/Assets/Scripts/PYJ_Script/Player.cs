using UnityEngine;
     
 
 
public class PlayerJump : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f; // Á¡ÇÁ Èû
    public int maxJumpCount = 2; // ÃÖ´ë Á¡ÇÁ È½¼ö

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
        // ÁÂ¿ì ÀÌµ¿
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

        // Á¡ÇÁ
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpCount++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ¹Ù´Ú¿¡ ´ê¾Ò´ÂÁö È®ÀÎ
        if (collision.contacts[0].normal.y > 0.5f)
        {
            jumpCount = 0;
        }
    }
}
