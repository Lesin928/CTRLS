using UnityEngine;

/// <summary>
/// ���� �Ѿ� ������Ʈ�� ó���ϴ� Ŭ�����Դϴ�.
/// �����κ��� �߻�ǰ�, �÷��̾ ���� �̵��ϸ�, �浹 �� �ǰ� �ִϸ��̼��� ����ϰ� ���ŵ˴ϴ�.
/// </summary>
public class EnemyBossCircleBullet : MonoBehaviour
{
    // �ִϸ����� �� Rigidbody2D ������Ʈ
    protected Animator anim;
    protected Rigidbody2D rb;

    [Header("Settings")]
    [SerializeField] private float speed = 10f;   // �Ѿ� �̵� �ӵ�
    [SerializeField] private float lifeTime = 5f; // �Ѿ� ����

    private EnemyObject attacker; // �߻��� �� ��ü

    private void Awake()
    {
        // �ִϸ����Ϳ� ������ٵ� ������Ʈ �ʱ�ȭ
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // ���� �ð��� ������ �Ѿ� ����
        Destroy(gameObject, lifeTime);
    }

    /// <summary>
    /// �� �Ѿ��� �߻��� ���� �����մϴ�.
    /// </summary>
    public void SetAttacker(EnemyObject enemy)
    {
        attacker = enemy;
    }

    // �÷��̾ ���� �Ѿ��� �߻��ϴ� �޼��� (�ִϸ��̼� �̺�Ʈ�� ȣ��)
    private void Shoot()
    {
        Vector2 targetPosition = PlayerManager.instance.player.transform.position;

        // ���� ��ġ���� �÷��̾� ��ġ���� ���� ���
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        // ����� �ӵ��� ���Ͽ� �̵� ����
        rb.linearVelocity = direction * speed;

        // �Ѿ� ȸ�� ���� ����
        RotateToVelocity(rb.linearVelocity);
    }

    // �Ѿ��� ���ư��� �������� ȸ����Ű�� �޼���
    private void RotateToVelocity(Vector2 velocity)
    {
        if (velocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    // �浹 ó�� �Լ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾�� �浹���� ��
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("Hit", true); // �ǰ� �ִϸ��̼� ���
            rb.linearVelocity = Vector2.zero; // �̵� ����
            GetComponent<Collider2D>().enabled = false; // �浹 ��Ȱ��ȭ

            // �÷��̾�� ���� ����
            collision.GetComponent<PlayerObject>()?.TakeDamage(attacker.Attack);
        }
        // �ٴڰ� �浹���� ��
        else if (collision.CompareTag("Ground"))
        {
            anim.SetBool("Hit", true); // �ǰ� �ִϸ��̼� ���
            rb.linearVelocity = Vector2.zero; // �̵� ����
            GetComponent<Collider2D>().enabled = false; // �浹 ��Ȱ��ȭ
        }
    }

    // �ִϸ��̼� �̺�Ʈ���� ȣ��Ǿ� �Ѿ��� �����ϴ� �޼���
    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
