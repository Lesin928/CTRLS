using UnityEngine;

public class EnemyBossTransformTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject demonBossPrefab;
    [SerializeField] private Transform spawnPoint;

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