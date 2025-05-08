using UnityEngine;

/// <summary>
/// 적의 근접 공격 트리거를 처리하는 클래스.
/// 근접 공격 범위 내에 있는 플레이어에게 피해를 입힌다.
/// </summary>
public class EnemyMeleeAttackTrigger : EnemyAnimationTrigger
{
    // 근접 공격 트리거를 활성화하여 범위 내 플레이어에게 피해를 입힌다.
    private void MeleeAttackTrigger()
    {
        // 공격 체크 위치를 중심으로 일정 반경 내에 있는 콜라이더를 모두 가져옴
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        // 공격 범위 내에 있는 모든 객체에 대해 처리
        foreach (var hit in colliders)
        {
            // 플레이어 객체가 있을 경우, 그에게 피해를 입힌다.
            hit.GetComponent<PlayerObject>()?.TakeDamage(enemy.Attack);
        }
    }
}
