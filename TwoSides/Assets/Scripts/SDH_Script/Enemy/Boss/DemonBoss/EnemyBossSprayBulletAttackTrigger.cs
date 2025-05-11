using UnityEngine;

/// <summary>
/// 보스의 총알 공격 애니메이션 트리거를 처리하는 클래스입니다.
/// 이 클래스는 보스가 총알을 발사하는 행동을 구현합니다.
/// </summary>
public class EnemyBossSprayBulletAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject bulletPrefab; // 발사할 총알의 프리팹
    [SerializeField] private Transform firePoint;     // 총알이 발사될 위치

    // 총알 생성 후 발사 (애니메이션 이벤트에서 호출)
    void SprayBulletAttackTrigger()
    {
        int bulletCount = 10;
        float angleStep = 360f / bulletCount;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep;
            float radian = angle * Mathf.Deg2Rad;

            Vector2 direction = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)).normalized;

            // 총알 인스턴스화
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            // 총알 스크립트 설정
            EnemyBossSprayBullet bulletScript = bullet.GetComponent<EnemyBossSprayBullet>();
            bulletScript.SetAttacker(enemy);
            bulletScript.Shoot(direction);
        }
    }

    // 애니메이션이 완료되었을 때 호출되는 메서드 (애니메이션 이벤트에서 호출)
    private void SprayBulletAnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
}