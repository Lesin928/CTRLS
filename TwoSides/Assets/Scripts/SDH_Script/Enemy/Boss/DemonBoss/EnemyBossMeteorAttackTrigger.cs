using UnityEngine;

/// <summary>
/// ���� Meteor ���� �ִϸ��̼� Ʈ���Ÿ� ó���ϴ� Ŭ�����Դϴ�.
/// ���� ���� ���� �÷��̾ ������ ���� ���� �޽����� ����մϴ�.
/// </summary>
public class EnemyBossMeteorAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject meteorPrefab; // Meteor ������
    [SerializeField] private Transform firePoint;     // ���� �߻� ��ġ

    /// <summary>
    /// ���� Meteor�� �߻��ϴ� Ʈ�����Դϴ�.
    /// �ִϸ��̼� �̺�Ʈ�� ȣ��˴ϴ�.
    /// </summary>
    private void MeteorAttackTrigger()
    {
        // �θ� EnemyObject�� ������
        EnemyObject enemy = GetComponentInParent<EnemyObject>();

        // Meteor �������� �߻� ������ ����
        GameObject meteor = Instantiate(meteorPrefab, firePoint.position, Quaternion.identity);

        // Meteor ��ü�� ��ũ��Ʈ�� �����ͼ� Ȱ��ȭ
        EnemyBossMeteor meteorScript = meteor.GetComponent<EnemyBossMeteor>();
        meteorScript.SetAttacker(enemy); // �߻��� ����
        meteorScript.SetDirection(enemy.facingDir);
    }

    private void MeteorAnimationTrigger()
    {
        // ���� �ִϸ��̼��� �Ϸ�Ǿ����� �˸��� �޼��带 ȣ��
        enemy.AnimationFinishTrigger();
    }
}
