using UnityEngine;

/// <summary>
/// ������ �Ѿ��� ó���ϴ� Ŭ�����Դϴ�.
/// ���� �Ÿ� �ȿ� �÷��̾ �����ϸ� �ҿ뵹��(Vortex)�� �����մϴ�.
/// </summary>
public class EnemyBossBullet : MonoBehaviour
{
    // �ִϸ��̼� ��Ʈ�ѷ�
    protected Animator anim;

    // Rigidbody2D ������Ʈ
    protected Rigidbody2D rb;

    [Header("Settings")]
    [SerializeField] private float speed = 10f;       // �Ѿ� �ӵ�
    [SerializeField] private float lifeTime = 5f;     // �Ѿ��� ����
    [SerializeField] private GameObject vortexPrefab; // ������ �ҿ뵹�� ������

    private EnemyObject attacker; // �� �Ѿ��� �߻��� �� ������Ʈ

    private void Awake()
    {
        // �ִϸ����Ϳ� ������ٵ� ������Ʈ �ʱ�ȭ
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // ���� �ð� �� �Ѿ� ����
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        // �÷��̾���� x�� �Ÿ� ���
        float distanceX = Mathf.Abs(transform.position.x - PlayerManager.instance.player.transform.position.x);

        if (distanceX < 0.5f)
        {
            // �÷��̾� ��ó�� �ҿ뵹�� ����
            GameObject vortex = Instantiate(vortexPrefab, transform.position, Quaternion.identity);

            // �ҿ뵹�� ��ũ��Ʈ�� ������ ���� ����
            EnemyBossVortex vortexScript = vortex.GetComponent<EnemyBossVortex>();
            vortexScript.SetAttacker(attacker);

            // �Ѿ� ����
            DestroyTrigger();
        }
    }

    /// <summary>
    /// �� �Ѿ��� �߻��� ���� �����մϴ�.
    /// </summary>
    public void SetAttacker(EnemyObject enemy)
    {
        attacker = enemy;
    }

    /// <summary>
    /// ������ ��ġ�� �Ѿ��� �߻��մϴ�.
    /// </summary>
    /// <param name="targetPosition">�߻��� ��ǥ ��ġ</param>
    public void Shoot(Vector2 targetPosition)
    {
        // ���� ��ġ���� ��ǥ ��ġ������ ���� ���
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        // �ӵ� ����
        rb.linearVelocity = direction * speed;

        // �Ѿ� ȸ�� ���� ����
        RotateToVelocity(rb.linearVelocity);
    }

    // �ӵ� ���� ���⿡ ���� �Ѿ��� ȸ����Ű�� �Լ�
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
        if (collision.CompareTag("Player"))
        {
            // �浹 �ִϸ��̼� ����
            anim.SetBool("Hit", true);

            // �̵� ����
            rb.linearVelocity = Vector2.zero;

            // �浹 ��Ȱ��ȭ
            GetComponent<Collider2D>().enabled = false;

            // ���� ó��
            collision.GetComponent<PlayerObject>()?.TakeDamage(attacker.Attack);
        }
        else if (collision.CompareTag("Ground"))
        {
            // �浹 �ִϸ��̼� ����
            anim.SetBool("Hit", true);

            // �̵� ����
            rb.linearVelocity = Vector2.zero;

            // �浹 ��Ȱ��ȭ
            GetComponent<Collider2D>().enabled = false;
        }
    }

    // �Ѿ��� �����ϴ� �Լ�
    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
