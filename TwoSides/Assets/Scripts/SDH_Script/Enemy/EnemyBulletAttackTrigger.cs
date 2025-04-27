using UnityEngine;

public class EnemyBulletAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform player;

    private void BulletAttackTrigger()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.Shoot(player.position);
    }
}
