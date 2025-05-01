using UnityEngine;

/// <summary>
/// ���� �Ѿ� ���� �ִϸ��̼� Ʈ���Ÿ� ó���ϴ� Ŭ�����Դϴ�.
/// �� Ŭ������ ���� �Ѿ��� �߻��ϴ� �ൿ�� �����մϴ�.
/// </summary>
public class EnemyBulletAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject bulletPrefab; // �߻��� �Ѿ��� ������
    [SerializeField] private Transform firePoint;     // �Ѿ��� �߻�� ��ġ

    // �Ѿ� ���� �� �߻� (�ִϸ��̼� �̺�Ʈ���� ȣ��)
    private void BulletAttackTrigger()
    {
        // �Ѿ��� �߻� ��ġ���� �ν��Ͻ�ȭ
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // �Ѿ��� Bullet ��ũ��Ʈ�� �����ͼ� �߻� ������ ����
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.Shoot(PlayerManager.instance.player.transform.position); // �÷��̾� ��ġ�� �Ѿ��� �߻�
    }
}