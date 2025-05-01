using UnityEngine;

/// <summary>
/// 적의 순찰 상태 클래스
/// </summary>
public class EnemyPatrolState : EnemyState
{
    // EnemyPatrolState 생성자
    public EnemyPatrolState(EnemyObject enemyBase, EnemyStateMachine stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 상태 진입 시 실행
    /// </summary>
    public override void Enter()
    {
        base.Enter();
    }

    /// <summary>
    /// 매 프레임 순찰 상태 로직 실행
    /// </summary>
    public override void Update()
    {
        base.Update();
        Debug.Log("Patrol");

        // 현재 바라보는 방향으로 이동
        enemyBase.SetVelocity(enemyBase.MoveSpeed * enemyBase.facingDir, rb.linearVelocityY);

        // 벽이 있거나 땅이 없으면 방향 전환 및 대기 상태 진입
        if (enemyBase.IsWallDetected() || !enemyBase.IsGroundDetected())
        {
            enemyBase.Flip();
            enemyBase.CallIdleState();
        }

        // 플레이어가 탐지되었고 벽이 가로막고 있지 않다면 플레이어 감지 상태로 전환
        if (enemyBase.IsPlayerDetected() && !enemyBase.IsWallBetweenPlayer())
            enemyBase.EnterPlayerDetection();
    }

    /// <summary>
    /// 상태 종료 시 실행
    /// </summary>
    public override void Exit()
    {
        base.Exit();
    }
}
