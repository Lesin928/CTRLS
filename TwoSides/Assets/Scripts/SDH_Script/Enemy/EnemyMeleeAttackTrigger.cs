using UnityEngine;

/// <summary>
/// ���� ���� ���� Ʈ���Ÿ� ó���ϴ� Ŭ����.
/// ���� ���� ���� ���� �ִ� �÷��̾�� ���ظ� ������.
/// </summary>
public class EnemyMeleeAttackTrigger : EnemyAnimationTrigger
{
    // ���� ���� Ʈ���Ÿ� Ȱ��ȭ�Ͽ� ���� �� �÷��̾�� ���ظ� ������.
    private void MeleeAttackTrigger()
    {
        // ���� üũ ��ġ�� �߽����� ���� �ݰ� ���� �ִ� �ݶ��̴��� ��� ������
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        // ���� ���� ���� �ִ� ��� ��ü�� ���� ó��
        foreach (var hit in colliders)
        {
            // �÷��̾� ��ü�� ���� ���, �׿��� ���ظ� ������.
            hit.GetComponent<PlayerObject>()?.TakeDamage(enemy.Attack);
        }
    }
}
