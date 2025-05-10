using UnityEngine;

/// <summary>
/// ���� ���� ���� Ŭ����
/// </summary>
public class EnemyDeadState : EnemyState
{
    // EnemyDeadState ������
    public EnemyDeadState(EnemyObject enemyBase, EnemyStateMachine stateMachine, string animBoolName) 
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

        //GameManager.Instance.OnMonsterDead();

        if (triggerCalled)
            GameObject.Destroy(enemyBase.gameObject);
    }

    /// <summary>
    /// ���� ���� �� ����
    /// </summary>
    public override void Exit()
    {
        base.Exit();
    }
}
