using UnityEngine;

/// <summary>
/// ������ �Ѿ� ���� �ִϸ��̼� Ʈ���Ÿ� ó���ϴ� Ŭ�����Դϴ�.
/// �� Ŭ������ ������ �Ѿ��� �߻��ϴ� �ൿ�� �����մϴ�.
/// </summary>
public class EnemyBossBulletAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject bulletPrefab; // �߻��� �Ѿ��� ������
    [SerializeField] private Transform firePoint;     // �Ѿ��� �߻�� ��ġ

    // �Ѿ� ���� �� �߻� (�ִϸ��̼� �̺�Ʈ���� ȣ��)
    private void BulletAttackTrigger()
    {
        // �Ѿ��� �߻� ��ġ���� �ν��Ͻ�ȭ
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // �Ѿ��� Bullet ��ũ��Ʈ�� �����ͼ� �߻� ������ ����
        EnemyBossBullet bulletScript = bullet.GetComponent<EnemyBossBullet>();
        bulletScript.SetAttacker(enemy); // �߻��� ����

        // ��ǥ ��ġ�� x�� �÷��̾�, y�� firePoint ��ġ�� ����
        Vector2 targetPosition = new Vector2(PlayerManager.instance.player.transform.position.x, firePoint.position.y);
        bulletScript.Shoot(targetPosition);
    }

    // �ִϸ��̼��� �Ϸ�Ǿ��� �� ȣ��Ǵ� �޼��� (�ִϸ��̼� �̺�Ʈ���� ȣ��)
    private void BulletAnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
}