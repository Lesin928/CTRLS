using UnityEngine;

/// <summary>
/// 보스의 해머 마법을 처리하는 클래스입니다.
/// 해머 공격 트리거를 통해 해머 생성 및 마법 처리를 담당합니다.
/// </summary>
public class EnemyBossHammerMagic : MonoBehaviour
{
    private EnemyBossHammerAttackTrigger attackTrigger; // 해머 공격 트리거를 처리하는 클래스

    /// <summary>
    /// 해머 공격 트리거를 설정합니다.
    /// </summary>
    public void SetAttackTrigger(EnemyBossHammerAttackTrigger trigger)
    {
        attackTrigger = trigger;
    }

    // 해머 공격 트리거 호출 후 객체 삭제 (애니메이션 이벤트에서 호출됨)
    private void DestroyTrigger()
    {
        attackTrigger.HammerAttackTrigger(); // 해머 공격 트리거를 호출
        Destroy(gameObject); // 마법 객체를 파괴
    }
}
