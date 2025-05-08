using UnityEngine;

/// <summary>
/// ������ ������ ���ݰ� ���õ� Ʈ���Ÿ� ó���ϴ� Ŭ�����Դϴ�.
/// �����ÿ� ������ �����ϰ�, �ش� ������ Ʈ���Ÿ� �����մϴ�.
/// </summary>
public class EnemyBossSlashAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject slashPrefab; // ������ ���� ������
    [SerializeField] private GameObject magicPrefab; // ���� ������
    [SerializeField] private Transform firePoint;    // ������ ������ �߻�� ��ġ
    [SerializeField] private Transform handPoint;    // ������ ������ ��ġ

    // ������ Magic ������ Ʈ�����ϴ� �Լ��Դϴ�.
    private void SlashMagicTrigger()
    {
        // ������ �߻��� ��ġ���� ���� �������� ����
        GameObject magic = Instantiate(magicPrefab, handPoint.position, Quaternion.identity);

        // ������ ���� ��ü�� ���� ��ũ��Ʈ�� ������ ����
        EnemyBossSlashMagic magicScript = magic.GetComponent<EnemyBossSlashMagic>();
        magicScript.SetAttackTrigger(this); // ���� Ʈ���Ÿ� ������ ����
    }

    /// <summary>
    /// ������ ������ Ʈ�����ϴ� �Լ��Դϴ�.
    /// EnemyBossSlashMagic�� DestroyTrigger()�� ȣ��Ǹ� �ش� ������ ó���մϴ�.
    /// </summary>
    public void SlashAttackTrigger()
    {
        // �θ� ��ü���� EnemyObject�� �����ɴϴ�.
        EnemyObject enemy = GetComponentInParent<EnemyObject>();

        // ������ ������ �߻��� ��ġ���� ������ ���� �������� ����
        GameObject slash = Instantiate(slashPrefab, firePoint.position, Quaternion.identity);

        // ������ ������ ���� ��ü�� ���� ��ũ��Ʈ�� ������ ����
        EnemyBossSlash slashScript = slash.GetComponent<EnemyBossSlash>();
        slashScript.SetAttacker(enemy); // ������ ������ �����ڸ� ����
        slashScript.SetDirection(enemy.facingDir); // ������ ������ ���� ����
    }

    // �ִϸ��̼��� �Ϸ�Ǿ��� �� ȣ��Ǵ� �޼��� (�ִϸ��̼� �̺�Ʈ���� ȣ��)
    private void SlashAnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
}
