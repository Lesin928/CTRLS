using Unity.VisualScripting;
using UnityEngine;

// TODO:
// FIXME:
// NOTE: ���� ���� ó�� ��� �����丵 ����

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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        // ���� ���� ���� ��ü���� ��ȸ
        foreach (var hit in colliders)
        {
            // Collider�� PlayerObject ������Ʈ�� ���� ��� ������ ������ ������ ó��
            if (hit.GetComponent<PlayerObject>() != null)
            {
                Debug.Log("���� ����");
            }
        }
    }
}
