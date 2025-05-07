//using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// 보스의 Hurricane 공격 이펙트를 처리하는 클래스입니다.
/// 애니메이션 이벤트를 통해 활성화되며, 플레이어와 충돌 시 처리 로직을 수행합니다.
/// </summary>
public class EnemyBossHurricane : MonoBehaviour
{
    [SerializeField] GameObject hurricanePrefab; // Hurricane 프리팹
    [SerializeField] LayerMask groundMask;       // Ground 레이어
    private EnemyObject attacker;                // 공격을 발사한 적 객체
    private int facingDir;                       // 소환 방향

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
        this.facingDir = facingDir;
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

    // 다음 Hurricane을 소환하는 트리거 (애니메이션 이벤트에서 호출)
    private void SpawnHurricaneTrigger()
    {
        // 콜라이더 가로 길이 기준 간격 계산
        float prefabWidth = hurricanePrefab.GetComponent<BoxCollider2D>().size.x * 2;
        Vector3 offset = new Vector3(prefabWidth * facingDir, 0f, 0f);

        Vector3 spawnPos = transform.position + offset;

        // 바닥 확인용 Raycast (불꽃 아래 방향으로 짧게 발사)
        RaycastHit2D hit = Physics2D.Raycast(spawnPos, Vector2.down, 0.1f, groundMask);
        if (hit.collider == null)
            return; // 땅이 아니면 생성하지 않음

        GameObject hurricane = Instantiate(hurricanePrefab, spawnPos, Quaternion.identity); // Hurricane 프리팹을 발사 지점에 생성

        // Slash 객체의 스크립트를 가져와서 활성화
        EnemyBossHurricane hurricaneScript = hurricane.GetComponent<EnemyBossHurricane>();
        hurricaneScript.SetAttacker(attacker); // 발사자 전달
        hurricaneScript.SetDirection(facingDir);
    }

    // 애니메이션 이벤트에서 호출되어 이펙트 오브젝트를 파괴
    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
