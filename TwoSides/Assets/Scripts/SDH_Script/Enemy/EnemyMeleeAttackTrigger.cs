using UnityEngine;

/// <summary>
/// ���� ���� ���� �ִϸ��̼� Ʈ���Ÿ� ó���ϴ� Ŭ�����Դϴ�.
/// ���� ���� ���� �÷��̾ ������ ���� ���� �޽����� ����մϴ�.
/// </summary>
public class EnemyMeleeAttackTrigger : EnemyAnimationTrigger
{
    // ���� ���� ó�� �޼��� (�ִϸ��̼� �̺�Ʈ���� ȣ��)
    private void MeleeAttackTrigger()
    {
        // ���� ���� ���� �ִ� ��� Collider2D ��ü�� ������
        // ��� ��ü�� �������� ����: ���� �ȿ� �����ϴ� Enemy�� ���ԵǱ� ������
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        // ���� ���� ���� ��ü���� ��ȸ
        foreach (var hit in colliders)
        {
            // �÷��̾�� ������ ����
            hit.GetComponent<PlayerObject>()?.TakeDamage(enemy.Attack);
        }
    }
}
