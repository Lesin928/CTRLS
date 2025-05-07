//using System.Threading;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// 총알 객체를 처리하는 클래스입니다.
/// 총알이 발사되고, 목표를 향해 이동하며, 충돌 시 애니메이션과 함께 처리됩니다.
/// </summary>
public class EnemyBossBullet : MonoBehaviour
{
    // 애니메이터와 리지드바디2D 컴포넌트
    protected Animator anim;
    protected Rigidbody2D rb;

    [Header("Settings")]
    [SerializeField] float speed = 10f;       // 총알의 이동 속도
    [SerializeField] float lifeTime = 5f;     // 총알의 생명 시간
    [SerializeField] GameObject vortexPrefab;

    private EnemyObject attacker;

    void Awake()
    {
        // 애니메이터와 리지드바디2D 컴포넌트 가져오기
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // 일정 시간 후 총알 삭제
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        float distanceX = Mathf.Abs(transform.position.x - PlayerManager.instance.player.transform.position.x);
        if (distanceX < 0.5f)
        {
            // 총알 위치에서 프리팹 생성
            GameObject vortex = Instantiate(vortexPrefab, transform.position, Quaternion.identity);

            // Vortex 스크립트를 가져옴
            EnemyBossVortex vortexScript = vortex.GetComponent<EnemyBossVortex>();
            vortexScript.SetAttacker(attacker); // 발사자 전달

            DestroyTrigger();
        }
    }

    /// <summary>
    /// 공격을 발사한 EnemyObject를 가져오는 함수입니다.
    /// </summary>
    public void SetAttacker(EnemyObject enemy)
    {
        attacker = enemy;
    }

    /// <summary>
    /// 총알을 발사하는 함수입니다.
    /// </summary>
    /// <param name="targetPosition">목표 지점의 위치</param>
    public void Shoot(Vector2 targetPosition)
    {
        // 현재 위치에서 목표 위치까지의 방향 계산
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        // 총알 속도 적용
        rb.linearVelocity = direction * speed;

        // 회전도 발사 방향으로 설정
        RotateToVelocity(rb.linearVelocity);
    }

    // 총알의 이동 방향에 맞게 회전하는 함수
    void RotateToVelocity(Vector2 velocity)
    {
        if (velocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    /// 충돌 처리 함수
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌한 경우
        if (collision.CompareTag("Player"))
        {
            // 히트 애니메이션 재생
            anim.SetBool("Hit", true);

            // 충돌 후 총알 정지
            rb.linearVelocity = Vector2.zero;

            // 콜라이더 비활성화
            GetComponent<Collider2D>().enabled = false;

            // 플레이어에게 데미지 전달
            collision.GetComponent<PlayerObject>()?.TakeDamage(attacker.Attack);
        }

        // 벽과 충돌한 경우
        else if (collision.CompareTag("Ground"))
        {
            // 히트 애니메이션 재생
            anim.SetBool("Hit", true);

            // 충돌 후 총알 정지
            rb.linearVelocity = Vector2.zero;

            // 콜라이더 비활성화
            GetComponent<Collider2D>().enabled = false;
        }
    }

    // 애니메이션 이벤트에서 호출되어 이펙트 오브젝트를 파괴
    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
