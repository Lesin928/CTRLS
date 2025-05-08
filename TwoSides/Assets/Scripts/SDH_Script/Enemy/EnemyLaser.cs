using UnityEngine;

/// <summary>
/// ���� ������ ������ ó���ϴ� Ŭ����.
/// �������� �߻� �� ���� �ð��� ���� �� �ڵ����� �����Ǹ�, �÷��̾�� �浹 �� ���ظ� ������.
/// </summary>
public class EnemyLaser : MonoBehaviour
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
    /// �������� ������ �����Ѵ�.
    /// </summary>
    /// <param name="facingDir">�������� ���� (1�� ������, -1�� ����)</param>
    public void SetDirection(int facingDir)
    {
        // �������� Y�� ������ �����Ͽ� ������ �ٲ۴�.
        Vector3 scale = transform.localScale;
        scale.y = Mathf.Abs(scale.y) * -facingDir;
        transform.localScale = scale;

        // Z �� ȸ���� 90���� �����Ͽ� �������� ������ �����Ѵ�.
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
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
    /// �������� �浹�� ��ü���� ��ȣ�ۿ��� ó���Ѵ�.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ��ü�� �÷��̾��� ���
        if (collision.CompareTag("Player"))
        {
            // �÷��̾� ��ü�� �����ϸ� ���ظ� ������.
            collision.GetComponent<PlayerObject>()?.TakeDamage(attacker.Attack);
        }
    }

    // �������� �� �̻� �ʿ� ���� ��� ������Ʈ�� �����Ѵ�.
    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
