using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// ���� Slash ���� ����Ʈ�� ó���ϴ� Ŭ�����Դϴ�.
/// �ִϸ��̼� �̺�Ʈ�� ���� Ȱ��ȭ�Ǹ�, �÷��̾�� �浹 �� ó�� ������ �����մϴ�.
/// </summary>
public class EnemyBossHammerMagic : MonoBehaviour
{
    private EnemyBossHammerAttackTrigger attackTrigger;

    public void SetAttackTrigger(EnemyBossHammerAttackTrigger trigger)
    {
        attackTrigger = trigger;
    }

    // �ִϸ��̼� �̺�Ʈ���� ȣ��Ǿ� ����Ʈ ������Ʈ�� �ı�
    private void DestroyTrigger()
    {
        // �ظ� ���� Ʈ���� ȣ��
        attackTrigger.HammerAttackTrigger();
        Destroy(gameObject);
    }
}
