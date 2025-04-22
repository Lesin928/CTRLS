using UnityEngine;

public class EnemyMeleeAttackTrigger : EnemyAnimationTrigger
{
    private void MeleeAttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                Debug.Log("공격 성공");
            }
        }
    }
}
