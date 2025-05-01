using UnityEngine;

/// <summary>
/// ���� ���� ���� Ŭ����
/// </summary>
public class EnemyPatrolState : EnemyState
{
    // EnemyPatrolState ������
    public EnemyPatrolState(EnemyObject enemyBase, EnemyStateMachine stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// ���� ���� �� ����
    /// </summary>
    public override void Enter()
    {
        base.Enter();
    }

    /// <summary>
    /// �� ������ ���� ���� ���� ����
    /// </summary>
    public override void Update()
    {
        base.Update();
        Debug.Log("Patrol");

        // ���� �ٶ󺸴� �������� �̵�
        enemyBase.SetVelocity(enemyBase.MoveSpeed * enemyBase.facingDir, rb.linearVelocityY);

        // ���� �ְų� ���� ������ ���� ��ȯ �� ��� ���� ����
        if (enemyBase.IsWallDetected() || !enemyBase.IsGroundDetected())
        {
            enemyBase.Flip();
            enemyBase.CallIdleState();
        }

        // �÷��̾ Ž���Ǿ��� ���� ���θ��� ���� �ʴٸ� �÷��̾� ���� ���·� ��ȯ
        if (enemyBase.IsPlayerDetected() && !enemyBase.IsWallBetweenPlayer())
            enemyBase.EnterPlayerDetection();
    }

    /// <summary>
    /// ���� ���� �� ����
    /// </summary>
    public override void Exit()
    {
        base.Exit();
    }
}
