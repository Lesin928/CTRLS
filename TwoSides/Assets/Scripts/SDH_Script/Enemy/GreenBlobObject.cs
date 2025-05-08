using UnityEngine;

/// <summary>
/// EvilEye �� ������Ʈ�� ���� �� �ൿ�� ������ Ŭ�����Դϴ�.
/// EnemyObject�� ����ϸ�, ���� �ӽ��� ���� Idle, Patrol, Chase, Attack ���¸� �����մϴ�.
/// </summary>
public class GreenBlobObject : EnemyObject
{
    #region State
    public EnemyDissasembleState dissasembleState { get; private set; } // ��� ����
    public EnemyAssembleState assembleState { get; private set; } // ��� ����
    public EnemyChaseState chaseState { get; private set; }   // �߰� ����
    public EnemyAttackState attackState { get; private set; } // ���� ����
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // ���� �ν��Ͻ��� �ʱ�ȭ
        dissasembleState = new EnemyDissasembleState(this, stateMachine, "Dissasemble");
        assembleState = new EnemyAssembleState(this, stateMachine, "Assemble");
        chaseState = new EnemyChaseState(this, stateMachine, "Move");
        attackState = new EnemyAttackState(this, stateMachine, "Attack");
    }

    protected override void Start()
    {
        base.Start();

        // ���� �� �⺻ ���¸� Idle�� ����
        stateMachine.Initialize(dissasembleState);
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
        if(stateMachine.currentState == attackState) // attackState -> ChaseState
            stateMachine.ChangeState(chaseState);
        else // dissasembleState -> assembleState
            stateMachine.ChangeState(assembleState);
    }

    /// <summary>
    /// �÷��̾ Ž������ ���� ��� ���� ���·� ��ȯ�մϴ�.
    /// </summary>
    public override void ExitPlayerDetection()
    {
        if(stateMachine.currentState == hitState)
            stateMachine.ChangeState(chaseState);
        else
            stateMachine.ChangeState(dissasembleState);
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
        stateMachine.ChangeState(dissasembleState);
    }

    /// <summary>
    /// ȣ�� �� �߰� ���·� ��ȯ�մϴ�.
    /// </summary>
    public override void CallChaseState()
    {
        stateMachine.ChangeState(chaseState);
    }
}
