using UnityEngine;

/// <summary>
/// EvilEye 적 오브젝트의 상태 및 행동을 정의한 클래스입니다.
/// EnemyObject를 상속하며, 상태 머신을 통해 Idle, Patrol, Chase, Attack 상태를 관리합니다.
/// </summary>
public class GreenBlobObject : EnemyObject
{
    #region State
    public EnemyDissasembleState dissasembleState { get; private set; } // 대기 상태
    public EnemyAssembleState assembleState { get; private set; } // 대기 상태
    public EnemyChaseState chaseState { get; private set; }   // 추격 상태
    public EnemyAttackState attackState { get; private set; } // 공격 상태
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // 상태 인스턴스를 초기화
        dissasembleState = new EnemyDissasembleState(this, stateMachine, "Dissasemble");
        assembleState = new EnemyAssembleState(this, stateMachine, "Assemble");
        chaseState = new EnemyChaseState(this, stateMachine, "Move");
        attackState = new EnemyAttackState(this, stateMachine, "Attack");
    }

    protected override void Start()
    {
        base.Start();

        // 시작 시 기본 상태를 Idle로 설정
        stateMachine.Initialize(dissasembleState);
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
        if(stateMachine.currentState == attackState) // attackState -> ChaseState
            stateMachine.ChangeState(chaseState);
        else // dissasembleState -> assembleState
            stateMachine.ChangeState(assembleState);
    }

    /// <summary>
    /// 플레이어를 탐지하지 못할 경우 순찰 상태로 전환합니다.
    /// </summary>
    public override void ExitPlayerDetection()
    {
        if(stateMachine.currentState == hitState)
            stateMachine.ChangeState(chaseState);
        else
            stateMachine.ChangeState(dissasembleState);
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
        stateMachine.ChangeState(dissasembleState);
    }

    /// <summary>
    /// 호출 시 추격 상태로 전환합니다.
    /// </summary>
    public override void CallChaseState()
    {
        stateMachine.ChangeState(chaseState);
    }
}
