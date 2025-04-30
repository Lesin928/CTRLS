using UnityEngine;

/// <summary>
/// ���� ��� ���� Ŭ����
/// </summary>
public class EnemyIdleState : EnemyState
{
    // EnemyIdleState ������
    public EnemyIdleState(EnemyObject enemyBase, EnemyStateMachine stateMachine, string animBoolName)
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
        Debug.Log("Idle");

        base.Update();

        if (stateTimer < 0)
        {
            // �÷��̾ Ž���Ǹ� ���� ���� ����
            if (enemyBase.IsPlayerDetected())
                enemyBase.EnterPlayerDetection();
            else
                enemyBase.ExitPlayerDetection();
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
