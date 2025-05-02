using UnityEngine;

/// <summary>
/// ���� ������ ���� �ִϸ��̼� Ʈ���Ÿ� ó���ϴ� Ŭ�����Դϴ�.
/// �ִϸ��̼� �߿� �������� Ȱ��ȭ�ǰų� �߻�˴ϴ�.
/// </summary>
public class EnemyLaserAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject laserActivePrefab; // ������ Ȱ��ȭ ������
    [SerializeField] private GameObject laserPrefab;       // ������ �߻� ������
    [SerializeField] private Transform firePoint;          // ������ �߻� ��ġ

    // LaserActive ������Ʈ(������)�� ���� (�ִϸ��̼� �̺�Ʈ���� ȣ��)
    private void LaserActiveTrigger()
    {
        // LaserActive(������) �������� �߻� ������ ����
        Instantiate(laserActivePrefab, firePoint.position, Quaternion.identity);
    }

    // Laser ������Ʈ�� ���� ( �ִϸ��̼� �̺�Ʈ���� ȣ��)
    private void LaserAttackTrigger()
    {
        // ������ �������� �߻� ������ ����
        GameObject laser = Instantiate(laserPrefab, firePoint.position, Quaternion.identity);

        // ������ ��ü�� ��ũ��Ʈ�� �����ͼ� �������� �߻�
        EnemyLaser laserScript = laser.GetComponent<EnemyLaser>();
        laserScript.SetAttacker(enemy); // �߻��� ����
        laserScript.Shoot(enemy.facingDir);
    }
}

