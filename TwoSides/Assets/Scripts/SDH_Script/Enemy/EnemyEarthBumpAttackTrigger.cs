using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ���� ���� ���� �ִϸ��̼� Ʈ���Ÿ� ó���ϴ� Ŭ�����Դϴ�.
/// �ִϸ��̼� �̺�Ʈ���� ȣ��Ǿ� ���� ���� ������ �߻��մϴ�.
/// </summary>
public class EnemyEarthBumpAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject earthBumpPrefab; // ���� ���� ���� ������
    [SerializeField] private Transform firePoint;        // ���� �߻� ��ġ

    /// ���� ���� ������ �߻� (�ִϸ��̼� �̺�Ʈ���� ȣ��)
    private void EarthBumpAttackTrigger()
    {
        // �θ� EnemyObject�� ������
        EnemyObject enemy = GetComponentInParent<EnemyObject>();

        // ���� ���� ���� �������� �߻� ������ ����
        GameObject earthBump = Instantiate(earthBumpPrefab, firePoint.position, Quaternion.identity);

        // ���� ���� ���� ��ü�� ��ũ��Ʈ�� �����ͼ� Ȱ��ȭ
        EarthBump earthBumpScript = earthBump.GetComponent<EarthBump>();
        earthBumpScript.Active(enemy.facingDir);
    }
}
