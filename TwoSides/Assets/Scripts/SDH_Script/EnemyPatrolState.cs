using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    private Transform player;

    public EnemyPatrolState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName) 
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
    }
    public override void Update()
    {
        base.Update();
        Debug.Log("Patrol");

        enemyBase.SetVelocity(enemyBase.moveSpeed * enemyBase.facingDir, rb.linearVelocityY);

        if (enemyBase.IsWallDetected() || !enemyBase.IsGroundDetected())
        {
            enemyBase.Flip();
            enemyBase.CallIdleState();
        }
            
        if (enemyBase.IsPlayerDetected()) //  || Vector2.Distance(enemyBase.transform.position, player.position) < 3
            enemyBase.EnterPlayerDetection();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
