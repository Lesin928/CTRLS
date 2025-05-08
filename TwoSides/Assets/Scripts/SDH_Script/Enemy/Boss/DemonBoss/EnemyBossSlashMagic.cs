using UnityEngine;

/// <summary>
/// 보스의 슬래시 마법과 관련된 처리를 담당하는 클래스입니다.
/// 슬래시 공격 트리거를 통해 슬래시 생성 및 마법 처리를 담당합니다.
/// </summary>
public class EnemyBossSlashMagic : MonoBehaviour
{
    private EnemyBossSlashAttackTrigger attackTrigger; // 슬래시 공격 트리거와 연동되는 변수

    /// <summary>
    /// 슬래시 공격 트리거를 설정하는 함수입니다.
    /// </summary>
    public void SetAttackTrigger(EnemyBossSlashAttackTrigger trigger)
    {
        attackTrigger = trigger; // 주어진 트리거를 attackTrigger 변수에 할당
    }

    // 슬래시 공격의 후속 처리를 실행하는 함수입니다. 
    // 공격이 끝나면 슬래시 공격 트리거를 호출하고, 해당 객체를 삭제합니다.
    private void DestroyTrigger()
    {
        attackTrigger.SlashAttackTrigger(); // 슬래시 공격을 트리거
        Destroy(gameObject); // 현재 게임 오브젝트를 삭제
    }
}
