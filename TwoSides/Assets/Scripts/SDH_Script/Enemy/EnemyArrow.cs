using UnityEngine;

/// <summary>
/// 적의 화살을 발사하는 스크립트입니다.
/// 화살은 목표 지점으로 일정한 비율로 곡선 비행하며, 목표에 충돌하면 피해를 입히고, 일정 시간이 지나면 자동으로 삭제됩니다.
/// </summary>
public class EnemyArrow : MonoBehaviour
{
    private Rigidbody2D rb; // Rigidbody2D 구성 요소
    private Collider2D cd;  // Collider2D 구성 요소

    [Header("Settings")]
    [SerializeField] private float gravityScale = 1f;     // 중력의 강도
    [SerializeField] private float arcHeightRatio = 0.2f; // 곡선 높이 비율 (기본 20%)
    [SerializeField] private float flightTime = 1f;       // 비행 시간
    [SerializeField] private float lifeTime = 5f;         // 화살의 생명 시간

    private EnemyObject attacker;

    private void Awake()
    {
        // 초기화: Rigidbody2D와 Collider2D 컴포넌트 가져오기
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        // 비행 중 화살의 속도에 맞춰 회전
        RotateToVelocity(rb.linearVelocity);
    }

    /// <summary>
    /// 적 객체를 설정합니다.
    /// </summary>
    public void SetAttacker(EnemyObject enemy)
    {
        attacker = enemy;
    }

    /// <summary>
    /// 목표 위치를 기준으로 화살을 발사합니다.
    /// 주어진 시간 내에 곡선 비행을 하도록 설정합니다.
    /// </summary>
    /// <param name="targetPosition">목표 위치</param>
    public void Shoot(Vector2 targetPosition)
    {
        Vector2 startPosition = transform.position;
        float gravity = Mathf.Abs(Physics2D.gravity.y * gravityScale); // 중력 계산
        float distanceX = targetPosition.x - startPosition.x;
        float distanceY = targetPosition.y - startPosition.y;

        // 수평 속도 계산
        float initialVx = distanceX / flightTime;

        // 곡선 높이 비율에 따른 수직 속도 계산
        float arcHeight = Mathf.Abs(distanceX) * arcHeightRatio;

        // 수직 속도 계산
        float initialVy = Mathf.Sqrt(2 * gravity * arcHeight);

        // 비행 시간을 고려한 최종 수직 속도 수정
        float totalTime = (2 * initialVy) / gravity;
        initialVy = (gravity * flightTime) / 2f;  // 수직 속도 수정

        // 초기 속도 벡터 설정
        Vector2 velocity = new Vector2(initialVx, initialVy);

        rb.gravityScale = gravityScale;
        rb.linearVelocity = velocity;

        // 화살 회전
        RotateToVelocity(velocity);
    }

    // 화살이 이동하는 방향에 맞춰 회전
    private void RotateToVelocity(Vector2 velocity)
    {
        if (velocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    // 충돌이 발생하면 적에게 피해를 입히고 화살을 삭제하는 함수
    // 화살이 일정 시간 후 파괴되도록 설정
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌 시
        if (collision.CompareTag("Player"))
        {
            cd.enabled = false;

            // 화살을 Kinematic으로 설정하여 물리 법칙에 의해 이동하지 않도록 설정
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            // 화살이 플레이어의 자식 객체가 되도록 설정
            transform.parent = collision.transform;

            // 플레이어에게 피해를 입힘
            collision.GetComponent<PlayerObject>()?.TakeDamage(attacker.Attack);
        }

        // 생명 시간이 끝나면 화살 파괴
        Destroy(gameObject, lifeTime);
    }
}
