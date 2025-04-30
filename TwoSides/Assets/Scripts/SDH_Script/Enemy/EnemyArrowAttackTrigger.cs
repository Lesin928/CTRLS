using UnityEngine;

/// <summary>
/// 적의 화살 공격 애니메이션 트리거를 처리하는 클래스입니다.
/// 애니메이션 이벤트에서 호출되어 화살을 발사합니다.
/// </summary>
public class EnemyArrowAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject arrowPrefab; // 화살 프리팹
    [SerializeField] private Transform firePoint;    // 화살 발사 위치
    [SerializeField] private Transform player;       // 플레이어의 위치

    // 화살 생성 후 발사 (애니메이션 이벤트에서 호출)
    private void ArrowAttackTrigger()
    {
        // 화살 프리팹을 발사 지점에 생성
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);

        // 화살 객체의 스크립트를 가져와서 플레이어를 향해 발사
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        arrowScript.Shoot(player.position);
    }
}
