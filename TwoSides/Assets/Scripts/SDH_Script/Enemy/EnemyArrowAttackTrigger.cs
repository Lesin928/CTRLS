using UnityEngine;

/// <summary>
/// ���� ȭ�� ���� �ִϸ��̼� Ʈ���Ÿ� ó���ϴ� Ŭ�����Դϴ�.
/// �ִϸ��̼� �̺�Ʈ���� ȣ��Ǿ� ȭ���� �߻��մϴ�.
/// </summary>
public class EnemyArrowAttackTrigger : EnemyAnimationTrigger
{
    [SerializeField] private GameObject arrowPrefab; // ȭ�� ������
    [SerializeField] private Transform firePoint;    // ȭ�� �߻� ��ġ

    // ȭ�� ���� �� �߻� (�ִϸ��̼� �̺�Ʈ���� ȣ��)
    private void ArrowAttackTrigger()
    {
        // ȭ�� �������� �߻� ������ ����
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);


        // ȭ�� ��ü�� ��ũ��Ʈ�� �����ͼ� �÷��̾ ���� �߻�
        EnemyArrow arrowScript = arrow.GetComponent<EnemyArrow>();
        arrowScript.SetAttacker(enemy); // �߻��� ����
        arrowScript.Shoot(PlayerManager.instance.player.transform.position);
    }
}
