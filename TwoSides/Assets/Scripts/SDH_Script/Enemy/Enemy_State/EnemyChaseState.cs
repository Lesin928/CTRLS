//using UnityEditor.Tilemaps;
using UnityEngine;
//using static UnityEngine.GraphicsBuffer;

/// <summary>
/// ���� �߰� ���¸� ����ϴ� Ŭ����
/// </summary>
public class EnemyChaseState : EnemyState
{
    private Transform player; // �÷��̾� ��ġ ����

    // EnemyChaseState ������
    public EnemyChaseState(EnemyObject enemyBase, EnemyStateMachine stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// ���� ���� �� ����Ǵ� �Լ�
    /// </summary>
    public override void Enter()
    {
        player = PlayerManager.instance.player.transform;
        enemyBase.moveSpeed = enemyBase.chaseSpeed;

        // ���� ���̿� ������ ���� ����
        if (enemyBase.IsWallBetweenPlayer())
        {
            enemyBase.ExitPlayerDetection();
            return;
        }

        // Y�� �Ÿ� ���̰� ũ�� ���� ������ ���� ����
        float yDistance = Mathf.Abs(enemyBase.transform.position.y - player.position.y);
        if (!enemyBase.IsGroundDetected() && yDistance > 0.3f)
        {
            enemyBase.CallIdleState();
            return;
        }

        base.Enter();
    }

    /// <summary>
    /// �߰� ������ �� ������ ó��
    /// </summary>
    public override void Update()
    {
        Debug.Log("Chase");
        base.Update();

        float distance = Vector2.Distance(enemyBase.transform.position, player.position);
        float xDiff = player.position.x - enemyBase.transform.position.x;
        float yDistance = Mathf.Abs(enemyBase.transform.position.y - player.position.y);

        // ���� ���� Y�� �Ÿ� ���̰� ũ�� ��� ���·� ��ȯ
        if (!enemyBase.IsGroundDetected() && yDistance > 0.3f)
        {
            enemyBase.CallIdleState();
            return;
        }

        // ���̳� ���� ������ ���� ��ȯ �� ���
        if (enemyBase.IsWallDetected() || !enemyBase.IsGroundDetected())
        {
            enemyBase.Flip();
            enemyBase.CallIdleState();
            return;
        }
        // �÷��̾ �� �̻� �������� ���ϰų� �� ���̿� ���� ��� ���� ����
        else if (!enemyBase.IsPlayerDetected() || enemyBase.IsWallBetweenPlayer())
        {
            enemyBase.ExitPlayerDetection();
        }
        else if (enemyBase.IsPlayerDetected() != null)
        {
            if (distance < enemyBase.attackCheckRadius)
            {
                // ���� ���� ���� �� ���� ���·� ��ȯ
                if (CanAttack() && !enemyBase.IsWallBetweenPlayer())
                {
                    int dir = xDiff > 0 ? 1 : -1;

                    // ���� �ٸ��� ��ȯ
                    if (dir != enemyBase.facingDir)
                        enemyBase.Flip();

                    enemyBase.CallAttackState();
                    return;
                }
            }
        }
        else
        {
            enemyBase.ExitPlayerDetection();
            return;
        }

        // X�� �Ÿ� ���̰� 0.05 �̻��� ���� �̵�
        if (Mathf.Abs(xDiff) > 0.05f)
        {
            int moveDir = xDiff > 0 ? 1 : -1; // �̵� ����
            enemyBase.SetVelocity(enemyBase.moveSpeed * moveDir, rb.linearVelocityY);
        }
    }

    /// <summary>
    /// ���� ���� �� ����
    /// </summary>
    public override void Exit()
    {
        base.Exit();
        enemyBase.moveSpeed = enemyBase.defaultMoveSpeed;
    }

    // ���� ���� ���� �Ǵ�
    private bool CanAttack()
    {
        if (Time.time >= enemyBase.lastTimeAttacked + enemyBase.attackCooldown)
        {
            enemyBase.lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }
}
