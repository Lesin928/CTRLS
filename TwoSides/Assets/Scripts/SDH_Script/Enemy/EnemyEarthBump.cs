using UnityEngine;

/// <summary>
/// 적의 지구 충돌 처리를 담당하는 클래스이다.
/// 이 클래스는 충돌 시 적의 공격을 활성화, 비활성화하며, 플레이어와 충돌 시 피해와 반격을 처리한다.
/// </summary>
public class EnemyEarthBump : MonoBehaviour
{
    private EnemyObject attacker; // 공격한 적 객체

    private void Start()
    {
        // 초기화 시 Collider2D 비활성화
        GetComponent<Collider2D>().enabled = false;
    }

    /// <summary>
    /// 공격한 적 객체를 설정한다.
    /// </summary>
    public void SetAttacker(EnemyObject enemy)
    {
        attacker = enemy;
    }

    /// <summary>
    /// 적의 방향을 설정한다.
    /// </summary>
    /// <param name="facingDir">적의 방향 (1은 오른쪽, -1은 왼쪽)</param>
    public void SetDirection(int facingDir)
    {
        // 적의 방향에 따라 X축 스케일을 반전시킨다.
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * facingDir;
        transform.localScale = scale;
    }

    // 공격을 활성화 (Collider2D 활성화)
    private void EnableAttack()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    // 공격을 비활성화 (Collider2D 비활성화)
    private void DisableAttack()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    /// <summary>
    /// 충돌이 발생했을 때 호출된다.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌한 경우
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<PlayerObject>();
            if (player != null)
            {
                // 플레이어에게 피해를 입힌다.
                player.TakeDamage(attacker.Attack);

                var rb = collision.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    // 플레이어에게 반격을 준다.
                    Vector2 knockbackDir = Vector2.up;  // 반격 방향
                    float knockbackPower = 20f;         // 반격 강도

                    // 반격 적용
                    rb.linearVelocity = knockbackDir * knockbackPower;
                }
            }
        }
    }

    // 공격을 종료하고 오브젝트를 파괴한다.
    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
