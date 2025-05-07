using UnityEngine;

/// <summary>
/// Boss�� Vortex ���� ����Ʈ�� ó���ϴ� Ŭ�����Դϴ�.
/// �ִϸ��̼� �̺�Ʈ�� ���� Ȱ��ȭ�Ǹ�, �÷��̾�� �浹 �� ó�� ������ �����մϴ�.
/// </summary>
public class EnemyBossVortex : MonoBehaviour
{
    private EnemyObject attacker; // ������ �߻��� �� ��ü

    private void Start()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = true;
    }

    /// <summary>
    /// ������ �߻��� EnemyObject�� �������� �Լ��Դϴ�.
    /// </summary>
    public void SetAttacker(EnemyObject enemy)
    {
        attacker = enemy;
    }

    /// <summary>
    /// ���� �ٶ󺸴� ���⿡ ���� ����Ʈ ������ �����մϴ�.
    /// </summary>
    /// <param name="facingDir">���� �ٶ󺸴� ���� (1 �Ǵ� -1)</param>
    public void SetDirection(int facingDir)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * facingDir;
        transform.localScale = scale;
    }

    // ���� Ȱ��ȭ (�ִϸ��̼� �̺�Ʈ���� ȣ��)
    private void EnableAttack()
    {
        GetComponent<CapsuleCollider2D>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = false;
    }

    // ���� ��Ȱ��ȭ (�ִϸ��̼� �̺�Ʈ���� ȣ��)
    private void DisableAttack()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
    }

    // �÷��̾�� �浹 �� ȣ��
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // �÷��̾�� ������ ����
            collision.GetComponent<PlayerObject>()?.TakeDamage(attacker.Attack);
        }
    }

    // �ִϸ��̼� �̺�Ʈ���� ȣ��Ǿ� ����Ʈ ������Ʈ�� �ı�
    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
