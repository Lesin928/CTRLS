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
    [SerializeField] private Transform handPoint;    // 손 위치

    public void SlashAttackTrigger()
    {
        // 부모 EnemyObject를 가져옴
        EnemyObject enemy = GetComponentInParent<EnemyObject>();

        // Slash 프리팹을 발사 지점에 생성
        GameObject slash = Instantiate(slashPrefab, firePoint.position, Quaternion.identity);

        // Slash 객체의 스크립트를 가져와서 활성화
        EnemyBossSlash slashScript = slash.GetComponent<EnemyBossSlash>();
        slashScript.SetAttacker(enemy); // 발사자 전달
        slashScript.Active(enemy.facingDir);
    }

    private void SlashMagicTrigger()
    {
        GameObject magic = Instantiate(magicPrefab, handPoint.position, Quaternion.identity);

        EnemyBossSlashMagic magicScript = magic.GetComponent<EnemyBossSlashMagic>();
        magicScript.SetAttackTrigger(this);
    }

    private void SlashAnimationTrigger()
    {
        // 적의 애니메이션이 완료되었음을 알리는 메서드를 호출
        enemy.AnimationFinishTrigger();
    }
}
