using UnityEngine;

/// <summary>
/// ���� Slash ���� �ִϸ��̼� Ʈ���Ÿ� ó���ϴ� Ŭ�����Դϴ�.
/// ���� ���� ���� �÷��̾ ������ ���� ���� �޽����� ����մϴ�.
/// </summary>
public class EnemyBossHammerAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject hamerPrefab; // Hammer ������
    [SerializeField] private GameObject magicPrefab; // Magic ������
    [SerializeField] private Transform firePoint;    // ���� �߻� ��ġ
    [SerializeField] private Transform handPoint;    // �� ��ġ

    // HammerMagic ������Ʈ(������)�� ���� (�ִϸ��̼� �̺�Ʈ���� ȣ��)
    private void HammerMagicTrigger()
    {
        // HammerMagic(������) �������� handPoint�� ����
        GameObject magic = Instantiate(magicPrefab, handPoint.position, Quaternion.identity);

        EnemyBossHammerMagic magicScript = magic.GetComponent<EnemyBossHammerMagic>();
        magicScript.SetAttackTrigger(this);
    }

    /// <summary>
    /// ���� Hammer�� �߻��ϴ� Ʈ�����Դϴ�.
    /// EnemyBossHammerMagic DestroyTrigger()���� ȣ��˴ϴ�.
    /// </summary>
    public void HammerAttackTrigger()
    {
        // �θ� EnemyObject�� ������
        EnemyObject enemy = GetComponentInParent<EnemyObject>();

        // Hammer �������� �߻� ������ ����
        GameObject hammer = Instantiate(hamerPrefab, firePoint.position, Quaternion.identity);

        // Hammer ��ü�� ��ũ��Ʈ�� �����ͼ� Ȱ��ȭ
        EnemyBossHammer hammerScript = hammer.GetComponent<EnemyBossHammer>();
        hammerScript.SetAttacker(enemy); // �߻��� ����
        hammerScript.SetDirection(enemy.facingDir);
    }

    // �ִϸ��̼��� �Ϸ�Ǿ��� �� ȣ��Ǵ� �޼���
    private void HammerAnimationTrigger()
    {
        // ���� �ִϸ��̼��� �Ϸ�Ǿ����� �˸��� �޼��带 ȣ��
        enemy.AnimationFinishTrigger();
    }
}
