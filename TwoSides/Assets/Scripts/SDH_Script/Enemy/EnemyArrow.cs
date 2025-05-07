//using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// 화살 발사를 처리하는 클래스입니다.
/// 목표 위치로 향하는 포물선 궤적을 따라 날아가며, 플레이어와 충돌 시 화살이 플레이어에게 붙습니다.
/// </summary>
public class EnemyArrow : MonoBehaviour
{
    private Rigidbody2D rb; // Rigidbody2D 컴포넌트
    private Collider2D cd;  // Collider2D 컴포넌트

    [Header("Settings")]
    [SerializeField] private float gravityScale = 1f;     // 중력 비율
    [SerializeField] private float arcHeightRatio = 0.2f; // 궤적 높이 비율 (전체 거리의 20%)
    [SerializeField] private float flightTime = 1f;       // 비행 시간
    [SerializeField] private float lifeTime = 5f;         // 화살의 생명 시간

    private EnemyObject attacker;

    void Awake()
    {
        // 애니메이터, 리지드바디2D, 콜라이더 컴포넌트 가져오기
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        // 화살의 속도에 맞춰 회전
        RotateToVelocity(rb.linearVelocity);
    }

    /// <summary>
    /// 공격을 발사한 EnemyObject를 가져오는 함수입니다.
    /// </summary>
    public void SetAttacker(EnemyObject enemy)
    {
        attacker = enemy;
    }

    /// <summary>
    /// 화살을 발사하는 함수입니다.
    /// 목표 위치를 향해 포물선 궤적을 그리며 날아갑니다.
    /// </summary>
    /// <param name="targetPosition">목표 지점의 위치</param>
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

        // 발사 방향에 맞게 화살 회전
        RotateToVelocity(velocity);
    }

    // 화살의 이동 속도에 맞춰 화살의 회전 방향을 설정하는 함수
    void RotateToVelocity(Vector2 velocity)
    {
        if (velocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    // 화살이 다른 객체와 충돌할 때 호출되는 함수
    // 플레이어와 충돌하면 화살이 플레이어에게 붙고, 충돌 후 삭제
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌한 경우
        if (collision.CompareTag("Player"))
        {
            cd.enabled = false;

            // 리지드바디를 Kinematic으로 설정하여 물리 반응을 중지
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            // 화살을 플레이어의 자식 객체로 설정
            transform.parent = collision.transform;

            // 플레이어에게 데미지 전달
            collision.GetComponent<PlayerObject>()?.TakeDamage(attacker.Attack);
        }

        // 일정 시간이 지나면 화살을 삭제
        Destroy(gameObject, lifeTime);
    }
}
