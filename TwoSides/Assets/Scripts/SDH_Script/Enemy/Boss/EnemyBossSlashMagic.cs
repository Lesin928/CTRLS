using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// ���� Slash ���� ����Ʈ�� ó���ϴ� Ŭ�����Դϴ�.
/// �ִϸ��̼� �̺�Ʈ�� ���� Ȱ��ȭ�Ǹ�, �÷��̾�� �浹 �� ó�� ������ �����մϴ�.
/// </summary>
public class EnemyBossSlashMagic : MonoBehaviour
{
    private EnemyBossSlashAttackTrigger attackTrigger;

    public void SetAttackTrigger(EnemyBossSlashAttackTrigger trigger)
    {
        attackTrigger = trigger;
    }

    // �ִϸ��̼� �̺�Ʈ���� ȣ��Ǿ� ����Ʈ ������Ʈ�� �ı�
    private void DestroyTrigger()
    {
        // ������ ���� Ʈ���� ȣ��
        attackTrigger.SlashAttackTrigger();
        Destroy(gameObject);
    }
}
