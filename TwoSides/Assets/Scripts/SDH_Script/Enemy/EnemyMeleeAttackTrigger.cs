using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// 적의 근접 공격 애니메이션 트리거를 처리하는 클래스입니다.
/// 공격 범위 내에 플레이어가 있으면 공격 성공 메시지를 출력합니다.
/// </summary>
public class EnemyMeleeAttackTrigger : EnemyAnimationTrigger
{
    // 근접 공격 처리 메서드 (애니메이션 이벤트에서 호출)
    private void MeleeAttackTrigger()
    {
        // 공격 범위 내에 있는 모든 Collider2D 객체를 가져옴
        // 모든 객체를 가져오는 이유: 범위 안에 공격하는 Enemy도 포함되기 때문에
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        // 공격 범위 내의 객체들을 순회
        foreach (var hit in colliders)
        {
            // 플레이어에게 데미지 전달
            hit.GetComponent<PlayerObject>()?.TakeDamage(enemy.Attack); 
        }
    }
}
