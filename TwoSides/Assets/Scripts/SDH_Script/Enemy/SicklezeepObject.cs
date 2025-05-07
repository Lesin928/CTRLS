using UnityEngine;

/// <summary>
/// Sicklezeep 적 오브젝트의 상태 및 행동을 정의한 클래스입니다.
/// EnemyObject를 상속하며, 상태 머신을 통해 Idle, Patrol, Chase, Attack 상태를 관리합니다.
/// </summary>
public class SicklezeepObject : EnemyObject
{
    private int currentAttackIndex = 0;

    #region State
    public EnemyIdleState idleState { get; private set; }     // 대기 상태
    public EnemyPatrolState patrolState { get; private set; } // 순찰 상태
    public EnemyChaseState chaseState { get; private set; }   // 추격 상태
    public EnemyAttackState attack1State { get; private set; } // 공격 상태
    public EnemyAttackState attack2State { get; private set; } // 공격 상태
    public EnemyAttackState attack3State { get; private set; } // 공격 상태
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // 상태 인스턴스를 초기화
        idleState = new EnemyIdleState(this, stateMachine, "Idle");
        patrolState = new EnemyPatrolState(this, stateMachine, "Move");
        chaseState = new EnemyChaseState(this, stateMachine, "Move");
        attack1State = new EnemyAttackState(this, stateMachine, "Attack1");
        attack2State = new EnemyAttackState(this, stateMachine, "Attack2");
        attack3State = new EnemyAttackState(this, stateMachine, "Attack3");
    }

    protected override void Start()
    {
        base.Start();

        // 시작 시 기본 상태를 Idle로 설정
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
        stateMachine.ChangeState(attack1State);

        switch (currentAttackIndex)
        {
            case 0:
                stateMachine.ChangeState(attack1State);
                break;
            case 1:
                stateMachine.ChangeState(attack2State);
                break;
            case 2:
                stateMachine.ChangeState(attack3State);
                break;
        }

        // 다음 공격 인덱스로 순환 (0 → 1 → 2 → 0 ...)
        currentAttackIndex = (currentAttackIndex + 1) % 3;
    }

    /// <summary>
    /// 호출 시 대기 상태로 전환합니다.
    /// </summary>
    public override void CallIdleState()
    {
        stateMachine.ChangeState(idleState);
    }
}
