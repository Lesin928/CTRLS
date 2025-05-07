using System.Net;
using UnityEngine;

/// <summary>
/// ���� ������ ���� �ִϸ��̼� Ʈ���Ÿ� ó���ϴ� Ŭ�����Դϴ�.
/// �ִϸ��̼� �߿� �������� Ȱ��ȭ�˴ϴ�.
/// </summary>
public class EnemyLaserAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject laserMagicPrefab; // ������ ������ ������
    [SerializeField] private GameObject laserPrefab;      // ������ �߻� ������
    [SerializeField] private Transform firePoint;         // ������ �߻� ��ġ

    // LaserMagic ������Ʈ(������)�� ���� (�ִϸ��̼� �̺�Ʈ���� ȣ��)
    private void LaserMagicTrigger()
    {
        // LaserMagic(������) �������� �߻� ������ ����
        GameObject magic = Instantiate(laserMagicPrefab, firePoint.position, Quaternion.identity);

        EnemyLaserMagic magicScript = magic.GetComponent<EnemyLaserMagic>();
        magicScript.SetAttackTrigger(this);
    }

    /// <summary>
    /// ���� �������� �߻��ϴ� Ʈ�����Դϴ�.
    /// EnemyLaserMagic�� DestroyTrigger()���� ȣ��˴ϴ�.
    /// </summary>
    public void LaserAttackTrigger()
    {
        // ������ �������� �߻� ������ ����
        GameObject laser = Instantiate(laserPrefab, firePoint.position, Quaternion.identity);

        // ������ ��ü�� ��ũ��Ʈ�� �����ͼ� �������� �߻�
        EnemyLaser laserScript = laser.GetComponent<EnemyLaser>();
        laserScript.SetAttacker(enemy); // �߻��� ����
        laserScript.SetDirection(enemy.facingDir);
    }
}

