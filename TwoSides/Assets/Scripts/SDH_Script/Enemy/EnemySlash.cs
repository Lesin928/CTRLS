using UnityEngine;

/// <summary>
/// ���� ������ ������ ó���ϴ� Ŭ����.
/// ������ ���� ���� ���� �ִ� �÷��̾�� ���ظ� ������.
/// </summary>
public class EnemySlash : MonoBehaviour
{
    private EnemyObject attacker; // ���� ������ (EnemyObject)

    private void Start()
    {
        // ���� �� ���� �ݶ��̴� ��Ȱ��ȭ
        GetComponent<Collider2D>().enabled = false;
    }

    /// <summary>
    /// �������� EnemyObject�� �����Ѵ�.
    /// </summary>
    public void SetAttacker(EnemyObject enemy)
    {
        attacker = enemy;
    }

    /// <summary>
    /// ���� ������ �����Ѵ�.
    /// </summary>
    /// <param name="facingDir">���� ���� (1�̸� ������, -1�̸� ����)</param>
    public void SetDirection(int facingDir)
    {
        Vector3 scale = transform.localScale;
        // x�� ũ�� ������ ���� ���� ������ �����Ѵ�.
        scale.x = Mathf.Abs(scale.x) * facingDir;
        transform.localScale = scale;
    }

    // ������ ���� Ȱ��ȭ
    private void EnableAttack()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    // ������ ���� ��Ȱ��ȭ
    private void DisableAttack()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    // ������ ������ �浹�� ��ü�� ���� ó��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ��ü�� �÷��̾��� ��� ���ظ� ������.
        if (collision.CompareTag("Player"))
        {
            // �÷��̾� ��ü�� ���� ���, ���ظ� ������.
            collision.GetComponent<PlayerObject>()?.TakeDamage(attacker.Attack);
        }
    }

    // ������ ���� �� Ʈ���Ÿ� �����Ͽ� ��ü�� �ı��Ѵ�.
    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
