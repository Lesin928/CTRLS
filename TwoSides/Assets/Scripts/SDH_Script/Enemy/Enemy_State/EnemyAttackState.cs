using UnityEngine;

/// <summary>
/// ���� ���� ���¸� �����ϴ� Ŭ����.
/// </summary>
public class EnemyAttackState : EnemyState
{
    // EnemyAttackState ������
    public EnemyAttackState(EnemyObject enemyBase, EnemyStateMachine stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// ���� ���� �� ȣ��˴ϴ�.
    /// </summary>
    public override void Enter()
    {
        base.Enter();
    }

    /// <summary>
    /// ���¿��� �� �����Ӹ��� ȣ��˴ϴ�.
    /// </summary>
    public override void Update()
    {
        Debug.Log("Attack");
        base.Update();

        // ���� �߿��� �ӵ��� 0���� �����Ͽ� ���� ���� ����
        enemyBase.SetZeroVelocity();

        // ���� Ʈ���Ű� ȣ��Ǹ� �÷��̾� ���� ���·� ��ȯ
        if (triggerCalled)
            enemyBase.EnterPlayerDetection();
    }

    /// <summary>
    /// ���� ���� �� ȣ��˴ϴ�.
    /// </summary>
    public override void Exit()
    {
        base.Exit();

        // ���� �� ������ ���� �ð��� ����
        enemyBase.lastTimeAttacked = Time.time;
    }
}
