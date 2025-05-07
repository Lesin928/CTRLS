using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 적의 Slash 공격 애니메이션 트리거를 처리하는 클래스입니다.
/// 공격 범위 내에 플레이어가 있으면 공격 성공 메시지를 출력합니다.
/// </summary>
public class EnemyBossSlashAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject slashPrefab; // Slash 프리팹
    [SerializeField] private GameObject magicPrefab; // Magic 프리팹
    [SerializeField] private Transform firePoint;    // 공격 발사 위치
    [SerializeField] private Transform handPoint;    // 손 위치 (Magic 활성화)

    // SlashMagic 오브젝트(마법진)를 생성 (애니메이션 이벤트에서 호출)
    private void SlashMagicTrigger()
    {
        // SlashMagic(마법진) 프리팹을 handPoint에 생성
        GameObject magic = Instantiate(magicPrefab, handPoint.position, Quaternion.identity);

        EnemyBossSlashMagic magicScript = magic.GetComponent<EnemyBossSlashMagic>();
        magicScript.SetAttackTrigger(this);
    }

    /// <summary>
    /// 실제 Slash를 발사하는 트리거입니다.
    /// EnemyBossSlashMagic DestroyTrigger()에서 호출됩니다.
    /// </summary>
    public void SlashAttackTrigger()
    {
        // 부모 EnemyObject를 가져옴
        EnemyObject enemy = GetComponentInParent<EnemyObject>();

        // Slash 프리팹을 발사 지점에 생성
        GameObject slash = Instantiate(slashPrefab, firePoint.position, Quaternion.identity);

        // Slash 객체의 스크립트를 가져와서 활성화
        EnemyBossSlash slashScript = slash.GetComponent<EnemyBossSlash>();
        slashScript.SetAttacker(enemy); // 발사자 전달
        slashScript.SetDirection(enemy.facingDir);
    }

    private void SlashAnimationTrigger()
    {
        // 적의 애니메이션이 완료되었음을 알리는 메서드를 호출
        enemy.AnimationFinishTrigger();
    }
}
