using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// 적의 레이저 발사를 처리하는 클래스입니다.
/// 애니메이션 이벤트에서 호출되어 방향 설정 및 충돌 처리를 수행합니다.
/// </summary>
public class EnemyLaser : MonoBehaviour
{
    private EnemyObject attacker;
    private bool isEnabled;

    /// <summary>
    /// 공격을 발사한 EnemyObject를 가져오는 함수입니다.
    /// </summary>
    public void SetAttacker(EnemyObject enemy)
    {
        attacker = enemy;
    }

    /// <summary>
    /// 애니메이션 이벤트에서 호출되며, 레이저의 방향과 회전을 설정합니다.
    /// </summary>
    /// <param name="facingDir">적이 바라보는 방향 (1 또는 -1)</param>
    public void Shoot(int facingDir)
    {
        // 바라보는 방향에 따라 Y 축 스케일 조정
        Vector3 scale = transform.localScale;
        scale.y = Mathf.Abs(scale.y) * -facingDir;
        transform.localScale = scale;

        // Z 축 회전값을 90도로 설정
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
    }

    private void EnableAttack()
    {
        isEnabled = true;
        Debug.Log(isEnabled);
    }

    private void DisableAttack()
    {
        isEnabled = false;
        Debug.Log(isEnabled);
    }

    // 플레이어와 충돌 시 호출
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌했을 경우
        if (collision.CompareTag("Player") && isEnabled)
        {
            // 플레이어에게 데미지 전달
            collision.GetComponent<PlayerObject>()?.TakeDamage(attacker.Attack);
        }
    }

    // 애니메이션 이벤트에서 호출되어 레이저 오브젝트를 파괴함
    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
