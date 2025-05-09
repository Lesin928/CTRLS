using UnityEngine;

/// <summary>
/// 보스의 메테오 공격 처리를 담당하는 클래스입니다.
/// 이 클래스는 메테오 공격을 활성화/비활성화하고, 플레이어와 충돌 시 데미지를 주는 등의 역할을 합니다.
/// </summary>
public class EnemyBossMeteor : MonoBehaviour
{
    private EnemyObject attacker; // 공격자

    private void Start()
    {
        // 게임 시작 시 콜라이더 비활성화
        GetComponent<Collider2D>().enabled = false;
    }

    /// <summary>
    /// 공격자를 설정하는 함수입니다.
    /// </summary>
    /// <param name="enemy">적 객체(EnemyObject)</param>
    public void SetAttacker(EnemyObject enemy)
    {
        attacker = enemy;
    }

    /// <summary>
    /// Meteor의 방향을 설정하는 함수입니다.
    /// </summary>
    /// <param name="facingDir">방향 설정 (1은 오른쪽, -1은 왼쪽)</param>
    public void SetDirection(int facingDir)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * facingDir; // 방향에 맞게 스케일을 조정
        transform.localScale = scale;
    }

    // 공격 활성화 (애니메이션 이벤트에서 호출됨)
    private void EnableAttack()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    // 공격 활성화 (애니메이션 이벤트에서 호출됨)
    private void DisableAttack()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    // 충돌 처리 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 플레이어가 충돌 시 데미지 처리
            collision.GetComponent<PlayerObject>()?.TakeDamage(attacker.Attack);
        }
    }

    // Meteor 공격 후 오브젝트를 제거하는 함수
    private void DestroyTrigger()
    {
        Destroy(gameObject); // Meteor 오브젝트 파괴
    }
}
