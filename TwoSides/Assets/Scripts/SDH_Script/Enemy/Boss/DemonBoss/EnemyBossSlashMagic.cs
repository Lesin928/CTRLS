using UnityEngine;

/// <summary>
/// ������ Slash ���� ����Ʈ�� ó���ϴ� Ŭ�����Դϴ�.
/// �ִϸ��̼� �̺�Ʈ�� ���� Ȱ��ȭ�Ǹ�, �÷��̾�� �浹 �� ó�� ������ �����մϴ�.
/// </summary>
public class EnemyBossSlashMagic : MonoBehaviour
{
    private EnemyBossSlashAttackTrigger attackTrigger; // Slash ������ Ʈ�����ϴ� ���� ����

    /// <summary>
    /// Slash ���� Ʈ���Ÿ� �����մϴ�.
    /// </summary>
    public void SetAttackTrigger(EnemyBossSlashAttackTrigger trigger)
    {
        attackTrigger = trigger;
    }

    // �ִϸ��̼� �̺�Ʈ���� ȣ��Ǿ� ������ �����ϰ� ������Ʈ�� ������
    private void DestroyTrigger()
    {
        attackTrigger.SlashAttackTrigger(); // Slash ���� ����
        Destroy(gameObject);
    }
}
