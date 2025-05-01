using Unity.VisualScripting;
using UnityEngine;

// TODO:
// FIXME:
// NOTE: 공격 범위 처리 방식 리팩토링 예정

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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        // 공격 범위 내의 객체들을 순회
        foreach (var hit in colliders)
        {
            // Collider에 PlayerObject 컴포넌트가 있을 경우 공격이 성공한 것으로 처리
            if (hit.GetComponent<PlayerObject>() != null)
            {
                Debug.Log("공격 성공");
            }
        }
    }
}
