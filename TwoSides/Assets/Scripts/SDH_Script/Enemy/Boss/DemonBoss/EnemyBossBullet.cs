using UnityEngine;

/// <summary>
/// 보스의 총알을 처리하는 클래스입니다.
/// 일정 거리 안에 플레이어가 접근하면 소용돌이(Vortex)를 생성합니다.
/// </summary>
public class EnemyBossBullet : MonoBehaviour
{
    // 애니메이션 컨트롤러
    protected Animator anim;

    // Rigidbody2D 컴포넌트
    protected Rigidbody2D rb;

    [Header("Settings")]
    [SerializeField] private float speed = 10f;       // 총알 속도
    [SerializeField] private float lifeTime = 5f;     // 총알의 수명
    [SerializeField] private GameObject vortexPrefab; // 생성할 소용돌이 프리팹

    private EnemyObject attacker; // 이 총알을 발사한 적 오브젝트

    private void Awake()
    {
        // 애니메이터와 리지드바디 컴포넌트 초기화
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // 일정 시간 후 총알 제거
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        // 플레이어와의 x축 거리 계산
        float distanceX = Mathf.Abs(transform.position.x - PlayerManager.instance.player.transform.position.x);

        if (distanceX < 0.5f)
        {
            // 플레이어 근처에 소용돌이 생성
            GameObject vortex = Instantiate(vortexPrefab, transform.position, Quaternion.identity);

            // 소용돌이 스크립트에 공격자 정보 전달
            EnemyBossVortex vortexScript = vortex.GetComponent<EnemyBossVortex>();
            vortexScript.SetAttacker(attacker);

            // 총알 제거
            DestroyTrigger();
        }
    }

    /// <summary>
    /// 이 총알을 발사한 적을 설정합니다.
    /// </summary>
    public void SetAttacker(EnemyObject enemy)
    {
        attacker = enemy;
    }

    /// <summary>
    /// 지정한 위치로 총알을 발사합니다.
    /// </summary>
    /// <param name="targetPosition">발사할 목표 위치</param>
    public void Shoot(Vector2 targetPosition)
    {
        // 현재 위치에서 목표 위치까지의 방향 계산
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        // 속도 적용
        rb.linearVelocity = direction * speed;

        // 총알 회전 방향 설정
        RotateToVelocity(rb.linearVelocity);
    }

    // 속도 벡터 방향에 따라 총알을 회전시키는 함수
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
        if (collision.CompareTag("Player"))
        {
            // 충돌 애니메이션 실행
            anim.SetBool("Hit", true);

            // 이동 정지
            rb.linearVelocity = Vector2.zero;

            // 충돌 비활성화
            GetComponent<Collider2D>().enabled = false;

            // 피해 처리
            collision.GetComponent<PlayerObject>()?.TakeDamage(attacker.Attack);
        }
        else if (collision.CompareTag("Ground"))
        {
            // 충돌 애니메이션 실행
            anim.SetBool("Hit", true);

            // 이동 정지
            rb.linearVelocity = Vector2.zero;

            // 충돌 비활성화
            GetComponent<Collider2D>().enabled = false;
        }
    }

    // 총알을 제거하는 함수
    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
