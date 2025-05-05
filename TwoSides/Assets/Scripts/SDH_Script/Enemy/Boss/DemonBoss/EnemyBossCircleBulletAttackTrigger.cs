using UnityEngine;

/// <summary>
/// ���� �Ѿ� ���� �ִϸ��̼� Ʈ���Ÿ� ó���ϴ� Ŭ�����Դϴ�.
/// �� Ŭ������ ���� �Ѿ��� �߻��ϴ� �ൿ�� �����մϴ�.
/// </summary>
public class EnemyBossCircleBulletAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject bulletPrefab; // �߻��� �Ѿ��� ������
    [SerializeField] private Transform firePoint;     // �Ѿ��� �߻�� ��ġ

    // �Ѿ� ���� �� �߻� (�ִϸ��̼� �̺�Ʈ���� ȣ��)
    private void CircleBulletAttackTrigger()
    {
        // �Ѿ��� �߻� ��ġ���� �ν��Ͻ�ȭ
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // �Ѿ��� Bullet ��ũ��Ʈ�� �����ͼ� �߻� ������ ����
        EnemyBossCircleBullet bulletScript = bullet.GetComponent<EnemyBossCircleBullet>();
        bulletScript.SetAttacker(enemy); // �߻��� ����
    }

    // �ִϸ��̼��� �Ϸ�Ǿ��� �� ȣ��Ǵ� �޼���
    private void CircleBulletAnimationTrigger()
    {
        // ���� �ִϸ��̼��� �Ϸ�Ǿ����� �˸��� �޼��带 ȣ��
        enemy.AnimationFinishTrigger();
    }
}