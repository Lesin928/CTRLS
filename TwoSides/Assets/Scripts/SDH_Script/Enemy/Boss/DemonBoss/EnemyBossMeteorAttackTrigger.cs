using UnityEngine;

/// <summary>
/// 보스의 메테오 공격 트리거 처리를 담당하는 클래스입니다.
/// 이 클래스는 메테오 공격을 발동하고, 애니메이션이 끝났을 때 후속 작업을 처리합니다.
/// </summary>
public class EnemyBossMeteorAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject meteorPrefab; // 메테오 프리팹
    [SerializeField] private Transform firePoint;     // 메테오 발사 위치

    // 메테오 공격 트리거
    private void MeteorAttackTrigger()
    {
        // 부모 객체에서 EnemyObject를 찾습니다.
        EnemyObject enemy = GetComponentInParent<EnemyObject>();

        // 메테오 프리팹을 발사 위치에서 인스턴스화합니다.
        GameObject meteor = Instantiate(meteorPrefab, firePoint.position, Quaternion.identity);

        // 메테오 객체에 대한 스크립트를 가져와서 공격자와 방향을 설정합니다.
        EnemyBossMeteor meteorScript = meteor.GetComponent<EnemyBossMeteor>();
        meteorScript.SetAttacker(enemy); // 공격자 설정
        meteorScript.SetDirection(enemy.facingDir); // 방향 설정
    }

    // 애니메이션이 완료되었을 때 호출되는 메서드 (애니메이션 이벤트에서 호출)
    private void MeteorAnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
}
