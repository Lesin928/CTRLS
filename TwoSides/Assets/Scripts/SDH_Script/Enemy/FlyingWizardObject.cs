using UnityEngine;

public class FlyingWizardObject : EnemyObject
{
    #region State
    public EnemyIdleState idleState { get; private set; }
    public EnemyPatrolState patrolState { get; private set; }
    public EnemyChaseState chaseState { get; private set; }
    public EnemyAttackState attackState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new EnemyIdleState(this, stateMachine, "Idle");
        patrolState = new EnemyPatrolState(this, stateMachine, "Idle");
        chaseState = new EnemyChaseState(this, stateMachine, "Move");
        attackState = new EnemyAttackState(this, stateMachine, "Attack");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void EnterPlayerDetection()
    {
        stateMachine.ChangeState(chaseState);
    }

    public override void ExitPlayerDetection()
    {
        stateMachine.ChangeState(patrolState);
    }

    public override void CallAttackState()
    {
        stateMachine.ChangeState(attackState);
    }

    public override void CallIdleState()
    {
        stateMachine.ChangeState(idleState);
    }
}

