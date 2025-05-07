using UnityEngine;

/// <summary>
/// ���� Slash ���� �ִϸ��̼� Ʈ���Ÿ� ó���ϴ� Ŭ�����Դϴ�.
/// ���� ���� ���� �÷��̾ ������ ���� ���� �޽����� ����մϴ�.
/// </summary>
public class EnemyBossSlashAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject slashPrefab; // Slash ������
    [SerializeField] private GameObject magicPrefab; // Magic ������
    [SerializeField] private Transform firePoint;    // ���� �߻� ��ġ
    [SerializeField] private Transform handPoint;    // �� ��ġ (Magic Ȱ��ȭ)

    // SlashMagic ������Ʈ(������)�� ���� (�ִϸ��̼� �̺�Ʈ���� ȣ��)
    private void SlashMagicTrigger()
    {
        // SlashMagic(������) �������� handPoint�� ����
        GameObject magic = Instantiate(magicPrefab, handPoint.position, Quaternion.identity);

        EnemyBossSlashMagic magicScript = magic.GetComponent<EnemyBossSlashMagic>();
        magicScript.SetAttackTrigger(this);
    }

    /// <summary>
    /// ���� Slash�� �߻��ϴ� Ʈ�����Դϴ�.
    /// EnemyBossSlashMagic DestroyTrigger()���� ȣ��˴ϴ�.
    /// </summary>
    public void SlashAttackTrigger()
    {
        // �θ� EnemyObject�� ������
        EnemyObject enemy = GetComponentInParent<EnemyObject>();

        // Slash �������� �߻� ������ ����
        GameObject slash = Instantiate(slashPrefab, firePoint.position, Quaternion.identity);

        // Slash ��ü�� ��ũ��Ʈ�� �����ͼ� Ȱ��ȭ
        EnemyBossSlash slashScript = slash.GetComponent<EnemyBossSlash>();
        slashScript.SetAttacker(enemy); // �߻��� ����
        slashScript.SetDirection(enemy.facingDir);
    }

    private void SlashAnimationTrigger()
    {
        // ���� �ִϸ��̼��� �Ϸ�Ǿ����� �˸��� �޼��带 ȣ��
        enemy.AnimationFinishTrigger();
    }
}
