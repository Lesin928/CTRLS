//using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// 보스의 Slash 공격 이펙트를 처리하는 클래스입니다.
/// 애니메이션 이벤트를 통해 활성화되며, 플레이어와 충돌 시 처리 로직을 수행합니다.
/// </summary>
public class EnemyBossSlashMagic : MonoBehaviour
{
    private EnemyBossSlashAttackTrigger attackTrigger; // Slash 공격을 트리거하는 참조 변수

    /// <summary>
    /// Slash 공격 트리거를 설정합니다.
    /// </summary>
    public void SetAttackTrigger(EnemyBossSlashAttackTrigger trigger)
    {
        attackTrigger = trigger;
    }

    // 애니메이션 이벤트에서 호출되어 공격을 실행하고 오브젝트를 삭제함
    private void DestroyTrigger()
    {
        attackTrigger.SlashAttackTrigger(); // Slash 공격 실행
        Destroy(gameObject);
    }
}
