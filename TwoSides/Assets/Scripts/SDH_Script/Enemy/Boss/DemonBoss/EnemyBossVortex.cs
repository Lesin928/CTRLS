using UnityEngine;

/// <summary>
/// ������ �ҿ뵹�� ���ݰ� ���õ� Ʈ���Ÿ� ����ϴ� Ŭ�����Դϴ�.
/// �ҿ뵹�� ������ �߻��ϸ� ������ ����� �ļ� ó���� �����մϴ�.
/// </summary>
public class EnemyBossVortex : MonoBehaviour
{
    private EnemyObject attacker; // ������

    private void Start()
    {
        // �ҿ뵹�� ���� ��, CapsuleCollider�� ��Ȱ��ȭ�ϰ� CircleCollider�� Ȱ��ȭ
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = true;
    }

    /// <summary>
    /// �����ڸ� �����ϴ� �Լ��Դϴ�.
    /// </summary>
    /// <param name="enemy">�� ��ü</param>
    public void SetAttacker(EnemyObject enemy)
    {
        attacker = enemy;
    }

    /// <summary>
    /// �ҿ뵹���� ������ �����ϴ� �Լ��Դϴ�.
    /// </summary>
    /// <param name="facingDir">�ҿ뵹���� ���� (1�̸� ������, -1�̸� ����)</param>
    public void SetDirection(int facingDir)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * facingDir; // x�� ũ�⸦ ���⿡ �°� ����
        transform.localScale = scale;
    }

    // ���� Ȱ��ȭ (�ִϸ��̼� �̺�Ʈ���� ȣ���)
    private void EnableAttack()
    {
        GetComponent<CapsuleCollider2D>().enabled = true; // ���� ���� Ȱ��ȭ
        GetComponent<CircleCollider2D>().enabled = false; // Vortex ȿ�� ��Ȱ��ȭ
    }

    // ���� ��Ȱ��ȭ (�ִϸ��̼� �̺�Ʈ���� ȣ���)
    private void DisableAttack()
    {
        GetComponent<CapsuleCollider2D>().enabled = false; // ���� ���� ��Ȱ��ȭ
    }

    // �浹 ó�� �Լ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // �÷��̾ �ҿ뵹�̿� ������ �������� ���ݷ¸�ŭ ���ظ� ����
            collision.GetComponent<PlayerObject>()?.TakeDamage(attacker.Attack);
        }
    }

    // �ҿ뵹�� ���� ��, ��ü�� �����ϴ� �Լ��Դϴ�.
    private void DestroyTrigger()
    {
        Destroy(gameObject); // �ҿ뵹�� ��ü ����
    }
}
