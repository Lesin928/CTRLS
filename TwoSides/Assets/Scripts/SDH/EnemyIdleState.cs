using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private Transform player;

    public EnemyIdleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemyBase.SetZeroVelocity();
        player = PlayerManager.instance.player.transform;
        stateTimer = 0.5f;
    }

    public override void Update()
    {
        Debug.Log("Idle");

        base.Update();

        if (stateTimer < 0)
        {
            if (enemyBase.IsPlayerDetected())
                enemyBase.EnterPlayerDetection();
            else
                enemyBase.ExitPlayerDetection();
        }
            
    }

    public override void Exit()
    {
        base.Exit();
    }
}
