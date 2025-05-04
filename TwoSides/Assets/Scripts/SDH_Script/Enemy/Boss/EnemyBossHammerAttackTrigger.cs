using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ���� Slash ���� �ִϸ��̼� Ʈ���Ÿ� ó���ϴ� Ŭ�����Դϴ�.
/// ���� ���� ���� �÷��̾ ������ ���� ���� �޽����� ����մϴ�.
/// </summary>
public class EnemyBossHammerAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject hamerSlashPrefab; // Slash ������
    [SerializeField] private GameObject magicPrefab; // Magic ������
    [SerializeField] private Transform firePoint;    // ���� �߻� ��ġ
    [SerializeField] private Transform handPoint;    // �� ��ġ

    public void HammerAttackTrigger()
    {
        // �θ� EnemyObject�� ������
        EnemyObject enemy = GetComponentInParent<EnemyObject>();

        // Slash �������� �߻� ������ ����
        GameObject slash = Instantiate(hamerSlashPrefab, firePoint.position, Quaternion.identity);

        // Slash ��ü�� ��ũ��Ʈ�� �����ͼ� Ȱ��ȭ
        EnemyBossHammer slashScript = slash.GetComponent<EnemyBossHammer>();
        slashScript.SetAttacker(enemy); // �߻��� ����
        slashScript.Active(enemy.facingDir);
    }

    private void HammerMagicTrigger()
    {
        GameObject magic = Instantiate(magicPrefab, handPoint.position, Quaternion.identity);

        EnemyBossHammerMagic magicScript = magic.GetComponent<EnemyBossHammerMagic>();
        magicScript.SetAttackTrigger(this);
    }

    // �ִϸ��̼��� �Ϸ�Ǿ��� �� ȣ��Ǵ� �޼���
    private void HammerAnimationTrigger()
    {
        // ���� �ִϸ��̼��� �Ϸ�Ǿ����� �˸��� �޼��带 ȣ��
        enemy.AnimationFinishTrigger();
    }
}
