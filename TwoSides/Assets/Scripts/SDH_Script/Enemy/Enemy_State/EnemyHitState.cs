using UnityEngine;

/// <summary>
/// 적의 피격 상태 클래스
/// </summary>
public class EnemyHitState : EnemyState
{
    public EnemyHitState(EnemyObject enemyBase, EnemyStateMachine stateMachine, string animBoolName) 
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
            enemyBase.ExitPlayerDetection();
    }

    /// <summary>
    /// 상태 종료 시 실행
    /// </summary>
    public override void Exit()
    {
        base.Exit();
    }
}