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
        EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();
        bulletScript.SetAttacker(enemy); // �߻��� ����
        bulletScript.Shoot(PlayerManager.instance.player.transform.position); // �÷��̾� ��ġ�� �Ѿ��� �߻�
    }

    private void DoubleBulletAttackTrigger()
    {
        Vector3 playerPos = PlayerManager.instance.player.transform.position;
        Vector3 baseDirection = (playerPos - firePoint.position).normalized;

        // �������� ������ �ϱ� ���� ���� ��� (��: 15���� �¿� ȸ��)
        float angleOffset = 15f;

        Vector3 leftDir = Quaternion.Euler(0, 0, angleOffset) * baseDirection;
        Vector3 rightDir = Quaternion.Euler(0, 0, -angleOffset) * baseDirection;

        // ���� �Ѿ�
        GameObject leftBullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        EnemyBullet leftScript = leftBullet.GetComponent<EnemyBullet>();
        leftScript.SetAttacker(enemy);
        leftScript.Shoot(firePoint.position + leftDir);

        // ������ �Ѿ�
        GameObject rightBullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        EnemyBullet rightScript = rightBullet.GetComponent<EnemyBullet>();
        rightScript.SetAttacker(enemy);
        rightScript.Shoot(firePoint.position + rightDir);
    }
}