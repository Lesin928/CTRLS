using UnityEngine;

/// <summary>
/// ������ �ظ� ���ݰ� ���õ� Ʈ���Ÿ� ó���ϴ� Ŭ�����Դϴ�.
/// �ִϸ��̼� �̺�Ʈ�� ���� �ظ� ����, ���� ����, �ִϸ��̼� ���� Ʈ���� ���� �����մϴ�.
/// </summary>
public class EnemyBossHammerAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject hamerPrefab; // ������ Hammer ������
    [SerializeField] private GameObject magicPrefab; // ������ Magic ������
    [SerializeField] private Transform firePoint;    // �ظ� ���� ��ġ
    [SerializeField] private Transform handPoint;    // ���� ���� ��ġ

    // �ظ� ����(�ִϸ��̼ǿ�)�� �����ϴ� Ʈ���� (�ִϸ��̼� �̺�Ʈ�� ȣ���)
    private void HammerMagicTrigger()
    {
        // handPoint ��ġ�� ���� ����
        GameObject magic = Instantiate(magicPrefab, handPoint.position, Quaternion.identity);

        EnemyBossHammerMagic magicScript = magic.GetComponent<EnemyBossHammerMagic>();
        magicScript.SetAttackTrigger(this);
    }

    /// <summary>
    /// �ظӸ� �����Ͽ� �����ϴ� Ʈ�����Դϴ�.
    /// EnemyBossHammerMagic�� DestroyTrigger()���� ȣ��˴ϴ�.
    /// </summary>
    public void HammerAttackTrigger()
    {
        // ���� EnemyObject ������Ʈ ��������
        EnemyObject enemy = GetComponentInParent<EnemyObject>();

        // �ظ� ����
        GameObject hammer = Instantiate(hamerPrefab, firePoint.position, Quaternion.identity);

        // ������ �ظӿ� ���� ���� ����
        EnemyBossHammer hammerScript = hammer.GetComponent<EnemyBossHammer>();
        hammerScript.SetAttacker(enemy);
        hammerScript.SetDirection(enemy.facingDir);
    }

    // �ִϸ��̼��� �Ϸ�Ǿ��� �� ȣ��Ǵ� �޼��� (�ִϸ��̼� �̺�Ʈ���� ȣ��)
    private void HammerAnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
}
