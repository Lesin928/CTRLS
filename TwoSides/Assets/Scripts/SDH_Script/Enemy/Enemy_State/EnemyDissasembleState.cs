using UnityEngine;

/// <summary>
/// ���� ��� ���� Ŭ����
/// </summary>
public class EnemyDissasembleState : EnemyState
{
    // EnemyDissasembleState ������
    public EnemyDissasembleState(EnemyObject enemyBase, EnemyStateMachine stateMachine, string animBoolName)
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
    }

    /// <summary>
    /// �� ������ ��� ���� ���� ����
    /// </summary>
    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            // �÷��̾ Ž���Ǹ� ���� ���� ����
            if (enemyBase.IsPlayerDetected())
                enemyBase.EnterPlayerDetection();
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
