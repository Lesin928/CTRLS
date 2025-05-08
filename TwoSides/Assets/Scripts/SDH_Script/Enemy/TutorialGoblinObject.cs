using UnityEngine;

/// <summary>
/// TutorialGoblin �� ������Ʈ�� ���� �� �ൿ�� ������ Ŭ�����Դϴ�.
/// EnemyObject�� ����ϸ�, ���� �ӽ��� ���� Idle, Attack ���¸� �����մϴ�.
/// </summary>
public class TutorialGoblinObject : EnemyObject
{
    #region State
    public EnemyIdleState idleState { get; private set; }     // ��� ����
    public EnemyAttackState attackState { get; private set; } // ���� ����
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // ���� �ν��Ͻ��� �ʱ�ȭ
        idleState = new EnemyIdleState(this, stateMachine, "Idle");
        attackState = new EnemyAttackState(this, stateMachine, "Attack");
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

        float playerX = PlayerManager.instance.player.transform.position.x;

        if (playerX > transform.position.x && !facingRight)
            Flip();
        else if (playerX < transform.position.x && facingRight)
            Flip();

        /*
        // L Ű�� ������ ���� ���·� ��ȯ
        if (Input.GetKeyDown(KeyCode.L))
            CallAttackState();
        */
    }

    /// <summary>
    /// �÷��̾ Ž���Ǿ��� �� �߰� ���·� ��ȯ�մϴ�.
    /// </summary>
    public override void EnterPlayerDetection()
    {
        stateMachine.ChangeState(idleState);
    }

    /// <summary>
    /// �÷��̾ Ž������ ���� ��� ���� ���·� ��ȯ�մϴ�.
    /// </summary>
    public override void ExitPlayerDetection()
    {
        stateMachine.ChangeState(idleState);
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
