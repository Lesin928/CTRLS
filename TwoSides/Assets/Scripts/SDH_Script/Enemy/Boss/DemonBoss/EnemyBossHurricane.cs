using UnityEngine;

/// <summary>
/// ������ �㸮���� ������ ó���ϴ� Ŭ�����Դϴ�.
/// �㸮���� ���� �� ���� ó���� ����մϴ�.
/// </summary>
public class EnemyBossHurricane : MonoBehaviour
{
    [SerializeField] GameObject hurricanePrefab; // �㸮���� ������
    [SerializeField] LayerMask groundMask;       // �� ���̾�
    private EnemyObject attacker;                // ������ ��ü
    private int facingDir;                       // �ٶ󺸴� ����

    private void Start()
    {
        // ���� �� �ݶ��̴� ��Ȱ��ȭ
        GetComponent<Collider2D>().enabled = false;
    }

    /// <summary>
    /// ������ ��ü�� �����մϴ�.
    /// </summary>
    public void SetAttacker(EnemyObject enemy)
    {
        attacker = enemy;
    }

    /// <summary>
    /// �����ڰ� �ٶ󺸴� ������ �����մϴ�.
    /// </summary>
    /// <param name="facingDir">�ٶ󺸴� ���� (1�̸� ������, -1�̸� ����)</param>
    public void SetDirection(int facingDir)
    {
        this.facingDir = facingDir;
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * facingDir; // ���⿡ �°� ������ ����
        transform.localScale = scale;
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

    // �浹 ó�� �Լ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // �÷��̾ �㸮���ο� ������ ���ظ� ����
            collision.GetComponent<PlayerObject>()?.TakeDamage(attacker.Attack);
        }
    }

    // �㸮���� ���� �� �ߵ�
    // ���� �㸮���� ������ ���� �Լ� (�ִϸ��̼� �̺�Ʈ���� ȣ��)
    private void SpawnHurricaneTrigger()
    {
        // �㸮���� �������� �ʺ� ���ϰ�, ���⿡ �°� ���� ��ġ�� ���
        float prefabWidth = hurricanePrefab.GetComponent<BoxCollider2D>().size.x * 2;
        Vector3 offset = new Vector3(prefabWidth * facingDir, 0f, 0f);

        Vector3 spawnPos = transform.position + offset;

        // ���� ��ġ �Ʒ��� ���� �ִ��� üũ
        RaycastHit2D hit = Physics2D.Raycast(spawnPos, Vector2.down, 0.1f, groundMask);
        if (hit.collider == null)
            return; // ���� ������ �㸮���� �������� ����

        // �㸮���� ����
        GameObject hurricane = Instantiate(hurricanePrefab, spawnPos, Quaternion.identity);

        // ������ �㸮���ο� �����ڿ� ���� ����
        EnemyBossHurricane hurricaneScript = hurricane.GetComponent<EnemyBossHurricane>();
        hurricaneScript.SetAttacker(attacker); // ������ ����
        hurricaneScript.SetDirection(facingDir); // ���� ����
    }

    // �㸮���� ��ü ����
    private void DestroyTrigger()
    {
        Destroy(gameObject); // �ڽ��� ����
    }
}
