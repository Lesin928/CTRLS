using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour
{
    protected EnemyObject enemy => GetComponentInParent<EnemyObject>();

    protected virtual void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

}
