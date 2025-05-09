using UnityEngine;

/// <summary>
/// 적의 대기 상태 클래스
/// </summary>
public class EnemyAssembleState : EnemyState
{
    // EnemyAssembleState 생성자
    public EnemyAssembleState(EnemyObject enemyBase, EnemyStateMachine stateMachine, string animBoolName)
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
        base.Update();

        if (triggerCalled)
        {
            enemyBase.CallChaseState();
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
