using UnityEngine;

/// <summary>
/// Boss�� Slash ���� ����Ʈ�� ó���ϴ� Ŭ�����Դϴ�.
/// �ִϸ��̼� �̺�Ʈ�� ���� Ȱ��ȭ�Ǹ�, �÷��̾�� �浹 �� ó�� ������ �����մϴ�.
/// </summary>
public class EnemyBossSlash : MonoBehaviour
{
    private EnemyObject attacker; // ������ �߻��� �� ��ü

    private void Start()
    {
        GetComponent<Collider2D>().enabled = false;
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
        GetComponent<Collider2D>().enabled = true;
    }

    // ���� ��Ȱ��ȭ (�ִϸ��̼� �̺�Ʈ���� ȣ��)
    private void DisableAttack()
    {
        GetComponent<Collider2D>().enabled = false;
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
