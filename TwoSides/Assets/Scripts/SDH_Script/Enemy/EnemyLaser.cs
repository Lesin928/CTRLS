using UnityEngine;

/// <summary>
/// 적의 레이저 공격을 처리하는 클래스.
/// 레이저는 발사 후 일정 시간이 지난 후 자동으로 삭제되며, 플레이어와 충돌 시 피해를 입힌다.
/// </summary>
public class EnemyLaser : MonoBehaviour
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
    /// 레이저의 방향을 설정한다.
    /// </summary>
    /// <param name="facingDir">레이저의 방향 (1은 오른쪽, -1은 왼쪽)</param>
    public void SetDirection(int facingDir)
    {
        // 레이저의 Y축 반전을 설정하여 방향을 바꾼다.
        Vector3 scale = transform.localScale;
        scale.y = Mathf.Abs(scale.y) * -facingDir;
        transform.localScale = scale;

        // Z 축 회전을 90도로 설정하여 레이저의 방향을 조정한다.
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
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
    /// 레이저가 충돌한 객체와의 상호작용을 처리한다.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 객체가 플레이어일 경우
        if (collision.CompareTag("Player"))
        {
            // 플레이어 객체가 존재하면 피해를 입힌다.
            collision.GetComponent<PlayerObject>()?.TakeDamage(attacker.Attack);
        }
    }

    // 레이저가 더 이상 필요 없을 경우 오브젝트를 삭제한다.
    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
