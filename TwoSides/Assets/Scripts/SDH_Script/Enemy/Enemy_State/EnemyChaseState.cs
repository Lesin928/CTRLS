//using UnityEditor.Tilemaps;
using UnityEngine;
//using static UnityEngine.GraphicsBuffer;

public class EnemyChaseState : EnemyState
{
    private Transform player;
    private int moveDir;

    public EnemyChaseState(EnemyObject enemyBase, EnemyStateMachine stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        player = PlayerManager.instance.player.transform;
        enemyBase.moveSpeed = enemyBase.chaseSpeed;

        if (enemyBase.IsWallBetweenPlayer())
        {
            enemyBase.ExitPlayerDetection();
            return;
        }

        float yDistance = Mathf.Abs(enemyBase.transform.position.y - player.transform.position.y);
        if (!enemyBase.IsGroundDetected() && yDistance > 0.3)
        {
            enemyBase.CallIdleState();
            return;
        }

        base.Enter();
    }

    public override void Update()
    {
        Debug.Log("Chase");
        base.Update();

        float distance = Vector2.Distance(enemyBase.transform.position, player.transform.position);
        float xDiff = player.position.x - enemyBase.transform.position.x;

        float yDistance = Mathf.Abs(enemyBase.transform.position.y - player.transform.position.y);
        if (!enemyBase.IsGroundDetected() && yDistance > 0.3)
        {
            enemyBase.CallIdleState();
            return;
        }

        if (enemyBase.IsWallDetected() || !enemyBase.IsGroundDetected())
        {
            enemyBase.Flip();
            enemyBase.CallIdleState();
            return;
        }
        else if (!enemyBase.IsPlayerDetected() || enemyBase.IsWallBetweenPlayer())
        {
            enemyBase.ExitPlayerDetection();
        }
        else if (enemyBase.IsPlayerDetected() != null)
        {
            if (distance < enemyBase.attackCheckRadius)
            {
                // 공격 상태
                if (CanAttack() && !enemyBase.IsWallBetweenPlayer())
                {
                    int dir = xDiff > 0 ? 1 : -1;

                    // 플레이어가 바라보는 방향이랑 다르면 Flip
                    if (dir != enemyBase.facingDir)
                    {
                        enemyBase.Flip();
                    }

                    enemyBase.CallAttackState();
                    return;
                }
            }
        }
        else
        {
            enemyBase.ExitPlayerDetection();
            return;
        }

        if (Mathf.Abs(xDiff) > 0.05f) // 거리 차이 0.05 이상일 때만 방향 계산
        {
            moveDir = xDiff > 0 ? 1 : -1;
            enemyBase.SetVelocity(enemyBase.moveSpeed * moveDir, rb.linearVelocityY);
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemyBase.moveSpeed = enemyBase.defaultMoveSpeed;
    }

    private bool CanAttack()
    {
        if (Time.time >= enemyBase.lastTimeAttacked + enemyBase.attackCooldown)
        {
            enemyBase.lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }
}
