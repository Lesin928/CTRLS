using UnityEngine;

/// <summary>
/// DemonBoss ������Ʈ�� ���� �� �ൿ�� ������ Ŭ�����Դϴ�.
/// EnemyObject�� ����ϸ�, ���� �ӽ��� ���� Idle, Patrol, Chase, Attack ���¸� �����մϴ�.
/// </summary>
public class DemonBossObject : EnemyObject
{
    [Header("Attack Check Transform")]
    [SerializeField] private Transform closeAttackCheck;
    [SerializeField] private Transform midAttackCheck;
    [SerializeField] private Transform longAttackCheck;

    [Header("Attack Check Radius")]
    [SerializeField] Vector2 closeAttackCheckSize;
    [SerializeField] Vector2 midAttackCheckSize;
    [SerializeField] Vector2 longAttackCheckSize;

    #region State
    public EnemyIdleState idleState { get; private set; }     // ��� ����
    public EnemyPatrolState patrolState { get; private set; } // ���� ����
    public EnemyChaseState chaseState { get; private set; }   // �߰� ����
    public EnemyAttackState attack1State { get; private set; } // ���� ����
    public EnemyAttackState attack2State { get; private set; } // ���� ����
    public EnemyAttackState attack3State { get; private set; } // ���� ����
    public EnemyAttackState attack4State { get; private set; } // ���� ����
    public EnemyAttackState attack5State { get; private set; } // ���� ����
    public EnemyAttackState attack6State { get; private set; } // ���� ����
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
        attack3State = new EnemyAttackState(this, stateMachine, "Attack3");
        attack4State = new EnemyAttackState(this, stateMachine, "Attack4");
        attack5State = new EnemyAttackState(this, stateMachine, "Attack5");
        attack6State = new EnemyAttackState(this, stateMachine, "Attack6");
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
    /// SlimeBoss -> DemonBoss ��ȯ�� ���� �ʱ�ȭ�մϴ�.
    /// </summary>
    /// <param name="direction">SlimeBoss�� ���Ⱚ</param>
    public void InitFacingDir(int direction)
    {
        if (facingDir != direction)
        {
            Flip(); // ������ �ٸ��� ������
        }
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
        float distanceX = Mathf.Abs(transform.position.x - PlayerManager.instance.player.transform.position.x);
        float rand = Random.value; // 0.0f ~ 1.0f ������ float

        if (closeRangeDetected())
            stateMachine.ChangeState(attack4State);
        else if (midRangeDetected())
        {
            if (rand < 0.20f)
                stateMachine.ChangeState(attack1State);
            else if (rand < 0.40f)
                stateMachine.ChangeState(attack2State);
            else if (rand < 0.60f)
                stateMachine.ChangeState(attack3State);
            else if (rand < 0.80f)
                stateMachine.ChangeState(attack5State);
            else
                stateMachine.ChangeState(attack6State);
        }
        else if (longRangeDetected())
        {
            if (rand < 0.33f)
                stateMachine.ChangeState(attack2State);
            else if (rand < 0.66f)
                stateMachine.ChangeState(attack5State);
            else
                stateMachine.ChangeState(attack6State);
        }
    }

    /// <summary>
    /// ȣ�� �� ��� ���·� ��ȯ�մϴ�.
    /// </summary>
    public override void CallIdleState()
    {
        stateMachine.ChangeState(idleState);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(closeAttackCheck.position, closeAttackCheckSize);
        Gizmos.DrawWireCube(midAttackCheck.position, midAttackCheckSize);
        Gizmos.DrawWireCube(longAttackCheck.position, longAttackCheckSize);
    }

    public Collider2D closeRangeDetected()
        => Physics2D.OverlapBox(closeAttackCheck.position, closeAttackCheckSize, 0f, whatIsPlayer);

    public Collider2D midRangeDetected()
        => Physics2D.OverlapBox(midAttackCheck.position, midAttackCheckSize, 0f, whatIsPlayer);

    public Collider2D longRangeDetected()
        => Physics2D.OverlapBox(longAttackCheck.position, longAttackCheckSize, 0f, whatIsPlayer);

}
