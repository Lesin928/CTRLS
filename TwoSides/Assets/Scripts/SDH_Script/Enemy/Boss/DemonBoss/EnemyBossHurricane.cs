using UnityEngine;

/// <summary>
/// ������ Hurricane ���� ����Ʈ�� ó���ϴ� Ŭ�����Դϴ�.
/// �ִϸ��̼� �̺�Ʈ�� ���� Ȱ��ȭ�Ǹ�, �÷��̾�� �浹 �� ó�� ������ �����մϴ�.
/// </summary>
public class EnemyBossHurricane : MonoBehaviour
{
    [SerializeField] GameObject hurricanePrefab; // Hurricane ������
    [SerializeField] LayerMask groundMask;       // Ground ���̾�
    private EnemyObject attacker;                // ������ �߻��� �� ��ü
    private int facingDir;                       // ��ȯ ����

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
        this.facingDir = facingDir;
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

    // ���� Hurricane�� ��ȯ�ϴ� Ʈ���� (�ִϸ��̼� �̺�Ʈ���� ȣ��)
    private void SpawnHurricaneTrigger()
    {
        // �ݶ��̴� ���� ���� ���� ���� ���
        float prefabWidth = hurricanePrefab.GetComponent<BoxCollider2D>().size.x * 2;
        Vector3 offset = new Vector3(prefabWidth * facingDir, 0f, 0f);

        Vector3 spawnPos = transform.position + offset;

        // �ٴ� Ȯ�ο� Raycast (�Ҳ� �Ʒ� �������� ª�� �߻�)
        RaycastHit2D hit = Physics2D.Raycast(spawnPos, Vector2.down, 0.1f, groundMask);
        if (hit.collider == null)
            return; // ���� �ƴϸ� �������� ����

        GameObject hurricane = Instantiate(hurricanePrefab, spawnPos, Quaternion.identity); // Hurricane �������� �߻� ������ ����

        // Slash ��ü�� ��ũ��Ʈ�� �����ͼ� Ȱ��ȭ
        EnemyBossHurricane hurricaneScript = hurricane.GetComponent<EnemyBossHurricane>();
        hurricaneScript.SetAttacker(attacker); // �߻��� ����
        hurricaneScript.SetDirection(facingDir);
    }

    // �ִϸ��̼� �̺�Ʈ���� ȣ��Ǿ� ����Ʈ ������Ʈ�� �ı�
    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
