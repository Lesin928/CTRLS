using UnityEngine;

/// <summary>
/// 적의 대기 상태 클래스
/// </summary>
public class EnemyIdleState : EnemyState
{
    // EnemyIdleState 생성자
    public EnemyIdleState(EnemyObject enemyBase, EnemyStateMachine stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 상태 진입 시 실행
    /// </summary>
    public override void Enter()
    {
        base.Enter();

        // 정지 상태로 전환
        enemyBase.SetZeroVelocity();

        // 대기 시간 설정
        stateTimer = 0.5f;
    }

    /// <summary>
    /// 매 프레임 대기 상태 로직 실행
    /// </summary>
    public override void Update()
    {
        Debug.Log("Idle");

        base.Update();

        if (stateTimer < 0)
        {
            // 플레이어가 탐지되면 추적 상태 진입
            if (enemyBase.IsPlayerDetected())
                enemyBase.EnterPlayerDetection();
            else
                enemyBase.ExitPlayerDetection();
        }
    }

    /// <summary>
    /// 상태 종료 시 실행
    /// </summary>
    public override void Exit()
    {
        base.Exit();
    }
}
