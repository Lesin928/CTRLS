using UnityEngine;

/// <summary>
/// Cthulu �� ������Ʈ�� ���� �� �ൿ�� ������ Ŭ�����Դϴ�.
/// EnemyObject�� ����ϸ�, ���� �ӽ��� ���� Idle, Patrol, Chase, Attack ���¸� �����մϴ�.
/// </summary>
public class CthuluObject : EnemyObject
{
    #region State
    public EnemyIdleState idleState { get; private set; }     // ��� ����
    public EnemyPatrolState patrolState { get; private set; } // ���� ����
    public EnemyChaseState chaseState { get; private set; }   // �߰� ����
    public EnemyAttackState attackState { get; private set; } // ���� ����
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // ���� �ν��Ͻ� �ʱ�ȭ
        idleState = new EnemyIdleState(this, stateMachine, "Idle");
        patrolState = new EnemyPatrolState(this, stateMachine, "Move");
        chaseState = new EnemyChaseState(this, stateMachine, "Fly");
        attackState = new EnemyAttackState(this, stateMachine, "Attack1");
    }

    protected override void Start()
    {
        base.Start();

        // ���� �� Idle ���·� �ʱ�ȭ
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// �÷��̾ Ž���Ǿ��� �� �߰� ���·� ��ȯ�մϴ�.
    /// </summary>
    public override void EnterPlayerDetection()
    {
        stateMachine.ChangeState(chaseState);
    }

    /// <summary>
    /// �÷��̾ Ž������ ���� ��� ���� ���·� ��ȯ�մϴ�.
    /// </summary>
    public override void ExitPlayerDetection()
    {
        stateMachine.ChangeState(patrolState);
    }

    /// <summary>
    /// ȣ�� �� ���� ���·� ��ȯ�մϴ�.
    /// </summary>
    public override void CallAttackState()
    {
        stateMachine.ChangeState(attackState);
    }

    /// <summary>
    /// ȣ�� �� ��� ���·� ��ȯ�մϴ�.
    /// </summary>
    public override void CallIdleState()
    {
        stateMachine.ChangeState(idleState);
    }
}
