using UnityEngine;

/// <summary>
/// Cthulu 적 오브젝트의 상태 및 행동을 정의한 클래스입니다.
/// EnemyObject를 상속하며, 상태 머신을 통해 Idle, Patrol, Chase, Attack 상태를 관리합니다.
/// </summary>
public class CthuluObject : EnemyObject
{
    #region State
    public EnemyIdleState idleState { get; private set; }     // 대기 상태
    public EnemyPatrolState patrolState { get; private set; } // 순찰 상태
    public EnemyChaseState chaseState { get; private set; }   // 추격 상태
    public EnemyAttackState attackState { get; private set; } // 공격 상태
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // 상태 인스턴스 초기화
        idleState = new EnemyIdleState(this, stateMachine, "Idle");
        patrolState = new EnemyPatrolState(this, stateMachine, "Move");
        chaseState = new EnemyChaseState(this, stateMachine, "Fly");
        attackState = new EnemyAttackState(this, stateMachine, "Attack1");
    }

    protected override void Start()
    {
        base.Start();

        // 시작 시 Idle 상태로 초기화
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// 플레이어가 탐지되었을 때 추격 상태로 전환합니다.
    /// </summary>
    public override void EnterPlayerDetection()
    {
        stateMachine.ChangeState(chaseState);
    }

    /// <summary>
    /// 플레이어를 탐지하지 못할 경우 순찰 상태로 전환합니다.
    /// </summary>
    public override void ExitPlayerDetection()
    {
        stateMachine.ChangeState(patrolState);
    }

    /// <summary>
    /// 호출 시 공격 상태로 전환합니다.
    /// </summary>
    public override void CallAttackState()
    {
        stateMachine.ChangeState(attackState);
    }

    /// <summary>
    /// 호출 시 대기 상태로 전환합니다.
    /// </summary>
    public override void CallIdleState()
    {
        stateMachine.ChangeState(idleState);
    }
}
