using UnityEngine;

public class EnemyArrowAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform player;

    private void ArrowAttackTrigger()
    {
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        arrowScript.Shoot(player.position);
    }
}
