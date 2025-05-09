using UnityEngine;

/// <summary>
/// ���� ���� �浹 ó���� ����ϴ� Ŭ�����̴�.
/// �� Ŭ������ �浹 �� ���� ������ Ȱ��ȭ, ��Ȱ��ȭ�ϸ�, �÷��̾�� �浹 �� ���ؿ� �ݰ��� ó���Ѵ�.
/// </summary>
public class EnemyEarthBump : MonoBehaviour
{
    private EnemyObject attacker; // ������ �� ��ü

    private void Start()
    {
        // �ʱ�ȭ �� Collider2D ��Ȱ��ȭ
        GetComponent<Collider2D>().enabled = false;
    }

    /// <summary>
    /// ������ �� ��ü�� �����Ѵ�.
    /// </summary>
    public void SetAttacker(EnemyObject enemy)
    {
        attacker = enemy;
    }

    /// <summary>
    /// ���� ������ �����Ѵ�.
    /// </summary>
    /// <param name="facingDir">���� ���� (1�� ������, -1�� ����)</param>
    public void SetDirection(int facingDir)
    {
        // ���� ���⿡ ���� X�� �������� ������Ų��.
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * facingDir;
        transform.localScale = scale;
    }

    // ������ Ȱ��ȭ (Collider2D Ȱ��ȭ)
    private void EnableAttack()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    // ������ ��Ȱ��ȭ (Collider2D ��Ȱ��ȭ)
    private void DisableAttack()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    /// <summary>
    /// �浹�� �߻����� �� ȣ��ȴ�.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾�� �浹�� ���
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<PlayerObject>();
            if (player != null)
            {
                // �÷��̾�� ���ظ� ������.
                player.TakeDamage(attacker.Attack);

                var rb = collision.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    // �÷��̾�� �ݰ��� �ش�.
                    Vector2 knockbackDir = Vector2.up;  // �ݰ� ����
                    float knockbackPower = 20f;         // �ݰ� ����

                    // �ݰ� ����
                    rb.linearVelocity = knockbackDir * knockbackPower;
                }
            }
        }
    }

    // ������ �����ϰ� ������Ʈ�� �ı��Ѵ�.
    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
