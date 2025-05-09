using UnityEngine;

/// <summary>
/// ������ ���׿� ���� ó���� ����ϴ� Ŭ�����Դϴ�.
/// �� Ŭ������ ���׿� ������ Ȱ��ȭ/��Ȱ��ȭ�ϰ�, �÷��̾�� �浹 �� �������� �ִ� ���� ������ �մϴ�.
/// </summary>
public class EnemyBossMeteor : MonoBehaviour
{
    private EnemyObject attacker; // ������

    private void Start()
    {
        // ���� ���� �� �ݶ��̴� ��Ȱ��ȭ
        GetComponent<Collider2D>().enabled = false;
    }

    /// <summary>
    /// �����ڸ� �����ϴ� �Լ��Դϴ�.
    /// </summary>
    /// <param name="enemy">�� ��ü(EnemyObject)</param>
    public void SetAttacker(EnemyObject enemy)
    {
        attacker = enemy;
    }

    /// <summary>
    /// Meteor�� ������ �����ϴ� �Լ��Դϴ�.
    /// </summary>
    /// <param name="facingDir">���� ���� (1�� ������, -1�� ����)</param>
    public void SetDirection(int facingDir)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * facingDir; // ���⿡ �°� �������� ����
        transform.localScale = scale;
    }

    // ���� Ȱ��ȭ (�ִϸ��̼� �̺�Ʈ���� ȣ���)
    private void EnableAttack()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    // ���� Ȱ��ȭ (�ִϸ��̼� �̺�Ʈ���� ȣ���)
    private void DisableAttack()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    // �浹 ó�� �Լ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // �÷��̾ �浹 �� ������ ó��
            collision.GetComponent<PlayerObject>()?.TakeDamage(attacker.Attack);
        }
    }

    // Meteor ���� �� ������Ʈ�� �����ϴ� �Լ�
    private void DestroyTrigger()
    {
        Destroy(gameObject); // Meteor ������Ʈ �ı�
    }
}
