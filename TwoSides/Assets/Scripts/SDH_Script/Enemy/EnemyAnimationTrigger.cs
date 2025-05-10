using UnityEngine;

/// <summary>
/// 적 오브젝트의 애니메이션 트리거를 처리하는 클래스입니다.
/// 애니메이션이 완료되면 적의 행동을 트리거합니다.
/// </summary>
public class EnemyAnimationTrigger : MonoBehaviour
{
    // 부모 오브젝트의 EnemyObject 컴포넌트를 가져옴
    protected EnemyObject enemy => GetComponentInParent<EnemyObject>();

    // 애니메이션이 완료되었을 때 호출되는 메서드
    protected virtual void AnimationTrigger()
    {
        // 적의 애니메이션이 완료되었음을 알리는 메서드를 호출
        enemy.AnimationFinishTrigger();
    }
}
