using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 애니메이터와 리지드바디2D 컴포넌트
    protected Animator anim;
    protected Rigidbody2D rb;

    [Header("Settings")]
    [SerializeField] float speed = 10f;
    [SerializeField] float lifeTime = 5f;

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

    public void Shoot(Vector2 targetPosition)
    {
        // 이동 애니메이션으로 변경
        anim.SetBool("Move", true);

        // 현재 위치에서 목표 위치까지의 방향 계산
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        // 총알 속도 적용
        rb.linearVelocity = direction * speed;

        // 회전도 발사 방향으로 설정
        RotateToVelocity(rb.linearVelocity);
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
        anim.SetBool("Move", false);

        // 플레이어와 충돌한 경우
        if (collision.CompareTag("Player"))
        {
            // 히트 애니메이션 재생
            anim.SetBool("Hit", true);

            // 충돌 후 총알 정지
            rb.linearVelocity = Vector2.zero;

            // 몬스터의 TakeDamage 함수 호출하여 피해 주기
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("공격 성공");
            }

            // 0.2초 후 총알 객체를 파괴
            Destroy(gameObject, 0.2f);
        }

        // 벽과 충돌한 경우
        else if(!collision.CompareTag("Enemy"))
        {
            // 히트 애니메이션 재생
            anim.SetBool("Hit", true);

            // 충돌 후 총알 정지
            rb.linearVelocity = Vector2.zero;

            // 0.2초 후 총알 객체를 파괴
            Destroy(gameObject, 0.2f);
        }
    }
}
