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

    // Slash 공격을 생성하고 방향 및 공격자를 설정하는 공통 함수
    private void TriggerSlashAttack(GameObject slashPrefab)
    {
        // 부모 오브젝트에서 EnemyObject 컴포넌트를 가져옴
        EnemyObject enemy = GetComponentInParent<EnemyObject>();

        // Slash 프리팹을 발사 위치에 생성
        GameObject slash = Instantiate(slashPrefab, firePoint.position, Quaternion.identity);

        // 생성된 Slash 오브젝트에 공격자와 방향 설정
        EnemySlash slashScript = slash.GetComponent<EnemySlash>();
        slashScript.SetAttacker(enemy);
        slashScript.SetDirection(enemy.facingDir);
    }

    // Slash1 공격을 생성하는 애니메이션 이벤트용 함수
    private void Slash1AttackTrigger()
    {
        TriggerSlashAttack(slash1Prefab);
    }

    // Slash2 공격을 생성하는 애니메이션 이벤트용 함수
    private void Slash2AttackTrigger()
    {
        TriggerSlashAttack(slash2Prefab);
    }
}
