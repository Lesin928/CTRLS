using UnityEngine;

/// <summary>
/// 보스의 해머 공격과 관련된 트리거를 처리하는 클래스입니다.
/// 애니메이션 이벤트에 의해 해머 생성, 마법 생성, 애니메이션 종료 트리거 등을 제어합니다.
/// </summary>
public class EnemyBossHammerAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject hamerPrefab; // 생성할 Hammer 프리팹
    [SerializeField] private GameObject magicPrefab; // 생성할 Magic 프리팹
    [SerializeField] private Transform firePoint;    // 해머 생성 위치
    [SerializeField] private Transform handPoint;    // 마법 생성 위치

    // 해머 마법(애니메이션용)을 생성하는 트리거 (애니메이션 이벤트로 호출됨)
    private void HammerMagicTrigger()
    {
        // handPoint 위치에 마법 생성
        GameObject magic = Instantiate(magicPrefab, handPoint.position, Quaternion.identity);

        EnemyBossHammerMagic magicScript = magic.GetComponent<EnemyBossHammerMagic>();
        magicScript.SetAttackTrigger(this);
    }

    /// <summary>
    /// 해머를 생성하여 공격하는 트리거입니다.
    /// EnemyBossHammerMagic의 DestroyTrigger()에서 호출됩니다.
    /// </summary>
    public void HammerAttackTrigger()
    {
        // 상위 EnemyObject 컴포넌트 가져오기
        EnemyObject enemy = GetComponentInParent<EnemyObject>();

        // 해머 생성
        GameObject hammer = Instantiate(hamerPrefab, firePoint.position, Quaternion.identity);

        // 생성된 해머에 보스 정보 전달
        EnemyBossHammer hammerScript = hammer.GetComponent<EnemyBossHammer>();
        hammerScript.SetAttacker(enemy);
        hammerScript.SetDirection(enemy.facingDir);
    }

    // 애니메이션이 완료되었을 때 호출되는 메서드 (애니메이션 이벤트에서 호출)
    private void HammerAnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
}
