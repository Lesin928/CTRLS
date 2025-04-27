using UnityEngine;

public class EnemyLaserAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject laserActivePrefab;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform player;

    private void LaserActiveTrigger()
    {
        Instantiate(laserActivePrefab, firePoint.position, Quaternion.identity);
    }

    private void LaserAttackTrigger()
    {
        EnemyObject enemy = GetComponentInParent<EnemyObject>();
        GameObject laser = Instantiate(laserPrefab, firePoint.position, Quaternion.identity);
        Laser laserScript = laser.GetComponent<Laser>();
        laserScript.Shoot(enemy.facingDir);
    }
}
