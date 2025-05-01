using UnityEngine;

/// <summary>
/// 적의 총알 공격 애니메이션 트리거를 처리하는 클래스입니다.
/// 이 클래스는 적이 총알을 발사하는 행동을 구현합니다.
/// </summary>
public class EnemyBulletAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject bulletPrefab; // 발사할 총알의 프리팹
    [SerializeField] private Transform firePoint;     // 총알이 발사될 위치

    // 총알 생성 후 발사 (애니메이션 이벤트에서 호출)
    private void BulletAttackTrigger()
    {
        // 총알을 발사 위치에서 인스턴스화
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // 총알의 Bullet 스크립트를 가져와서 발사 방향을 설정
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.Shoot(PlayerManager.instance.player.transform.position); // 플레이어 위치로 총알을 발사
    }
}