//using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// 보스의 Meteor 공격 이펙트를 처리하는 클래스입니다.
/// 애니메이션 이벤트를 통해 활성화되며, 플레이어와 충돌 시 처리 로직을 수행합니다.
/// </summary>
public class EnemyBossMeteor : MonoBehaviour
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
    /// 적의 바라보는 방향에 맞춰 이펙트 방향을 설정합니다.
    /// </summary>
    /// <param name="facingDir">적이 바라보는 방향 (1 또는 -1)</param>
    public void SetDirection(int facingDir)
    {
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
            // 플레이어에게 데미지 전달
            collision.GetComponent<PlayerObject>()?.TakeDamage(attacker.Attack);
        }
    }

    // 애니메이션 이벤트에서 호출되어 이펙트 오브젝트를 파괴
    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
