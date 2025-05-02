using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 적의 Slash 공격 애니메이션 트리거를 처리하는 클래스입니다.
/// 공격 범위 내에 플레이어가 있으면 공격 성공 메시지를 출력합니다.
/// </summary>
public class EnemySlashAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject slash1Prefab; // Slash1 공격 프리팹
    [SerializeField] private GameObject slash2Prefab; // Slash2 공격 프리팹
    [SerializeField] private Transform firePoint;     // 공격 발사 위치

    private void Slash1AttackTrigger()
    {
        // 부모 EnemyObject를 가져옴
        EnemyObject enemy = GetComponentInParent<EnemyObject>();

        // Slash 프리팹을 발사 지점에 생성
        GameObject slash = Instantiate(slash1Prefab, firePoint.position, Quaternion.identity);

        // Slash 객체의 스크립트를 가져와서 활성화
        EnemySlash slashScript = slash.GetComponent<EnemySlash>();
        slashScript.SetAttacker(enemy); // 발사자 전달
        slashScript.Active(enemy.facingDir);
    }

    private void Slash2AttackTrigger()
    {
        // 부모 EnemyObject를 가져옴
        EnemyObject enemy = GetComponentInParent<EnemyObject>();

        // Slash 프리팹을 발사 지점에 생성
        GameObject slash = Instantiate(slash2Prefab, firePoint.position, Quaternion.identity);

        // Slash 객체의 스크립트를 가져와서 활성화
        EnemySlash slashScript = slash.GetComponent<EnemySlash>();
        slashScript.SetAttacker(enemy); // 발사자 전달
        slashScript.Active(enemy.facingDir);
    }
}
