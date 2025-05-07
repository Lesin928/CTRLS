using UnityEngine;

/// <summary>
/// Sicklezeep �� ������Ʈ�� ���� �� �ൿ�� ������ Ŭ�����Դϴ�.
/// EnemyObject�� ����ϸ�, ���� �ӽ��� ���� Idle, Patrol, Chase, Attack ���¸� �����մϴ�.
/// </summary>
public class SicklezeepObject : EnemyObject
{
    [Header("Collider Info")]
    [SerializeField] private Transform dashAttackCheck; // ��� ���� ���� ��ġ
    [SerializeField] Vector2 dashAttackBoxSize; // ���� �ڽ� ũ�� ����

    private int currentAttackIndex = 0;

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

        // ���� �ν��Ͻ��� �ʱ�ȭ
        idleState = new EnemyIdleState(this, stateMachine, "Idle");
        patrolState = new EnemyPatrolState(this, stateMachine, "Move");
        chaseState = new EnemyChaseState(this, stateMachine, "Move");
        attack1State = new EnemyAttackState(this, stateMachine, "Attack1");
        attack2State = new EnemyAttackState(this, stateMachine, "Attack2");
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
        if (IsDashAttackDetected() && Time.time >= lastTimeAttacked + attackCooldown)
        {
            lastTimeAttacked = Time.time;
            CallAttackState();
        }    
        else
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
    /// ȣ�� �� ������ ������ �̵�(���)�� �� ���� ���·� ��ȯ�մϴ�.
    /// </summary>
    public override void CallAttackState()
    {
        // ���ó�� �̵�: �÷��̾� ��ġ �������� stopOffset��ŭ ������ �������� �̵�
        float stopOffset = 1f; // �÷��̾���� �ּ� �Ÿ�
        float rawDistance = PlayerManager.instance.player.transform.position.x - transform.position.x;

        // ������ �ٸ��� Flip
        if (Mathf.Sign(rawDistance) != facingDir)
        {
            Flip();
        }

        // �̵��ؾ� �� �Ÿ� = ��ü �Ÿ� - �ּ� ����
        float dashDistance = Mathf.Abs(rawDistance) - stopOffset;
        dashDistance = Mathf.Max(0, dashDistance); // ���� ����

        // ������ ����� ���� (�¿� �ڵ� ���)
        Vector2 dashVector = new Vector2(facingDir * dashDistance, 0f);
        transform.position += (Vector3)dashVector;

        // ���� ���·� ��ȯ
        switch (currentAttackIndex)
        {
            case 0:
                stateMachine.ChangeState(attack1State);
                break;
            case 1:
                stateMachine.ChangeState(attack2State);
                break;
        }

        // ���� ���� �ε����� ��ȯ
        currentAttackIndex = (currentAttackIndex + 1) % 2;
    }

    /// <summary>
    /// ȣ�� �� ��� ���·� ��ȯ�մϴ�.
    /// </summary>
    public override void CallIdleState()
    {
        stateMachine.ChangeState(idleState);
    }

    /// <summary>
    /// ��� �������� ���� ���� ����
    /// </summary>
    private bool IsDashAttackDetected()
        => Physics2D.OverlapBox(dashAttackCheck.position, dashAttackBoxSize, 0f, whatIsPlayer) != null;

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;

        // �ڽ� �߽� ��ġ�� ũ��
        Vector2 center = dashAttackCheck.position;
        Vector2 size = dashAttackBoxSize;

        // �簢�� ����� �׸���
        Gizmos.DrawWireCube(center, size);
    }
}
