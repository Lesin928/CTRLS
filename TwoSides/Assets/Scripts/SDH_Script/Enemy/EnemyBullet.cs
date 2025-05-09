using UnityEngine;

/// <summary>
/// 적의 총알을 관리하는 클래스이다.
/// 총알은 발사 후 목표 위치를 향해 비행하며, 충돌 시 적이나 땅에 피해를 입힌다.
/// </summary>
public class EnemyBullet : MonoBehaviour
{
    // 애니메이터와 리지드바디2D 컴포넌트
    protected Animator anim;  // 애니메이터
    protected Rigidbody2D rb; // 리지드바디2D

    [Header("Settings")]
    [SerializeField] private float speed = 10f;   // 총알의 비행 속도
    [SerializeField] private float lifeTime = 5f; // 총알의 생명 시간

    private EnemyObject attacker; // 총알을 발사한 적

    private void Awake()
    {
        // 애니메이터와 리지드바디2D 컴포넌트를 가져온다.
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // 생명 시간이 끝난 후 총알을 파괴한다.
        Destroy(gameObject, lifeTime);
    }

    /// <summary>
    /// 적 오브젝트를 설정한다.
    /// </summary>
    public void SetAttacker(EnemyObject enemy)
    {
        attacker = enemy;
    }

    /// <summary>
    /// 총알을 발사한다.
    /// </summary>
    /// <param name="targetPosition">목표 위치</param>
    public void Shoot(Vector2 targetPosition)
    {
        // 목표 위치로 향하는 방향을 구한다.
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        // 방향에 속도를 곱해 총알의 속도를 설정한다.
        rb.linearVelocity = direction * speed;

        // 총알이 이동하는 방향을 맞춘다.
        RotateToVelocity(rb.linearVelocity);
    }

    // 총알의 방향에 맞게 회전하는 함수
    private void RotateToVelocity(Vector2 velocity)
    {
        if (velocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    /// <summary>
    /// 충돌 발생 시 호출되는 함수
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌한 경우
        if (collision.CompareTag("Player"))
        {
            // 충돌 애니메이션을 실행한다.
            anim.SetBool("Hit", true);

            // 총알의 속도를 0으로 설정하여 멈춘다.
            rb.linearVelocity = Vector2.zero;

            // Collider2D를 비활성화하여 충돌 처리를 막는다.
            GetComponent<Collider2D>().enabled = false;

            // 플레이어에게 피해를 입힌다.
            collision.GetComponent<PlayerObject>()?.TakeDamage(attacker.Attack);
        }

        // 땅과 충돌한 경우
        else if (collision.CompareTag("Ground"))
        {
            // 충돌 애니메이션을 실행한다.
            anim.SetBool("Hit", true);

            // 총알의 속도를 0으로 설정하여 멈춘다.
            rb.linearVelocity = Vector2.zero;

            // Collider2D를 비활성화하여 충돌 처리를 막는다.
            GetComponent<Collider2D>().enabled = false;
        }
    }

    // 총알이 충돌 후 일정 시간 지나면 파괴되는 함수
    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
