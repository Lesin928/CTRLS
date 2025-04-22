using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour
{
    protected Enemy enemy => GetComponentInParent<Enemy>();

    protected virtual void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

}
