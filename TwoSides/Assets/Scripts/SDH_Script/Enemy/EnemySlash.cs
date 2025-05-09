using UnityEngine;

/// <summary>
/// 적의 슬래시 공격을 처리하는 클래스.
/// 슬래시 공격 범위 내에 있는 플레이어에게 피해를 입힌다.
/// </summary>
public class EnemySlash : MonoBehaviour
{
    private EnemyObject attacker; // 적의 공격자 (EnemyObject)

    private void Start()
    {
        // 시작 시 공격 콜라이더 비활성화
        GetComponent<Collider2D>().enabled = false;
    }

    /// <summary>
    /// 공격자의 EnemyObject를 설정한다.
    /// </summary>
    public void SetAttacker(EnemyObject enemy)
    {
        attacker = enemy;
    }

    /// <summary>
    /// 공격 방향을 설정한다.
    /// </summary>
    /// <param name="facingDir">공격 방향 (1이면 오른쪽, -1이면 왼쪽)</param>
    public void SetDirection(int facingDir)
    {
        Vector3 scale = transform.localScale;
        // x축 크기 조정을 통해 공격 방향을 설정한다.
        scale.x = Mathf.Abs(scale.x) * facingDir;
        transform.localScale = scale;
    }

    // 슬래시 공격 활성화
    private void EnableAttack()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    // 슬래시 공격 비활성화
    private void DisableAttack()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    // 슬래시 공격이 충돌한 객체에 대해 처리
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 객체가 플레이어일 경우 피해를 입힌다.
        if (collision.CompareTag("Player"))
        {
            // 플레이어 객체가 있을 경우, 피해를 입힌다.
            collision.GetComponent<PlayerObject>()?.TakeDamage(attacker.Attack);
        }
    }

    // 슬래시 공격 후 트리거를 제거하여 객체를 파괴한다.
    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
