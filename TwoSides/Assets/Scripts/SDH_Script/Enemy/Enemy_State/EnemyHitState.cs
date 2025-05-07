using UnityEngine;

/// <summary>
/// ���� �ǰ� ���� Ŭ����
/// </summary>
public class EnemyHitState : EnemyState
{
    public EnemyHitState(EnemyObject enemyBase, EnemyStateMachine stateMachine, string animBoolName) 
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
        stateTimer = 1f;
    }

    /// <summary>
    /// �� ������ ��� ���� ���� ����
    /// </summary>
    public override void Update()
    {
        base.Update();

        if (triggerCalled || stateTimer < 0) // stateTimer < 0 ������ Hit State ���� ���� ������ ����
            enemyBase.ExitPlayerDetection();
    }

    /// <summary>
    /// ���� ���� �� ����
    /// </summary>
    public override void Exit()
    {
        base.Exit();
    }
}