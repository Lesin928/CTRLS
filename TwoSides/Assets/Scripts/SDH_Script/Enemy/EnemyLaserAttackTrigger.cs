using System.Net;
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
    private void LaserMagicTrigger()
    {
        // LaserActive(������) �������� �߻� ������ ����
        GameObject magic = Instantiate(laserActivePrefab, firePoint.position, Quaternion.identity);

        EnemyLaserMagic magicScript = magic.GetComponent<EnemyLaserMagic>();
        magicScript.SetAttackTrigger(this);
    }

    // Laser ������Ʈ�� ����
    public void LaserAttackTrigger()
    {
        // ������ �������� �߻� ������ ����
        GameObject laser = Instantiate(laserPrefab, firePoint.position, Quaternion.identity);

        // ������ ��ü�� ��ũ��Ʈ�� �����ͼ� �������� �߻�
        EnemyLaser laserScript = laser.GetComponent<EnemyLaser>();
        laserScript.SetAttacker(enemy); // �߻��� ����
        laserScript.Shoot(enemy.facingDir);
    }
}

