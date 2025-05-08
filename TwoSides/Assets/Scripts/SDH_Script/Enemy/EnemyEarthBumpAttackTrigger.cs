using UnityEngine;

/// <summary>
/// 적의 지진 공격 애니메이션 트리거를 처리하는 클래스입니다.
/// 애니메이션 이벤트에서 호출되어 지진 범위 공격을 발사합니다.
/// </summary>
public class EnemyEarthBumpAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject earthBumpPrefab; // 지진 범위 공격 프리팹
    [SerializeField] private Transform firePoint;        // 공격 발사 위치

    /// 지진 범위 공격을 발사 (애니메이션 이벤트에서 호출)
    private void EarthBumpAttackTrigger()
    {
        // 지진 범위 공격 프리팹을 발사 지점에 생성
        GameObject earthBump = Instantiate(earthBumpPrefab, firePoint.position, Quaternion.identity);

        // 지진 범위 공격 객체의 스크립트를 가져와서 활성화
        EnemyEarthBump earthBumpScript = earthBump.GetComponent<EnemyEarthBump>();
        earthBumpScript.SetAttacker(enemy); // 발사자 전달
        earthBumpScript.SetDirection(enemy.facingDir);// 방향 설정
    }
}
