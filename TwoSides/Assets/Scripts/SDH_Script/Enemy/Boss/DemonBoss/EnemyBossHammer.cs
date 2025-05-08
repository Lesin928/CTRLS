using UnityEngine;

/// <summary>
/// ���� ���� �ֵθ��� �ظ� ���� ������ ����ϴ� Ŭ�����Դϴ�.
/// �ִϸ��̼� �̺�Ʈ�� ���� ���� Ÿ�ֿ̹� Ȱ��ȭ�Ǹ�, 
/// �÷��̾�� �浹 �� �������� �ְ�, ����̵� ����Ʈ�� ������ �� �ֽ��ϴ�.
/// </summary>
public class EnemyBossHammer : MonoBehaviour
{
    [SerializeField] GameObject hurricanePrefab; // ������ �㸮���� ������
    private EnemyObject attacker;                // �� �ظӸ� ����ϴ� ���� ��

    private void Start()
    {
        // ���� �� �ݶ��̴� ��Ȱ��ȭ
        GetComponent<Collider2D>().enabled = false;
    }

    /// <summary>
    /// �ظ��� ������ ������ �����մϴ�.
    /// </summary>
    public void SetAttacker(EnemyObject enemy)
    {
        attacker = enemy;
    }

    // ���� Ȱ��ȭ (�ִϸ��̼� �̺�Ʈ���� ȣ���)
    private void EnableAttack()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    // ���� ��Ȱ��ȭ (�ִϸ��̼� �̺�Ʈ���� ȣ���)
    private void DisableAttack()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    /// <summary>
    /// ������ �ٶ󺸴� ���⿡ ���� �ظ��� ������ �����մϴ�.
    /// </summary>
    /// <param name="facingDir">������ �ٶ󺸴� ���� (1 �Ǵ� -1)</param>
    public void SetDirection(int facingDir)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * facingDir;
        transform.localScale = scale;
    }

    // �÷��̾�� �浹 �� �������� ��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // �÷��̾�� ������ �ο�
            collision.GetComponent<PlayerObject>()?.TakeDamage(attacker.Attack);
        }
    }

    // �㸮���� ����Ʈ ���� (�ִϸ��̼� �̺�Ʈ�� ���� ȣ��)
    private void SpawnHurricaneTrigger()
    {
        // ���� ��ġ�� �㸮���� ������ ����
        GameObject hurricane = Instantiate(hurricanePrefab, transform.position, Quaternion.identity);

        // �㸮���ο� ������ ���� �� ���� ����
        EnemyBossHurricane hurricaneScript = hurricane.GetComponent<EnemyBossHurricane>();
        hurricaneScript.SetAttacker(attacker);
        hurricaneScript.SetDirection(attacker.facingDir);
    }

    // �ظ� ������Ʈ ���� (�ִϸ��̼� �̺�Ʈ�� ���� ȣ���)
    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
