//using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 적의 Slash 공격 애니메이션 트리거를 처리하는 클래스입니다.
/// 공격 범위 내에 플레이어가 있으면 공격 성공 메시지를 출력합니다.
/// </summary>
public class EnemyBossHammerAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject hamerPrefab; // Hammer 프리팹
    [SerializeField] private GameObject magicPrefab; // Magic 프리팹
    [SerializeField] private Transform firePoint;    // 공격 발사 위치
    [SerializeField] private Transform handPoint;    // 손 위치

    // HammerMagic 오브젝트(마법진)를 생성 (애니메이션 이벤트에서 호출)
    private void HammerMagicTrigger()
    {
        // HammerMagic(마법진) 프리팹을 handPoint에 생성
        GameObject magic = Instantiate(magicPrefab, handPoint.position, Quaternion.identity);

        EnemyBossHammerMagic magicScript = magic.GetComponent<EnemyBossHammerMagic>();
        magicScript.SetAttackTrigger(this);
    }

    /// <summary>
    /// 실제 Hammer를 발사하는 트리거입니다.
    /// EnemyBossHammerMagic DestroyTrigger()에서 호출됩니다.
    /// </summary>
    public void HammerAttackTrigger()
    {
        // 부모 EnemyObject를 가져옴
        EnemyObject enemy = GetComponentInParent<EnemyObject>();

        // Hammer 프리팹을 발사 지점에 생성
        GameObject hammer = Instantiate(hamerPrefab, firePoint.position, Quaternion.identity);

        // Hammer 객체의 스크립트를 가져와서 활성화
        EnemyBossHammer hammerScript = hammer.GetComponent<EnemyBossHammer>();
        hammerScript.SetAttacker(enemy); // 발사자 전달
        hammerScript.SetDirection(enemy.facingDir);
    }

    // 애니메이션이 완료되었을 때 호출되는 메서드
    private void HammerAnimationTrigger()
    {
        // 적의 애니메이션이 완료되었음을 알리는 메서드를 호출
        enemy.AnimationFinishTrigger();
    }
}
