using UnityEngine;

/// <summary>
/// 보스의 소용돌이 공격과 관련된 트리거를 담당하는 클래스입니다.
/// 소용돌이 공격이 발생하면 공격의 연출과 후속 처리를 수행합니다.
/// </summary>
public class EnemyBossVortex : MonoBehaviour
{
    private EnemyObject attacker; // 공격자

    private void Start()
    {
        // 소용돌이 시작 시, CapsuleCollider를 비활성화하고 CircleCollider를 활성화
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = true;
    }

    /// <summary>
    /// 공격자를 설정하는 함수입니다.
    /// </summary>
    /// <param name="enemy">적 객체</param>
    public void SetAttacker(EnemyObject enemy)
    {
        attacker = enemy;
    }

    /// <summary>
    /// 소용돌이의 방향을 설정하는 함수입니다.
    /// </summary>
    /// <param name="facingDir">소용돌이의 방향 (1이면 오른쪽, -1이면 왼쪽)</param>
    public void SetDirection(int facingDir)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * facingDir; // x축 크기를 방향에 맞게 설정
        transform.localScale = scale;
    }

    // 공격 활성화 (애니메이션 이벤트에서 호출됨)
    private void EnableAttack()
    {
        GetComponent<CapsuleCollider2D>().enabled = true; // 공격 범위 활성화
        GetComponent<CircleCollider2D>().enabled = false; // Vortex 효과 비활성화
    }

    // 공격 비활성화 (애니메이션 이벤트에서 호출됨)
    private void DisableAttack()
    {
        GetComponent<CapsuleCollider2D>().enabled = false; // 공격 범위 비활성화
    }

    // 충돌 처리 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 플레이어가 소용돌이에 닿으면 공격자의 공격력만큼 피해를 입힘
            collision.GetComponent<PlayerObject>()?.TakeDamage(attacker.Attack);
        }
    }

    // 소용돌이 공격 후, 객체를 제거하는 함수입니다.
    private void DestroyTrigger()
    {
        Destroy(gameObject); // 소용돌이 객체 제거
    }
}
