using UnityEngine;

/// <summary>
/// 적의 공격 상태를 관리하는 클래스.
/// </summary>
public class EnemyAttackState : EnemyState
{
    // EnemyAttackState 생성자
    public EnemyAttackState(EnemyObject enemyBase, EnemyStateMachine stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 상태 진입 시 호출됩니다.
    /// </summary>
    public override void Enter()
    {
        base.Enter();
    }

    /// <summary>
    /// 상태에서 매 프레임마다 호출됩니다.
    /// </summary>
    public override void Update()
    {
        Debug.Log("Attack");
        base.Update();

        // 공격 중에는 속도를 0으로 설정하여 멈춤 상태 유지
        enemyBase.SetZeroVelocity();

        // 공격 트리거가 호출되면 플레이어 감지 상태로 전환
        if (triggerCalled)
            enemyBase.EnterPlayerDetection();
    }

    /// <summary>
    /// 상태 종료 시 호출됩니다.
    /// </summary>
    public override void Exit()
    {
        base.Exit();

        // 공격 후 마지막 공격 시간을 갱신
        enemyBase.lastTimeAttacked = Time.time;
    }
}
