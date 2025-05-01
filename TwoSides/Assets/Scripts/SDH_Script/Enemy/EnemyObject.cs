using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

// TODO:
// FIXME:
// NOTE:

/// <summary>
/// �� ������Ʈ�� �⺻ ������ �����ϴ� Ŭ����.
/// �̵�, ����, ����, ���� ���� ���� ����� ������.
/// </summary>
public class EnemyObject : CharacterObject
{
    #region [Move Info]
    [Header("Move Info")]
    //public float moveSpeed;        // ���� �̵� �ӵ�
    public float defaultMoveSpeed; // �⺻ �̵� �ӵ�
    public float chaseSpeed;       // �߰� �� �ӵ�
    #endregion

    #region [Attack Info]
    [Header("Attack Info")]
    public float attackCooldown;                     // ���� ��Ÿ�� ����
    [HideInInspector] public float lastTimeAttacked; // ������ ���� �ð�
    #endregion

    #region [Collider Info]
    [Header("Collider Info")]
    [SerializeField] protected Transform groundCheck;     // ���� ���� ��ġ
    [SerializeField] protected float groundCheckDistance; // ���� ���� �Ÿ�
    [SerializeField] protected Transform wallCheck;       // �� ���� ��ġ
    [SerializeField] protected float wallCheckDistance;   // �� ���� �Ÿ�
    [SerializeField] protected Transform playerCheck;     // �÷��̾� ���� ��ġ
    [SerializeField] protected float playerCheckRadius;   // �÷��̾� ���� �ݰ�
    public Transform attackCheck;                         // ���� ���� ��ġ
    public float attackCheckRadius;                       // ���� ���� �ݰ�
    [Space]
    [SerializeField] protected LayerMask whatIsGround; // ���� ���̾�
    [SerializeField] protected LayerMask whatIsPlayer; // �÷��̾� ���̾�
    #endregion

    #region [Components]
    public Animator anim { get; private set; }  // �ִϸ����� ������Ʈ
    public Rigidbody2D rb { get; private set; } // Rigidbody2D ������Ʈ
    #endregion

    #region [StateMachine]
    public EnemyStateMachine stateMachine { get; private set; } // ������Ʈ �ӽ�
    #endregion

    #region [State]
    public EnemyHitState hitState { get; private set; }   // �ǰ� ����
    public EnemyDeadState deadState { get; private set; } // ���� ����
    #endregion

    #region [Facing]
    public int facingDir { get; private set; } = 1; // ���� �ٶ󺸴� ���� (1: ������, -1: ����)
    protected bool facingRight = true;              // ������ �ٶ󺸰� �ִ��� ����
    #endregion

    // ������Ʈ �ӽ� �ʱ�ȭ
    protected virtual void Awake()
    {
        stateMachine = new EnemyStateMachine();

        // ���� �ν��Ͻ��� �ʱ�ȭ
        hitState = new EnemyHitState(this, stateMachine, "Hit");
        deadState = new EnemyDeadState(this, stateMachine, "Dead");
    }

