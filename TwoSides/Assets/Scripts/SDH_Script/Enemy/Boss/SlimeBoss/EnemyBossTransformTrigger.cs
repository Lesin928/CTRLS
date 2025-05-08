using UnityEngine;

/// <summary>
/// Slime Boss -> Demon Boss �� ��ȯ�ϴ� Ʈ�����Դϴ�.
/// Slime Boss�� Dead �ִϸ��̼ǿ��� �̺�Ʈ�� ȣ���Ͽ� Demon Boss�� �����մϴ�.
/// </summary>
public class EnemyBossTransformTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject demonBossPrefab; // DemonBoss ������
    [SerializeField] private Transform spawnPoint;       // ���� ��ġ

    // ���� ���� ����
    // Dead �ִϸ��̼ǿ��� �̺�Ʈ�� ȣ��
    private void CallDemonBoss()
    {
        // �θ� ������Ʈ(SlimeBoss)�� EnemyObject ������Ʈ���� facingDir�� ������
        EnemyObject slimeBoss = transform.parent.GetComponent<EnemyObject>();
        int slimeFacingDir = slimeBoss != null ? slimeBoss.facingDir : 1;

        // DemonBoss ������Ʈ ����
        GameObject boss = Instantiate(demonBossPrefab, spawnPoint.position, Quaternion.identity);

        // DemonBoss�� facingDir ����
        DemonBossObject demonScript = boss.GetComponent<DemonBossObject>();
        if (demonScript != null)
        {
            demonScript.InitFacingDir(slimeFacingDir);
        }
    }
}