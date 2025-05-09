using UnityEngine;

/// <summary>
/// ���� �Ѿ��� �����ϴ� Ŭ�����̴�.
/// �Ѿ��� �߻� �� ��ǥ ��ġ�� ���� �����ϸ�, �浹 �� ���̳� ���� ���ظ� ������.
/// </summary>
public class EnemyBullet : MonoBehaviour
{
    // �ִϸ����Ϳ� ������ٵ�2D ������Ʈ
    protected Animator anim;  // �ִϸ�����
    protected Rigidbody2D rb; // ������ٵ�2D

    [Header("Settings")]
    [SerializeField] private float speed = 10f;   // �Ѿ��� ���� �ӵ�
    [SerializeField] private float lifeTime = 5f; // �Ѿ��� ���� �ð�

    private EnemyObject attacker; // �Ѿ��� �߻��� ��

    private void Awake()
    {
        // �ִϸ����Ϳ� ������ٵ�2D ������Ʈ�� �����´�.
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // ���� �ð��� ���� �� �Ѿ��� �ı��Ѵ�.
        Destroy(gameObject, lifeTime);
    }

    /// <summary>
    /// �� ������Ʈ�� �����Ѵ�.
    /// </summary>
    public void SetAttacker(EnemyObject enemy)
    {
        attacker = enemy;
    }

    /// <summary>
    /// �Ѿ��� �߻��Ѵ�.
    /// </summary>
    /// <param name="targetPosition">��ǥ ��ġ</param>
    public void Shoot(Vector2 targetPosition)
    {
        // ��ǥ ��ġ�� ���ϴ� ������ ���Ѵ�.
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        // ���⿡ �ӵ��� ���� �Ѿ��� �ӵ��� �����Ѵ�.
        rb.linearVelocity = direction * speed;

        // �Ѿ��� �̵��ϴ� ������ �����.
        RotateToVelocity(rb.linearVelocity);
    }

    // �Ѿ��� ���⿡ �°� ȸ���ϴ� �Լ�
    private void RotateToVelocity(Vector2 velocity)
    {
        if (velocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    /// <summary>
    /// �浹 �߻� �� ȣ��Ǵ� �Լ�
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾�� �浹�� ���
        if (collision.CompareTag("Player"))
        {
            // �浹 �ִϸ��̼��� �����Ѵ�.
            anim.SetBool("Hit", true);

            // �Ѿ��� �ӵ��� 0���� �����Ͽ� �����.
            rb.linearVelocity = Vector2.zero;

            // Collider2D�� ��Ȱ��ȭ�Ͽ� �浹 ó���� ���´�.
            GetComponent<Collider2D>().enabled = false;

            // �÷��̾�� ���ظ� ������.
            collision.GetComponent<PlayerObject>()?.TakeDamage(attacker.Attack);
        }

        // ���� �浹�� ���
        else if (collision.CompareTag("Ground"))
        {
            // �浹 �ִϸ��̼��� �����Ѵ�.
            anim.SetBool("Hit", true);

            // �Ѿ��� �ӵ��� 0���� �����Ͽ� �����.
            rb.linearVelocity = Vector2.zero;

            // Collider2D�� ��Ȱ��ȭ�Ͽ� �浹 ó���� ���´�.
            GetComponent<Collider2D>().enabled = false;
        }
    }

    // �Ѿ��� �浹 �� ���� �ð� ������ �ı��Ǵ� �Լ�
    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
