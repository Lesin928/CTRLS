using UnityEngine;

/// <summary>
/// 레이저 공격과 함께 마법진 이펙트를 나타내는 클래스입니다.
/// 애니메이션 이벤트를 통해 일정 시점에 삭제됩니다.
/// </summary>
public class EnemyLaserMagic : MonoBehaviour
{
    private EnemyLaserAttackTrigger attackTrigger; // 레이저 공격을 트리거하는 참조 변수

    /// <summary>
    /// 레이저 공격 트리거를 설정합니다.
    /// </summary>
    public void SetAttackTrigger(EnemyLaserAttackTrigger trigger)
    {
        attackTrigger = trigger;
    }

    // 애니메이션 이벤트에서 호출되어 공격을 실행하고 오브젝트를 삭제함
    private void DestroyTrigger()
    {
        attackTrigger.LaserAttackTrigger(); // 레이저 공격 실행
        Destroy(gameObject);
    }
}
