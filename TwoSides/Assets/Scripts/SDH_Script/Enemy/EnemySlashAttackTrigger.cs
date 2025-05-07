using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ���� Slash ���� �ִϸ��̼� Ʈ���Ÿ� ó���ϴ� Ŭ�����Դϴ�.
/// ���� ���� ���� �÷��̾ ������ ���� ���� �޽����� ����մϴ�.
/// </summary>
public class EnemySlashAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject slash1Prefab; // Slash1 ���� ������
    [SerializeField] private GameObject slash2Prefab; // Slash2 ���� ������
    [SerializeField] private Transform firePoint;     // ���� �߻� ��ġ

    // Slash ������ �����ϰ� ���� �� �����ڸ� �����ϴ� ���� �Լ�
    private void TriggerSlashAttack(GameObject slashPrefab)
    {
        // �θ� ������Ʈ���� EnemyObject ������Ʈ�� ������
        EnemyObject enemy = GetComponentInParent<EnemyObject>();

        // Slash �������� �߻� ��ġ�� ����
        GameObject slash = Instantiate(slashPrefab, firePoint.position, Quaternion.identity);

        // ������ Slash ������Ʈ�� �����ڿ� ���� ����
        EnemySlash slashScript = slash.GetComponent<EnemySlash>();
        slashScript.SetAttacker(enemy);
        slashScript.SetDirection(enemy.facingDir);
    }

    // Slash1 ������ �����ϴ� �ִϸ��̼� �̺�Ʈ�� �Լ�
    private void Slash1AttackTrigger()
    {
        TriggerSlashAttack(slash1Prefab);
    }

    // Slash2 ������ �����ϴ� �ִϸ��̼� �̺�Ʈ�� �Լ�
    private void Slash2AttackTrigger()
    {
        TriggerSlashAttack(slash2Prefab);
    }
}
