using UnityEngine;

/// <summary>
/// ���� ��� ���� Ŭ����
/// </summary>
public class EnemyAssembleState : EnemyState
{
    // EnemyAssembleState ������
    public EnemyAssembleState(EnemyObject enemyBase, EnemyStateMachine stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// ���� ���� �� ����
    /// </summary>
    public override void Enter()
    {
        base.Enter();

        // ���� ���·� ��ȯ
        enemyBase.SetZeroVelocity();

        // ��� �ð� ����
        stateTimer = 0.5f;
    }

    /// <summary>
    /// �� ������ ��� ���� ���� ����
    /// </summary>
    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            enemyBase.CallChaseState();
        }
    }

    /// <summary>
    /// ���� ���� �� ����
    /// </summary>
    public override void Exit()
    {
        base.Exit();
    }
}
