using UnityEngine;

/// <summary>
/// 보스의 스매쉬 공격과 관련된 처리를 담당하는 클래스입니다.
/// 스매쉬 공격이 발생하면, 해당 공격이 플레이어와 충돌할 때 피해를 주고 후속 처리를 수행합니다.
/// </summary>
public class EnemyBossSmash : MonoBehaviour
{
    private EnemyObject attacker; // 스매쉬 공격을 실행한 적 객체

    /// <summary>
    /// 공격을 수행한 적 객체를 설정하는 함수입니다.
    /// </summary>
    /// <param name="enemy">스매쉬 공격을 수행한 적 객체</param>
    public void SetAttacker(EnemyObject enemy)
    {
        attacker = enemy; // 주어진 적 객체를 attacker 변수에 할당
    }

    /// <summary>
    /// 스매쉬 공격 방향을 설정하는 함수입니다.
    /// 공격 방향에 맞게 게임 오브젝트의 스케일을 변경합니다.
    /// </summary>
    /// <param name="facingDir">스매쉬 공격의 방향 (1이면 오른쪽, -1이면 왼쪽)</param>
    public void SetDirection(int facingDir)
    {
        Vector3 scale = transform.localScale; // 현재 스케일을 가져옴
        scale.x = Mathf.Abs(scale.x) * facingDir; // 공격 방향에 맞게 x축 스케일을 변경
        transform.localScale = scale; // 변경된 스케일을 적용
    }

    // 충돌 처리 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 플레이어와 충돌한 경우
        {
            // 플레이어에게 피해를 주기 위해 TakeDamage 함수 호출
            collision.GetComponent<PlayerObject>()?.TakeDamage(attacker.Attack);
        }
    }
}
