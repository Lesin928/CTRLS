using UnityEngine;

/// <summary>
/// ���� ȭ���� �߻��ϴ� ��ũ��Ʈ�Դϴ�.
/// ȭ���� ��ǥ �������� ������ ������ � �����ϸ�, ��ǥ�� �浹�ϸ� ���ظ� ������, ���� �ð��� ������ �ڵ����� �����˴ϴ�.
/// </summary>
public class EnemyArrow : MonoBehaviour
{
    private Rigidbody2D rb; // Rigidbody2D ���� ���
    private Collider2D cd;  // Collider2D ���� ���

    [Header("Settings")]
    [SerializeField] private float gravityScale = 1f;     // �߷��� ����
    [SerializeField] private float arcHeightRatio = 0.2f; // � ���� ���� (�⺻ 20%)
    [SerializeField] private float flightTime = 1f;       // ���� �ð�
    [SerializeField] private float lifeTime = 5f;         // ȭ���� ���� �ð�

    private EnemyObject attacker;

    private void Awake()
    {
        // �ʱ�ȭ: Rigidbody2D�� Collider2D ������Ʈ ��������
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        // ���� �� ȭ���� �ӵ��� ���� ȸ��
        RotateToVelocity(rb.linearVelocity);
    }

    /// <summary>
    /// �� ��ü�� �����մϴ�.
    /// </summary>
    public void SetAttacker(EnemyObject enemy)
    {
        attacker = enemy;
    }

    /// <summary>
    /// ��ǥ ��ġ�� �������� ȭ���� �߻��մϴ�.
    /// �־��� �ð� ���� � ������ �ϵ��� �����մϴ�.
    /// </summary>
    /// <param name="targetPosition">��ǥ ��ġ</param>
    public void Shoot(Vector2 targetPosition)
    {
        Vector2 startPosition = transform.position;
        float gravity = Mathf.Abs(Physics2D.gravity.y * gravityScale); // �߷� ���
        float distanceX = targetPosition.x - startPosition.x;
        float distanceY = targetPosition.y - startPosition.y;

        // ���� �ӵ� ���
        float initialVx = distanceX / flightTime;

        // � ���� ������ ���� ���� �ӵ� ���
        float arcHeight = Mathf.Abs(distanceX) * arcHeightRatio;

        // ���� �ӵ� ���
        float initialVy = Mathf.Sqrt(2 * gravity * arcHeight);

        // ���� �ð��� ����� ���� ���� �ӵ� ����
        float totalTime = (2 * initialVy) / gravity;
        initialVy = (gravity * flightTime) / 2f;  // ���� �ӵ� ����

        // �ʱ� �ӵ� ���� ����
        Vector2 velocity = new Vector2(initialVx, initialVy);

        rb.gravityScale = gravityScale;
        rb.linearVelocity = velocity;

        // ȭ�� ȸ��
        RotateToVelocity(velocity);
    }

    // ȭ���� �̵��ϴ� ���⿡ ���� ȸ��
    private void RotateToVelocity(Vector2 velocity)
    {
        if (velocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    // �浹�� �߻��ϸ� ������ ���ظ� ������ ȭ���� �����ϴ� �Լ�
    // ȭ���� ���� �ð� �� �ı��ǵ��� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾�� �浹 ��
        if (collision.CompareTag("Player"))
        {
            cd.enabled = false;

            // ȭ���� Kinematic���� �����Ͽ� ���� ��Ģ�� ���� �̵����� �ʵ��� ����
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            // ȭ���� �÷��̾��� �ڽ� ��ü�� �ǵ��� ����
            transform.parent = collision.transform;

            // �÷��̾�� ���ظ� ����
            collision.GetComponent<PlayerObject>()?.TakeDamage(attacker.Attack);
        }

        // ���� �ð��� ������ ȭ�� �ı�
        Destroy(gameObject, lifeTime);
    }
}
