using UnityEngine;

/// <summary>
/// ������ Hammer ���� ����Ʈ�� ó���ϴ� Ŭ�����Դϴ�.
/// �ִϸ��̼� �̺�Ʈ�� ���� Ȱ��ȭ�Ǹ�, �÷��̾�� �浹 �� ó�� ������ �����մϴ�.
/// </summary>
public class EnemyBossHammerMagic : MonoBehaviour
{
    private EnemyBossHammerAttackTrigger attackTrigger; // Hammer ������ Ʈ�����ϴ� ���� ����

    /// <summary>
    /// Hammer ���� Ʈ���Ÿ� �����մϴ�.
    /// </summary>
    public void SetAttackTrigger(EnemyBossHammerAttackTrigger trigger)
    {
        attackTrigger = trigger;
    }

    // �ִϸ��̼� �̺�Ʈ���� ȣ��Ǿ� ������ �����ϰ� ������Ʈ�� ������
    private void DestroyTrigger()
    {
        attackTrigger.HammerAttackTrigger(); // Hammer ���� Ʈ���� ȣ��
        Destroy(gameObject);
    }
}
