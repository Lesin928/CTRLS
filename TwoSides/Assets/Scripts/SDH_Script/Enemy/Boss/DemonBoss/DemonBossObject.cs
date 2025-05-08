using UnityEngine;

/// <summary>
/// DemonBoss ������Ʈ�� ���� �� �ൿ�� �����ϴ� Ŭ�����Դϴ�.
/// EnemyObject�� ����ϸ�, �پ��� ���� (Idle, Patrol, Chase, Attack)�� ó���մϴ�.
/// </summary>
public class DemonBossObject : EnemyObject
{
    [Header("Attack Check Transform")]
    [SerializeField] private Transform closeAttackCheck; // �ٰŸ� ���� ���� ���� ��ġ
    [SerializeField] private Transform midAttackCheck;   // �߰Ÿ� ���� ���� ���� ��ġ
    [SerializeField] private Transform longAttackCheck;  // ���Ÿ� ���� ���� ���� ��ġ

    [Header("Attack Check Radius")]
    [SerializeField] Vector2 closeAttackCheckSize; // �ٰŸ� ���� ���� ũ��
    [SerializeField] Vector2 midAttackCheckSize;   // �߰Ÿ� ���� ���� ũ��
    [SerializeField] Vector2 longAttackCheckSize;  // ���Ÿ� ���� ���� ũ��

    #region State
    public EnemyIdleState idleState { get; private set; }      // ��� ����
    public EnemyPatrolState patrolState { get; private set; }  // ���� ����
    public EnemyChaseState chaseState { get; private set; }    // �߰� ����
    public EnemyAttackState attack1State { get; private set; } // ����1 ����
    public EnemyAttackState attack2State { get; private set; } // ����2 ����
    public EnemyAttackState attack3State { get; private set; } // ����3 ����
    public EnemyAttackState attack4State { get; private set; } // ����4 ����
    public EnemyAttackState attack5State { get; private set; } // ����5 ����
    public EnemyAttackState attack6State { get; private set; } // ����6 ����
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // ���� ��ü ���� �� �ʱ�ȭ
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
    /// SlimeBoss -> DemonBoss ��ȯ �� ������ �ʱ�ȭ�մϴ�.
    /// </summary>
    /// <param name="direction">SlimeBoss�� �ٶ󺸴� ����</param>
    public void InitFacingDir(int direction)
    {
        if (facingDir != direction)
        {
            Flip(); // ���� ����
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
    /// �÷��̾ Ž�� �������� ����� �� ���� ���·� ��ȯ�մϴ�.
    /// </summary>
    public override void ExitPlayerDetection()
    {
        stateMachine.ChangeState(patrolState);
    }

    /// <summary>
    /// ȣ�� �� ������ ���� ���·� ��ȯ�մϴ�.
    /// ���� �Ÿ��� ���� �پ��� ���� ������ ���õ˴ϴ�.
    /// </summary>
    public override void CallAttackState()
    {
        float distanceX = Mathf.Abs(transform.position.x - PlayerManager.instance.player.transform.position.x);
        int rand;

        if (closeRangeDetected())
        {
            stateMachine.ChangeState(attack4State); // �ٰŸ� ���� ����
        }
        else if (midRangeDetected())
        {
            // �߰Ÿ�: �پ��� ���� �� ������ ���� (0~4)
            rand = Random.Range(0, 5); // 0, 1, 2, 3, 4
            switch (rand)
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
                case 3:
                    stateMachine.ChangeState(attack5State);
                    break;
                case 4:
                    stateMachine.ChangeState(attack6State);
                    break;
            }
        }
        else if (longRangeDetected())
        {
            // ���Ÿ�: ���� 2, 5, 6 �� ���� ���� (0~2)
            rand = Random.Range(0, 3); // 0, 1, 2
            switch (rand)
            {
                case 0:
                    stateMachine.ChangeState(attack2State);
                    break;
                case 1:
                    stateMachine.ChangeState(attack5State);
                    break;
                case 2:
                    stateMachine.ChangeState(attack6State);
                    break;
            }
        }
    }


    /// <summary>
    /// ȣ�� �� ��� ���·� ��ȯ�մϴ�.
    /// </summary>
    public override void CallIdleState()
    {
        stateMachine.ChangeState(idleState);
    }

    // �ٰŸ� ���� ���� ���� �÷��̾ �ִ��� Ȯ��
    public Collider2D closeRangeDetected()
        => Physics2D.OverlapBox(closeAttackCheck.position, closeAttackCheckSize, 0f, whatIsPlayer);

    // �߰Ÿ� ���� ���� ���� �÷��̾ �ִ��� Ȯ��
    public Collider2D midRangeDetected()
        => Physics2D.OverlapBox(midAttackCheck.position, midAttackCheckSize, 0f, whatIsPlayer);

    // ���Ÿ� ���� ���� ���� �÷��̾ �ִ��� Ȯ��
    public Collider2D longRangeDetected()
        => Physics2D.OverlapBox(longAttackCheck.position, longAttackCheckSize, 0f, whatIsPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        // ���� ������ Scene �信 �ð������� ǥ��
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(closeAttackCheck.position, closeAttackCheckSize);
        Gizmos.DrawWireCube(midAttackCheck.position, midAttackCheckSize);
        Gizmos.DrawWireCube(longAttackCheck.position, longAttackCheckSize);
    }
}
