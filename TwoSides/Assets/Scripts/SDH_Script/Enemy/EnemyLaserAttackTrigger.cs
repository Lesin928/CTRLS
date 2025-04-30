using UnityEngine;

/// <summary>
/// 적의 레이저 공격 애니메이션 트리거를 처리하는 클래스입니다.
/// 애니메이션 중에 레이저가 활성화되거나 발사됩니다.
/// </summary>
public class EnemyLaserAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject laserActivePrefab; // 레이저 활성화 프리팹
    [SerializeField] private GameObject laserPrefab;       // 레이저 발사 프리팹
    [SerializeField] private Transform firePoint;          // 레이저 발사 위치
    [SerializeField] private Transform player;             // 플레이어의 위치

    // LaserActive 오브젝트(마법진)를 생성 (애니메이션 이벤트에서 호출)
    private void LaserActiveTrigger()
    {
        // LaserActive(마법진) 프리팹을 발사 지점에 생성
        Instantiate(laserActivePrefab, firePoint.position, Quaternion.identity);
    }

    // Laser 오브젝트를 생성 ( 애니메이션 이벤트에서 호출)
    private void LaserAttackTrigger()
    {
        // 부모 EnemyObject를 가져옴
        EnemyObject enemy = GetComponentInParent<EnemyObject>();

        // 레이저 프리팹을 발사 지점에 생성
        GameObject laser = Instantiate(laserPrefab, firePoint.position, Quaternion.identity);

        // 레이저 객체의 스크립트를 가져와서 레이저를 발사
        Laser laserScript = laser.GetComponent<Laser>();
        laserScript.Shoot(enemy.facingDir);
    }
}

