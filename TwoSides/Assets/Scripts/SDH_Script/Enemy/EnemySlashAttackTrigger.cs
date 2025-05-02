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

    private void Slash1AttackTrigger()
    {
        // �θ� EnemyObject�� ������
        EnemyObject enemy = GetComponentInParent<EnemyObject>();

        // Slash �������� �߻� ������ ����
        GameObject slash = Instantiate(slash1Prefab, firePoint.position, Quaternion.identity);

        // Slash ��ü�� ��ũ��Ʈ�� �����ͼ� Ȱ��ȭ
        EnemySlash slashScript = slash.GetComponent<EnemySlash>();
        slashScript.SetAttacker(enemy); // �߻��� ����
        slashScript.Active(enemy.facingDir);
    }

    private void Slash2AttackTrigger()
    {
        // �θ� EnemyObject�� ������
        EnemyObject enemy = GetComponentInParent<EnemyObject>();

        // Slash �������� �߻� ������ ����
        GameObject slash = Instantiate(slash2Prefab, firePoint.position, Quaternion.identity);

        // Slash ��ü�� ��ũ��Ʈ�� �����ͼ� Ȱ��ȭ
        EnemySlash slashScript = slash.GetComponent<EnemySlash>();
        slashScript.SetAttacker(enemy); // �߻��� ����
        slashScript.Active(enemy.facingDir);
    }
}
