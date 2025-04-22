using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        Debug.Log("Attack");
        base.Update();

        enemyBase.SetZeroVelocity();

        if (triggerCalled)
            enemyBase.EnterPlayerDetection();
    }

    public override void Exit()
    {
        base.Exit();

        enemyBase.lastTimeAttacked = Time.time;
    }
}
