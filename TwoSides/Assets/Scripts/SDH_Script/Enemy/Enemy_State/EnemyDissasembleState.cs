using UnityEngine;

/// <summary>
/// 적의 대기 상태 클래스
/// </summary>
public class EnemyDissasembleState : EnemyState
{
    // EnemyDissasembleState 생성자
    public EnemyDissasembleState(EnemyObject enemyBase, EnemyStateMachine stateMachine, string animBoolName)
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
    }

    /// <summary>
    /// 매 프레임 대기 상태 로직 실행
    /// </summary>
    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            // 플레이어가 탐지되면 추적 상태 진입
            if (enemyBase.IsPlayerDetected())
                enemyBase.EnterPlayerDetection();
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
