using UnityEngine;

/// <summary>
/// ���� ���¸� �����ϴ� �⺻ Ŭ����
/// </summary>
public class EnemyState
{
    protected EnemyStateMachine stateMachine; // ���� �ӽ� ����
    protected EnemyObject enemyBase;          // �� ������Ʈ �⺻ ����
    protected Rigidbody2D rb;                 // ���� Rigidbody2D ������Ʈ
    protected bool triggerCalled;             // �ִϸ��̼� Ʈ���� ȣ�� ����
    protected float stateTimer;               // ���� ���� �ð� Ÿ�̸�
    private string animBoolName;              // ���� ��ȯ �� ����� �ִϸ��̼� Bool �̸�

    // ���� Ŭ���� ������
    public EnemyState(EnemyObject enemyBase, EnemyStateMachine stateMachine, string animBoolName)
    {
        this.enemyBase = enemyBase;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    /// <summary>
    /// ���� ���� �� ����Ǵ� �Լ�
    /// </summary>
    public virtual void Enter()
    {
        triggerCalled = false;
        rb = enemyBase.rb;
        enemyBase.anim.SetBool(animBoolName, true);
    }

    /// <summary>
    /// �� ������ ���� ������Ʈ
    /// </summary>
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    /// <summary>
    /// ���� ���� �� ����Ǵ� �Լ�
    /// </summary>
    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBoolName, false);
    }

    /// <summary>
    /// �ִϸ��̼� ���� �� ȣ��Ǵ� Ʈ���� �Լ�
    /// </summary>
    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
