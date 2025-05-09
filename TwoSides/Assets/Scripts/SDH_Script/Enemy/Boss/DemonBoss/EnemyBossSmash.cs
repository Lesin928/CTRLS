using UnityEngine;

/// <summary>
/// ������ ���Ž� ���ݰ� ���õ� ó���� ����ϴ� Ŭ�����Դϴ�.
/// ���Ž� ������ �߻��ϸ�, �ش� ������ �÷��̾�� �浹�� �� ���ظ� �ְ� �ļ� ó���� �����մϴ�.
/// </summary>
public class EnemyBossSmash : MonoBehaviour
{
    private EnemyObject attacker; // ���Ž� ������ ������ �� ��ü

    /// <summary>
    /// ������ ������ �� ��ü�� �����ϴ� �Լ��Դϴ�.
    /// </summary>
    /// <param name="enemy">���Ž� ������ ������ �� ��ü</param>
    public void SetAttacker(EnemyObject enemy)
    {
        attacker = enemy; // �־��� �� ��ü�� attacker ������ �Ҵ�
    }

    /// <summary>
    /// ���Ž� ���� ������ �����ϴ� �Լ��Դϴ�.
    /// ���� ���⿡ �°� ���� ������Ʈ�� �������� �����մϴ�.
    /// </summary>
    /// <param name="facingDir">���Ž� ������ ���� (1�̸� ������, -1�̸� ����)</param>
    public void SetDirection(int facingDir)
    {
        Vector3 scale = transform.localScale; // ���� �������� ������
        scale.x = Mathf.Abs(scale.x) * facingDir; // ���� ���⿡ �°� x�� �������� ����
        transform.localScale = scale; // ����� �������� ����
    }

    // �浹 ó�� �Լ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // �÷��̾�� �浹�� ���
        {
            // �÷��̾�� ���ظ� �ֱ� ���� TakeDamage �Լ� ȣ��
            collision.GetComponent<PlayerObject>()?.TakeDamage(attacker.Attack);
        }
    }
}
