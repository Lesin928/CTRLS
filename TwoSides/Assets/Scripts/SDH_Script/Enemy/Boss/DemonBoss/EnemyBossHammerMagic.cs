using UnityEngine;

/// <summary>
/// ������ �ظ� ������ ó���ϴ� Ŭ�����Դϴ�.
/// �ظ� ���� Ʈ���Ÿ� ���� �ظ� ���� �� ���� ó���� ����մϴ�.
/// </summary>
public class EnemyBossHammerMagic : MonoBehaviour
{
    private EnemyBossHammerAttackTrigger attackTrigger; // �ظ� ���� Ʈ���Ÿ� ó���ϴ� Ŭ����

    /// <summary>
    /// �ظ� ���� Ʈ���Ÿ� �����մϴ�.
    /// </summary>
    public void SetAttackTrigger(EnemyBossHammerAttackTrigger trigger)
    {
        attackTrigger = trigger;
    }

    // �ظ� ���� Ʈ���� ȣ�� �� ��ü ���� (�ִϸ��̼� �̺�Ʈ���� ȣ���)
    private void DestroyTrigger()
    {
        attackTrigger.HammerAttackTrigger(); // �ظ� ���� Ʈ���Ÿ� ȣ��
        Destroy(gameObject); // ���� ��ü�� �ı�
    }
}
