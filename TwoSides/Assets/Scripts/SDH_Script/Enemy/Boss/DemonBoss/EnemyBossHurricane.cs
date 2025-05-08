using UnityEngine;

/// <summary>
/// 보스의 허리케인 공격을 처리하는 클래스입니다.
/// 허리케인 생성 및 공격 처리를 담당합니다.
/// </summary>
public class EnemyBossHurricane : MonoBehaviour
{
    [SerializeField] GameObject hurricanePrefab; // 허리케인 프리팹
    [SerializeField] LayerMask groundMask;       // 땅 레이어
    private EnemyObject attacker;                // 공격자 객체
    private int facingDir;                       // 바라보는 방향

    private void Start()
    {
        // 시작 시 콜라이더 비활성화
        GetComponent<Collider2D>().enabled = false;
    }

    /// <summary>
    /// 공격자 객체를 설정합니다.
    /// </summary>
    public void SetAttacker(EnemyObject enemy)
    {
        attacker = enemy;
    }

    /// <summary>
    /// 공격자가 바라보는 방향을 설정합니다.
    /// </summary>
    /// <param name="facingDir">바라보는 방향 (1이면 오른쪽, -1이면 왼쪽)</param>
    public void SetDirection(int facingDir)
    {
        this.facingDir = facingDir;
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * facingDir; // 방향에 맞게 스케일 조정
        transform.localScale = scale;
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

    // 충돌 처리 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 플레이어가 허리케인에 맞으면 피해를 입음
            collision.GetComponent<PlayerObject>()?.TakeDamage(attacker.Attack);
        }
    }

    // 허리케인 생성 및 발동
    // 다음 허리케인 생성을 위한 함수 (애니메이션 이벤트에서 호출)
    private void SpawnHurricaneTrigger()
    {
        // 허리케인 프리팹의 너비를 구하고, 방향에 맞게 생성 위치를 계산
        float prefabWidth = hurricanePrefab.GetComponent<BoxCollider2D>().size.x * 2;
        Vector3 offset = new Vector3(prefabWidth * facingDir, 0f, 0f);

        Vector3 spawnPos = transform.position + offset;

        // 생성 위치 아래에 땅이 있는지 체크
        RaycastHit2D hit = Physics2D.Raycast(spawnPos, Vector2.down, 0.1f, groundMask);
        if (hit.collider == null)
            return; // 땅이 없으면 허리케인 생성하지 않음

        // 허리케인 생성
        GameObject hurricane = Instantiate(hurricanePrefab, spawnPos, Quaternion.identity);

        // 생성된 허리케인에 공격자와 방향 설정
        EnemyBossHurricane hurricaneScript = hurricane.GetComponent<EnemyBossHurricane>();
        hurricaneScript.SetAttacker(attacker); // 공격자 설정
        hurricaneScript.SetDirection(facingDir); // 방향 설정
    }

    // 허리케인 객체 삭제
    private void DestroyTrigger()
    {
        Destroy(gameObject); // 자신을 삭제
    }
}
