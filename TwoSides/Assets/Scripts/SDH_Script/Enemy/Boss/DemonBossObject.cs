using UnityEngine;

/// <summary>
/// Cthulu �� ������Ʈ�� ���� �� �ൿ�� ������ Ŭ�����Դϴ�.
/// EnemyObject�� ����ϸ�, ���� �ӽ��� ���� Idle, Patrol, Chase, Attack ���¸� �����մϴ�.
/// </summary>
public class DemonBossObject : EnemyObject
{
    #region State
    public EnemyIdleState idleState { get; private set; }     // ��� ����
    public EnemyPatrolState patrolState { get; private set; } // ���� ����
    public EnemyChaseState chaseState { get; private set; }   // �߰� ����
    public EnemyAttackState attack1State { get; private set; } // ���� ����
    public EnemyAttackState attack2State { get; private set; } // ���� ����
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // ���� �ν��Ͻ� �ʱ�ȭ
        idleState = new EnemyIdleState(this, stateMachine, "Idle");
        patrolState = new EnemyPatrolState(this, stateMachine, "Move");
        chaseState = new EnemyChaseState(this, stateMachine, "Move");
        attack1State = new EnemyAttackState(this, stateMachine, "Attack1");
        attack2State = new EnemyAttackState(this, stateMachine, "Attack2");
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
    /// ���� ������ �������� ȣ���մϴ�.
    /// </summary>
    public override void CallAttackState()
    {
        float rand = Random.value; // 0.0f ~ 1.0f ������ float

        if (rand < 0.7f)
        {
            stateMachine.ChangeState(attack1State); // 70%
        }
        else
        {
            stateMachine.ChangeState(attack2State); // 30%
        }
    }

    /// <summary>
    /// ȣ�� �� ��� ���·� ��ȯ�մϴ�.
    /// </summary>
    public override void CallIdleState()
    {
        stateMachine.ChangeState(idleState);
    }
}
