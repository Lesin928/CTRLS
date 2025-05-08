using UnityEngine;

/// <summary>
/// 보스 총알 오브젝트를 처리하는 클래스입니다.
/// 보스로부터 발사되고, 플레이어를 향해 이동하며, 충돌 시 피격 애니메이션을 재생하고 제거됩니다.
/// </summary>
public class EnemyBossCircleBullet : MonoBehaviour
{
    // 애니메이터 및 Rigidbody2D 컴포넌트
    protected Animator anim;
    protected Rigidbody2D rb;

    [Header("Settings")]
    [SerializeField] private float speed = 10f;   // 총알 이동 속도
    [SerializeField] private float lifeTime = 5f; // 총알 수명

    private EnemyObject attacker; // 발사한 적 객체

    private void Awake()
    {
        // 애니메이터와 리지드바디 컴포넌트 초기화
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // 일정 시간이 지나면 총알 삭제
        Destroy(gameObject, lifeTime);
    }

    /// <summary>
    /// 이 총알을 발사한 적을 설정합니다.
    /// </summary>
    public void SetAttacker(EnemyObject enemy)
    {
        attacker = enemy;
    }

    // 플레이어를 향해 총알을 발사하는 메서드 (애니메이션 이벤트로 호출)
    private void Shoot()
    {
        Vector2 targetPosition = PlayerManager.instance.player.transform.position;

        // 현재 위치에서 플레이어 위치까지 방향 계산
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        // 방향과 속도를 곱하여 이동 시작
        rb.linearVelocity = direction * speed;

        // 총알 회전 방향 설정
        RotateToVelocity(rb.linearVelocity);
    }

    // 총알이 날아가는 방향으로 회전시키는 메서드
    private void RotateToVelocity(Vector2 velocity)
    {
        if (velocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    // 충돌 처리 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌했을 때
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("Hit", true); // 피격 애니메이션 재생
            rb.linearVelocity = Vector2.zero; // 이동 중지
            GetComponent<Collider2D>().enabled = false; // 충돌 비활성화

            // 플레이어에게 피해 입힘
            collision.GetComponent<PlayerObject>()?.TakeDamage(attacker.Attack);
        }
        // 바닥과 충돌했을 때
        else if (collision.CompareTag("Ground"))
        {
            anim.SetBool("Hit", true); // 피격 애니메이션 재생
            rb.linearVelocity = Vector2.zero; // 이동 중지
            GetComponent<Collider2D>().enabled = false; // 충돌 비활성화
        }
    }

    // 애니메이션 이벤트에서 호출되어 총알을 제거하는 메서드
    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
