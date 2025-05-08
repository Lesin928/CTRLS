using UnityEngine;

/// <summary>
/// 보스 적이 휘두르는 해머 공격 판정을 담당하는 클래스입니다.
/// 애니메이션 이벤트를 통해 공격 타이밍에 활성화되며, 
/// 플레이어와 충돌 시 데미지를 주고, 토네이도 이펙트를 생성할 수 있습니다.
/// </summary>
public class EnemyBossHammer : MonoBehaviour
{
    [SerializeField] GameObject hurricanePrefab; // 생성할 허리케인 프리팹
    private EnemyObject attacker;                // 이 해머를 사용하는 보스 적

    private void Start()
    {
        // 시작 시 콜라이더 비활성화
        GetComponent<Collider2D>().enabled = false;
    }

    /// <summary>
    /// 해머의 공격자 정보를 설정합니다.
    /// </summary>
    public void SetAttacker(EnemyObject enemy)
    {
        attacker = enemy;
    }

    // 공격 활성화 (애니메이션 이벤트에서 호출됨)
    private void EnableAttack()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    // 공격 비활성화 (애니메이션 이벤트에서 호출됨)
    private void DisableAttack()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    /// <summary>
    /// 보스의 바라보는 방향에 따라 해머의 방향을 설정합니다.
    /// </summary>
    /// <param name="facingDir">보스가 바라보는 방향 (1 또는 -1)</param>
    public void SetDirection(int facingDir)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * facingDir;
        transform.localScale = scale;
    }

    // 플레이어와 충돌 시 데미지를 줌
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 플레이어에게 데미지 부여
            collision.GetComponent<PlayerObject>()?.TakeDamage(attacker.Attack);
        }
    }

    // 허리케인 이펙트 생성 (애니메이션 이벤트를 통해 호출)
    private void SpawnHurricaneTrigger()
    {
        // 현재 위치에 허리케인 프리팹 생성
        GameObject hurricane = Instantiate(hurricanePrefab, transform.position, Quaternion.identity);

        // 허리케인에 공격자 정보 및 방향 전달
        EnemyBossHurricane hurricaneScript = hurricane.GetComponent<EnemyBossHurricane>();
        hurricaneScript.SetAttacker(attacker);
        hurricaneScript.SetDirection(attacker.facingDir);
    }

    // 해머 오브젝트 제거 (애니메이션 이벤트를 통해 호출됨)
    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
