using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyChaseState : EnemyState
{
    private Transform player;
    private int moveDir;
    
    
    public EnemyChaseState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
        enemyBase.moveSpeed = enemyBase.chaseSpeed;
    }

    public override void Update()
    {
        Debug.Log("Chase");
        base.Update();        

        if (enemyBase.IsPlayerDetected() != null)
        {
            float distance = Vector2.Distance(enemyBase.transform.position, player.transform.position);
            if (distance < enemyBase.attackDistance)
            {
                // 공격 상태
                if (CanAttack())
                    enemyBase.CallAttackState();
            }
        }
        else
        {
            if (enemyBase.IsWallDetected() || !enemyBase.IsGroundDetected())
            {
                enemyBase.Flip();
                enemyBase.CallIdleState();
                return;
            }

            if (Vector2.Distance(player.transform.position, enemyBase.transform.position) > 3)
                enemyBase.ExitPlayerDetection();
        }

        float xDiff = player.position.x - enemyBase.transform.position.x;
        if (Mathf.Abs(xDiff) > 0.05f) // 거리 차이 0.05 이상일 때만 방향 계산
        {
            moveDir = xDiff > 0 ? 1 : -1;
            enemyBase.SetVelocity(enemyBase.moveSpeed * moveDir, rb.linearVelocityY);
        }
        else
        {
            // 너무 가까우면 좌우 움직이지 않음
            enemyBase.CallIdleState();
        }


        //enemyBase.SetVelocity(enemyBase.moveSpeed * moveDir, rb.linearVelocityY);
    }

    public override void Exit()
    {
        base.Exit();
        enemyBase.moveSpeed = enemyBase.defaultmoveSpeed;
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
