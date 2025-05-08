using UnityEngine;

/// <summary>
/// ������ ������ ������ ���õ� ó���� ����ϴ� Ŭ�����Դϴ�.
/// ������ ���� Ʈ���Ÿ� ���� ������ ���� �� ���� ó���� ����մϴ�.
/// </summary>
public class EnemyBossSlashMagic : MonoBehaviour
{
    private EnemyBossSlashAttackTrigger attackTrigger; // ������ ���� Ʈ���ſ� �����Ǵ� ����

    /// <summary>
    /// ������ ���� Ʈ���Ÿ� �����ϴ� �Լ��Դϴ�.
    /// </summary>
    public void SetAttackTrigger(EnemyBossSlashAttackTrigger trigger)
    {
        attackTrigger = trigger; // �־��� Ʈ���Ÿ� attackTrigger ������ �Ҵ�
    }

    // ������ ������ �ļ� ó���� �����ϴ� �Լ��Դϴ�. 
    // ������ ������ ������ ���� Ʈ���Ÿ� ȣ���ϰ�, �ش� ��ü�� �����մϴ�.
    private void DestroyTrigger()
    {
        attackTrigger.SlashAttackTrigger(); // ������ ������ Ʈ����
        Destroy(gameObject); // ���� ���� ������Ʈ�� ����
    }
}
