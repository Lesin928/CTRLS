using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// 적의 지진 공격 이펙트를 처리하는 클래스입니다.
/// 애니메이션 이벤트를 통해 활성화되며, 플레이어와 충돌 시 처리 로직을 수행합니다.
/// </summary>
public class EnemyEarthBump : MonoBehaviour
{
    private EnemyObject attacker; // 공격을 발사한 적 객체

    private void Start()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    /// <summary>
    /// 공격을 발사한 EnemyObject를 가져오는 함수입니다.
    /// </summary>
    public void SetAttacker(EnemyObject enemy)
    {
        attacker = enemy;
    }

    /// <summary>
    /// 이펙트의 방향과 회전을 설정합니다.
    /// </summary>
    /// <param name="facingDir">적이 바라보는 방향 (1 또는 -1)</param>
    public void SetDirection(int facingDir)
    {
        // 바라보는 방향에 따라 Y 축 스케일 조정
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * facingDir;
        transform.localScale = scale;
    }

    // 공격 활성화 (애니메이션 이벤트에서 호출)
    private void EnableAttack()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    // 공격 비활성화 (애니메이션 이벤트에서 호출)
    private void DisableAttack()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    // 플레이어와 충돌 시 호출
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<PlayerObject>();
            if (player != null)
            {
                player.TakeDamage(attacker.Attack);

                var rb = collision.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 knockbackDir = Vector2.up;

                    float knockbackPower = 20f;

                    rb.linearVelocity = knockbackDir * knockbackPower;
                }
            }
        }
    }


    // 애니메이션 이벤트에서 호출되어 오브젝트를 삭제함
    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
