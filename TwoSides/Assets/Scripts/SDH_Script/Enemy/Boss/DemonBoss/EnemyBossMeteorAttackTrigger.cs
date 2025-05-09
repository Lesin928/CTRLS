using UnityEngine;

/// <summary>
/// ������ ���׿� ���� Ʈ���� ó���� ����ϴ� Ŭ�����Դϴ�.
/// �� Ŭ������ ���׿� ������ �ߵ��ϰ�, �ִϸ��̼��� ������ �� �ļ� �۾��� ó���մϴ�.
/// </summary>
public class EnemyBossMeteorAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject meteorPrefab; // ���׿� ������
    [SerializeField] private Transform firePoint;     // ���׿� �߻� ��ġ

    // ���׿� ���� Ʈ����
    private void MeteorAttackTrigger()
    {
        // �θ� ��ü���� EnemyObject�� ã���ϴ�.
        EnemyObject enemy = GetComponentInParent<EnemyObject>();

        // ���׿� �������� �߻� ��ġ���� �ν��Ͻ�ȭ�մϴ�.
        GameObject meteor = Instantiate(meteorPrefab, firePoint.position, Quaternion.identity);

        // ���׿� ��ü�� ���� ��ũ��Ʈ�� �����ͼ� �����ڿ� ������ �����մϴ�.
        EnemyBossMeteor meteorScript = meteor.GetComponent<EnemyBossMeteor>();
        meteorScript.SetAttacker(enemy); // ������ ����
        meteorScript.SetDirection(enemy.facingDir); // ���� ����
    }

    // �ִϸ��̼��� �Ϸ�Ǿ��� �� ȣ��Ǵ� �޼��� (�ִϸ��̼� �̺�Ʈ���� ȣ��)
    private void MeteorAnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
}
