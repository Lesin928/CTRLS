using UnityEngine;

/// <summary>
/// ���� Smash ���� �ִϸ��̼� Ʈ���Ÿ� ó���ϴ� Ŭ�����Դϴ�.
/// ���� ���� ���� �÷��̾ ������ ���� ���� �޽����� ����մϴ�.
/// </summary>
public class EnemyBossSmashAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject smashPrefab; // Smash ������
    [SerializeField] private Transform firePoint;    // ���� �߻� ��ġ

    private GameObject smash; // ������ smash ������Ʈ ����

    /// <summary>
    /// Smash�� Ȱ��ȭ�ϴ� Ʈ�����Դϴ�.
    /// </summary>
    private void SmashAttackTrigger()
    {
        // �θ� EnemyObject�� ������
        EnemyObject enemy = GetComponentInParent<EnemyObject>();

        // Smash �������� �߻� ������ ����
        smash = Instantiate(smashPrefab, firePoint.position, Quaternion.identity);

        // Slash ��ü�� ��ũ��Ʈ�� �����ͼ� Ȱ��ȭ
        EnemyBossSmash smashScript = smash.GetComponent<EnemyBossSmash>();
        smashScript.SetAttacker(enemy); // �߻��� ����
        smashScript.SetDirection(enemy.facingDir);
    }

    private void SmashDestroyTrigger()
    {
        if (smash != null)
        {
            Destroy(smash); // �ִϸ��̼� ���� �� ����
            smash = null;
        }
    }

    private void Collider2DOff()
    {
        GetComponentInParent<Collider2D>().enabled = false;
    }

    private void Collider2DOn()
    {
        GetComponentInParent<Collider2D>().enabled = true;
    }

    private void SmashAnimationTrigger()
    {
        // ���� �ִϸ��̼��� �Ϸ�Ǿ����� �˸��� �޼��带 ȣ��
        enemy.AnimationFinishTrigger();
    }
}
