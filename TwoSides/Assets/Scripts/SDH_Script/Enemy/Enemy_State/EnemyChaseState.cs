//using UnityEditor.Tilemaps;
using UnityEngine;
//using static UnityEngine.GraphicsBuffer;

/// <summary>
/// 적의 추격 상태를 담당하는 클래스
/// </summary>
public class EnemyChaseState : EnemyState
{
    private Transform player; // 플레이어 위치 정보

    // EnemyChaseState 생성자
    public EnemyChaseState(EnemyObject enemyBase, EnemyStateMachine stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 상태 진입 시 실행되는 함수
    /// </summary>
    public override void Enter()
    {
        player = PlayerManager.instance.player.transform;
        enemyBase.moveSpeed = enemyBase.chaseSpeed;

        // 벽이 사이에 있으면 추적 종료
        if (enemyBase.IsWallBetweenPlayer())
        {
            enemyBase.ExitPlayerDetection();
            return;
        }

        // Y축 거리 차이가 크고 땅이 없으면 추적 포기
        float yDistance = Mathf.Abs(enemyBase.transform.position.y - player.position.y);
        if (!enemyBase.IsGroundDetected() && yDistance > 0.3f)
        {
            enemyBase.CallIdleState();
            return;
        }

        base.Enter();
    }

    /// <summary>
    /// 추격 상태의 매 프레임 처리
    /// </summary>
    public override void Update()
    {
        Debug.Log("Chase");
        base.Update();

        float distance = Vector2.Distance(enemyBase.transform.position, player.position);
        float xDiff = player.position.x - enemyBase.transform.position.x;
        float yDistance = Mathf.Abs(enemyBase.transform.position.y - player.position.y);

        // 땅이 없고 Y축 거리 차이가 크면 대기 상태로 전환
        if (!enemyBase.IsGroundDetected() && yDistance > 0.3f)
        {
            enemyBase.CallIdleState();
            return;
        }

        // 벽이나 땅이 없으면 방향 전환 후 대기
        if (enemyBase.IsWallDetected() || !enemyBase.IsGroundDetected())
        {
            enemyBase.Flip();
            enemyBase.CallIdleState();
            return;
        }
        // 플레이어를 더 이상 감지하지 못하거나 벽 사이에 있을 경우 추적 종료
        else if (!enemyBase.IsPlayerDetected() || enemyBase.IsWallBetweenPlayer())
        {
            enemyBase.ExitPlayerDetection();
        }
        else if (enemyBase.IsPlayerDetected() != null)
        {
            if (distance < enemyBase.attackCheckRadius)
            {
                // 공격 조건 만족 시 공격 상태로 전환
                if (CanAttack() && !enemyBase.IsWallBetweenPlayer())
                {
                    int dir = xDiff > 0 ? 1 : -1;

                    // 방향 다르면 전환
                    if (dir != enemyBase.facingDir)
                        enemyBase.Flip();

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

        // X축 거리 차이가 0.05 이상일 때만 이동
        if (Mathf.Abs(xDiff) > 0.05f)
        {
            int moveDir = xDiff > 0 ? 1 : -1; // 이동 방향
            enemyBase.SetVelocity(enemyBase.moveSpeed * moveDir, rb.linearVelocityY);
        }
    }

    /// <summary>
    /// 상태 종료 시 실행
    /// </summary>
    public override void Exit()
    {
        base.Exit();
        enemyBase.moveSpeed = enemyBase.defaultMoveSpeed;
    }

    // 공격 가능 여부 판단
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
