using UnityEngine;

/// <summary>
/// 보스의 슬래시 공격과 관련된 트리거를 처리하는 클래스입니다.
/// 슬래시와 마법을 생성하고, 해당 공격의 트리거를 관리합니다.
/// </summary>
public class EnemyBossSlashAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject slashPrefab; // 슬래시 공격 프리팹
    [SerializeField] private GameObject magicPrefab; // 마법 프리팹
    [SerializeField] private Transform firePoint;    // 슬래시 공격이 발사될 위치
    [SerializeField] private Transform handPoint;    // 마법이 생성될 위치

    // 슬래시 Magic 공격을 트리거하는 함수입니다.
    private void SlashMagicTrigger()
    {
        // 마법을 발사할 위치에서 마법 프리팹을 생성
        GameObject magic = Instantiate(magicPrefab, handPoint.position, Quaternion.identity);

        // 생성된 마법 객체에 대한 스크립트를 가져와 설정
        EnemyBossSlashMagic magicScript = magic.GetComponent<EnemyBossSlashMagic>();
        magicScript.SetAttackTrigger(this); // 현재 트리거를 마법에 전달
    }

    /// <summary>
    /// 슬래시 공격을 트리거하는 함수입니다.
    /// EnemyBossSlashMagic의 DestroyTrigger()가 호출되면 해당 공격을 처리합니다.
    /// </summary>
    public void SlashAttackTrigger()
    {
        // 부모 객체에서 EnemyObject를 가져옵니다.
        EnemyObject enemy = GetComponentInParent<EnemyObject>();

        // 슬래시 공격을 발사할 위치에서 슬래시 공격 프리팹을 생성
        GameObject slash = Instantiate(slashPrefab, firePoint.position, Quaternion.identity);

        // 생성된 슬래시 공격 객체에 대한 스크립트를 가져와 설정
        EnemyBossSlash slashScript = slash.GetComponent<EnemyBossSlash>();
        slashScript.SetAttacker(enemy); // 슬래시 공격의 공격자를 설정
        slashScript.SetDirection(enemy.facingDir); // 슬래시 공격의 방향 설정
    }

    // 애니메이션이 완료되었을 때 호출되는 메서드 (애니메이션 이벤트에서 호출)
    private void SlashAnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
}
