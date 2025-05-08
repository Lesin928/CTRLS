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
        // ���� ���� ���� �������� �߻� ������ ����
        GameObject earthBump = Instantiate(earthBumpPrefab, firePoint.position, Quaternion.identity);

        // ���� ���� ���� ��ü�� ��ũ��Ʈ�� �����ͼ� Ȱ��ȭ
        EnemyEarthBump earthBumpScript = earthBump.GetComponent<EnemyEarthBump>();
        earthBumpScript.SetAttacker(enemy); // �߻��� ����
        earthBumpScript.SetDirection(enemy.facingDir);// ���� ����
    }
}
