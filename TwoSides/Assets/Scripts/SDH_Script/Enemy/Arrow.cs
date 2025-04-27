using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D cd;

    [Header("Settings")]
    [SerializeField] private float gravityScale = 1f;
    [SerializeField] private float arcHeightRatio = 0.2f; // 전체 거리의 20%를 궤적 높이로
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] float flightTime = 1f;
    private bool hasHit = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        RotateToVelocity(rb.linearVelocity);
    }

    public void Shoot(Vector2 targetPosition)
    {
        Vector2 startPosition = transform.position;
        float gravity = Mathf.Abs(Physics2D.gravity.y * gravityScale);
        float distanceX = targetPosition.x - startPosition.x;
        float distanceY = targetPosition.y - startPosition.y;

        // 수평 속도는 거리 / 시간
        float initialVx = distanceX / flightTime;

        // arcHeight를 설정하여 목표까지 가는 경로의 높이를 결정
        float arcHeight = Mathf.Abs(distanceX) * arcHeightRatio;

        // arcHeight를 고려한 수직 속도 계산
        float initialVy = Mathf.Sqrt(2 * gravity * arcHeight);

        // 시간에 맞춰서 Vy 보정
        float totalTime = (2 * initialVy) / gravity;
        initialVy = (gravity * flightTime) / 2f;  // 수정된 수직 속도

        // 수직과 수평 속도를 각각 설정하여 발사
        Vector2 velocity = new Vector2(initialVx, initialVy);

        rb.gravityScale = gravityScale;
        rb.linearVelocity = velocity;

        RotateToVelocity(velocity);
    }

    void RotateToVelocity(Vector2 velocity)
    {
        if (velocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit) return;

        if (collision.CompareTag("Player"))
        {
            hasHit = true;
            cd.enabled = false;

            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            transform.parent = collision.transform;
        }

        Destroy(gameObject, lifeTime);
    }
}