    // ������Ʈ �ʱ�ȭ
    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // ���� ���� ����
    protected virtual void Update()
    {
        stateMachine.currentState?.Update();

        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(10);
        }
    }

    /// <summary>
    /// �÷��̾� ���� ���� ���� �� ȣ��˴ϴ�.
    /// </summary>
    public virtual void EnterPlayerDetection()
    {
    }

    /// <summary>
    /// �÷��̾� ���� ���� ���� �� ȣ��˴ϴ�.
    /// </summary>
    public virtual void ExitPlayerDetection()
    {
    }

    /// <summary>
    /// ����(Attack) ���·� ��ȯ�� ��û�մϴ�.
    /// </summary>
    public virtual void CallAttackState()
    {
    }

    /// <summary>
    /// ���(Idle) ���·� ��ȯ ��û�� �մϴ�.
    /// </summary>
    public virtual void CallIdleState()
    {
    }

    #region [Animation Event]
    /// <summary>
    /// �ִϸ��̼� �̺�Ʈ�� ����Ǿ����� ���� �ӽſ� �˸��ϴ�.
    /// </summary>
    public virtual void AnimationFinishTrigger()
        => stateMachine.currentState?.AnimationFinishTrigger();
    #endregion

    #region [Detection]
    /// <summary>
    /// ������ �����Ǿ����� �Ǵ��մϴ�.
    /// </summary>
    /// <returns>������ �����Ǹ� true, �ƴϸ� false�� ��ȯ�մϴ�.</returns>
    public virtual bool IsGroundDetected()
        => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    /// <summary>
    /// ���� �����Ǿ����� �Ǵ��մϴ�.
    /// </summary>
    /// <returns>���� �����Ǹ� true, �ƴϸ� false�� ��ȯ�մϴ�.</returns>
    public virtual bool IsWallDetected()
        => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    /// <summary>
    /// �÷��̾ �����Ǿ����� �˻��մϴ�.
    /// </summary>
    /// <returns>�÷��̾�� �浹�� Collider2D ��ü�� ��ȯ�ϰų�, �������� ���� ��� null�� ��ȯ�մϴ�.</returns>
    public virtual Collider2D IsPlayerDetected()
        => Physics2D.OverlapCircle(playerCheck.position, playerCheckRadius, whatIsPlayer);

    /// <summary>
    /// �÷��̾�� ������Ʈ ���̿� ���� �ִ��� ���θ� �Ǵ��մϴ�.
    /// </summary>
    /// <returns>���� �����Ǹ� true, �ƴϸ� false�� ��ȯ�մϴ�.</returns>
    public bool IsWallBetweenPlayer()
    {
        Vector2 start = transform.position;
        Vector2 direction = (PlayerManager.instance.player.transform.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, PlayerManager.instance.player.transform.position);

        RaycastHit2D hit = Physics2D.Raycast(start, direction, distance, whatIsGround);
        return hit.collider != null;
    }
    #endregion

    #region [Flip]
    /// <summary>
    /// ������Ʈ�� ������ ��ȯ�մϴ�.
    /// (�¿� ���� ����)
    /// </summary>
    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    /// <summary>
    /// �Էµ� x���� ���� ������Ʈ�� ������ ��ȯ�մϴ�.
    /// </summary>
    /// <param name="xInput">�̵� �Է� �� (��� -> ������, ���� -> ����)</param>
    private void FlipController(float xInput)
    {
        if (xInput > 0 && !facingRight) Flip();
        else if (xInput < 0 && facingRight) Flip();
    }
    #endregion

    #region [Velocity Control]
    /// <summary>
    /// Rigidbody�� �ӵ��� 0���� �����մϴ�.
    /// </summary>
    public void SetZeroVelocity()
    {
        rb.linearVelocity = Vector2.zero;
    }

    /// <summary>
    /// Rigidbody�� �ӵ��� �����ϰ�, �Է� ���� ���� ������ �����մϴ�.
    /// </summary>
    /// <param name="xVelocity">X �� �ӵ� ��</param>
    /// <param name="yVelocity">Y �� �ӵ� ��</param>
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }
    #endregion

    #region [Gizmos]
    // ���� �� ���� ���� �ð�ȭ
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * wallCheckDistance * facingDir);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(playerCheck.position, playerCheckRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);

        Gizmos.color = Color.blue;
        if (PlayerManager.instance != null && PlayerManager.instance.player != null)
            Gizmos.DrawLine(transform.position, PlayerManager.instance.player.transform.position);
    }
    #endregion

    public override void TakeDamage(float _damage)
    {
        currentHp -= (float)((Mathf.Pow(_damage, 2f) / ((double)armor + (double)_damage)));

        if (currentHp <= 0)
        {
            currentHp = 0;
            stateMachine.ChangeState(deadState);
        }
        else
        {
            stateMachine.ChangeState(hitState);
        }
    }

    public override void Die()
    {

    }
}
