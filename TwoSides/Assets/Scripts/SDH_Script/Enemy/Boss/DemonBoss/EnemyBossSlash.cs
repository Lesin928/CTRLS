using UnityEngine;

/// <summary>
/// 보스의 슬래시 공격을 처리하는 클래스입니다.
/// 슬래시 공격의 발동과 콜라이더 활성화/비활성화를 담당합니다.
/// </summary>
public class EnemyBossSlash : MonoBehaviour
{
    private EnemyObject attacker; // 공격자

    private void Start()
    {
        // 시작 시 콜라이더 비활성화
        GetComponent<Collider2D>().enabled = false;
    }

    /// <summary>
    /// 공격자를 설정하는 함수입니다.
    /// </summary>
    /// <param name="enemy">공격자</param>
    public void SetAttacker(EnemyObject enemy)
    {
        attacker = enemy;
    }

    /// <summary>
    /// 슬래시 공격의 방향을 설정하는 함수입니다.
    /// </summary>
    /// <param name="facingDir">공격 방향 (1이면 오른쪽, -1이면 왼쪽)</param>
    public void SetDirection(int facingDir)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * facingDir; // x축 방향 반전
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
            // 플레이어가 공격에 맞으면 피해를 입힘
            collision.GetComponent<PlayerObject>()?.TakeDamage(attacker.Attack);
        }
    }

    // 공격이 끝나면 해당 객체를 파괴하는 함수
    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
