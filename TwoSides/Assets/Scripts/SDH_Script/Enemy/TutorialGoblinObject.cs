using UnityEngine;

/// <summary>
/// TutorialGoblin 적 오브젝트의 상태 및 행동을 정의한 클래스입니다.
/// EnemyObject를 상속하며, 상태 머신을 통해 Idle, Attack 상태를 관리합니다.
/// </summary>
public class TutorialGoblinObject : EnemyObject
{
    #region State
    public EnemyIdleState idleState { get; private set; }     // 대기 상태
    public EnemyAttackState attackState { get; private set; } // 공격 상태
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // 상태 인스턴스를 초기화
        idleState = new EnemyIdleState(this, stateMachine, "Idle");
        attackState = new EnemyAttackState(this, stateMachine, "Attack");
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

        float playerX = PlayerManager.instance.player.transform.position.x;

        if (playerX > transform.position.x && !facingRight)
            Flip();
        else if (playerX < transform.position.x && facingRight)
            Flip();
    }

    /// <summary>
    /// 플레이어가 탐지되었을 때 추격 상태로 전환합니다.
    /// </summary>
    public override void EnterPlayerDetection()
    {
        stateMachine.ChangeState(idleState);
    }

    /// <summary>
    /// 플레이어를 탐지하지 못할 경우 순찰 상태로 전환합니다.
    /// </summary>
    public override void ExitPlayerDetection()
    {
        stateMachine.ChangeState(idleState);
    }

    /// <summary>
    /// 호출 시 공격 상태로 전환합니다. (튜토리얼에서 호출)
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
