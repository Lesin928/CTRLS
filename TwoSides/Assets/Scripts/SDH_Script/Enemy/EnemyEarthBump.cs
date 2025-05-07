using UnityEngine;

/// <summary>
/// ���� ���� ���� ����Ʈ�� ó���ϴ� Ŭ�����Դϴ�.
/// �ִϸ��̼� �̺�Ʈ�� ���� Ȱ��ȭ�Ǹ�, �÷��̾�� �浹 �� ó�� ������ �����մϴ�.
/// </summary>
public class EnemyEarthBump : MonoBehaviour
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
    /// ����Ʈ�� ����� ȸ���� �����մϴ�.
    /// </summary>
    /// <param name="facingDir">���� �ٶ󺸴� ���� (1 �Ǵ� -1)</param>
    public void SetDirection(int facingDir)
    {
        // �ٶ󺸴� ���⿡ ���� Y �� ������ ����
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
            var player = collision.GetComponent<PlayerObject>();
            if (player != null)
            {
                player.TakeDamage(attacker.Attack);

                var rb = collision.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 knockbackDir = Vector2.up;

                    float knockbackPower = 20f;

                    rb.linearVelocity = knockbackDir * knockbackPower;
                }
            }
        }
    }


    // �ִϸ��̼� �̺�Ʈ���� ȣ��Ǿ� ������Ʈ�� ������
    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
