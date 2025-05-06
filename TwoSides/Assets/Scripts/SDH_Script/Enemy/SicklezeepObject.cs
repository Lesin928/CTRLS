using UnityEngine;

/// <summary>
/// Sicklezeep �� ������Ʈ�� ���� �� �ൿ�� ������ Ŭ�����Դϴ�.
/// EnemyObject�� ����ϸ�, ���� �ӽ��� ���� Idle, Patrol, Chase, Attack ���¸� �����մϴ�.
/// </summary>
public class SicklezeepObject : EnemyObject
{
    private int currentAttackIndex = 0;

    #region State
    public EnemyIdleState idleState { get; private set; }     // ��� ����
    public EnemyPatrolState patrolState { get; private set; } // ���� ����
    public EnemyChaseState chaseState { get; private set; }   // �߰� ����
    public EnemyAttackState attack1State { get; private set; } // ���� ����
    public EnemyAttackState attack2State { get; private set; } // ���� ����
    public EnemyAttackState attack3State { get; private set; } // ���� ����
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // ���� �ν��Ͻ��� �ʱ�ȭ
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

        // ���� �� �⺻ ���¸� Idle�� ����
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

        // ���� ���� �ε����� ��ȯ (0 �� 1 �� 2 �� 0 ...)
        currentAttackIndex = (currentAttackIndex + 1) % 3;
    }

    /// <summary>
    /// ȣ�� �� ��� ���·� ��ȯ�մϴ�.
    /// </summary>
    public override void CallIdleState()
    {
        stateMachine.ChangeState(idleState);
    }
}
