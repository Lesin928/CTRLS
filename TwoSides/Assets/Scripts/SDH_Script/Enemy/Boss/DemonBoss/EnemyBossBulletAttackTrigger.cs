using UnityEngine;

/// <summary>
/// 보스의 총알 공격 애니메이션 트리거를 처리하는 클래스입니다.
/// 이 클래스는 보스가 총알을 발사하는 행동을 구현합니다.
/// </summary>
public class EnemyBossBulletAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject bulletPrefab; // 발사할 총알의 프리팹
    [SerializeField] private Transform firePoint;     // 총알이 발사될 위치

    // 총알 생성 후 발사 (애니메이션 이벤트에서 호출)
    private void BulletAttackTrigger()
    {
        // 총알을 발사 위치에서 인스턴스화
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // 총알의 Bullet 스크립트를 가져와서 발사 방향을 설정
        EnemyBossBullet bulletScript = bullet.GetComponent<EnemyBossBullet>();
        bulletScript.SetAttacker(enemy); // 발사자 전달

        // 목표 위치의 x는 플레이어, y는 firePoint 위치로 고정
        Vector2 targetPosition = new Vector2(PlayerManager.instance.player.transform.position.x, firePoint.position.y);
        bulletScript.Shoot(targetPosition);
    }

    // 애니메이션이 완료되었을 때 호출되는 메서드 (애니메이션 이벤트에서 호출)
    private void BulletAnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
}