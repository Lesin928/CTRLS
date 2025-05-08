using UnityEngine;

/// <summary>
/// ������ ���Ž� ���ݰ� ���õ� Ʈ���Ÿ� ����ϴ� Ŭ�����Դϴ�.
/// ���Ž� ������ �߻��� ��, ������ ���� �� �ļ� ó���� �����մϴ�.
/// </summary>
public class EnemyBossSmashAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject smashPrefab; // ���Ž� ���ݿ� ����� ������
    [SerializeField] private Transform firePoint;    // ���Ž� ���� �߻� ����

    private GameObject smash; // ���Ž� ���� ������ ��ü

    /// <summary>
    /// ���Ž� ������ Ȱ��ȭ��Ű�� Ʈ���� �Լ� (�ִϸ��̼� �̺�Ʈ���� ȣ��)
    private void SmashAttackTrigger()
    {
        // ������ �����ϴ� �� ��ü�� ������
        EnemyObject enemy = GetComponentInParent<EnemyObject>();

        // ���Ž� ������ �߻� �������� ����
        smash = Instantiate(smashPrefab, firePoint.position, Quaternion.identity);

        // ���Ž� ���� ��ü�� ��ũ��Ʈ�� ������ ����
        EnemyBossSmash smashScript = smash.GetComponent<EnemyBossSmash>();
        smashScript.SetAttacker(enemy); // �������� ���� ����
        smashScript.SetDirection(enemy.facingDir); // ���� ���� ����
    }

    // ���Ž� ���� ��ü�� �ı��ϴ� �Լ� (�ִϸ��̼� �̺�Ʈ���� ȣ��)
    private void SmashDestroyTrigger()
    {
        if (smash != null)
        {
            Destroy(smash); // ���Ž� ��ü �ı�
            smash = null; // ���Ž� ���� �ʱ�ȭ
        }
    }

    // ������ �ݶ��̴��� ��Ȱ��ȭ�ϴ� �Լ� (���� �ִϸ��̼��� �� ��Ȱ��ȭ)
    private void Collider2DOff()
    {
        GetComponentInParent<Collider2D>().enabled = false; // �ݶ��̴� ��Ȱ��ȭ
    }

    // ������ �ݶ��̴��� Ȱ��ȭ�ϴ� �Լ�
    private void Collider2DOn()
    {
        GetComponentInParent<Collider2D>().enabled = true; // �ݶ��̴� Ȱ��ȭ
    }

    // �ִϸ��̼��� �Ϸ�Ǿ��� �� ȣ��Ǵ� �޼��� (�ִϸ��̼� �̺�Ʈ���� ȣ��)
    private void SmashAnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
}
