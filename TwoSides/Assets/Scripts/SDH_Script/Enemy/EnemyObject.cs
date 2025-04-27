using System.Collections;
using UnityEngine;

// TODO:
// FIXME:
// NOTE:

/// <summary>
/// 
/// </summary>
public class EnemyObject : MonoBehaviour
{
    #region [Move Info]
    [Header("Move Info")]
    public float moveSpeed;
    public float defaultMoveSpeed;
    public float chaseSpeed;
    #endregion

    #region [Attack Info]
    [Header("Attack Info")]
    public float attackCooldown;
    [HideInInspector] public float lastTimeAttacked;
    #endregion

    #region [Collider Info]
    [Header("Collider Info")]
    [SerializeField] private Transform player;
    [Space]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected Transform playerCheck;
    [SerializeField] protected float playerCheckRadius;
    public Transform attackCheck;
    public float attackCheckRadius;
    [Space]
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected LayerMask whatIsPlayer;
    #endregion

    #region [Components]
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EnemyStateMachine stateMachine { get; private set; }
    #endregion

    #region [Facing]
    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;
    #endregion

    protected virtual void Awake()
    {
        stateMachine = new EnemyStateMachine();
    }

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        stateMachine.currentState?.Update();
    }

    public virtual void EnterPlayerDetection()
    {
    }

    public virtual void ExitPlayerDetection()
    {
    }

    public virtual void CallAttackState()
    {
    }

    public virtual void CallIdleState()
    {
    }

    #region [Animation Event]
    public virtual void AnimationFinishTrigger()
        => stateMachine.currentState?.AnimationFinishTrigger();
    #endregion

    #region [Detection]
    public virtual bool IsGroundDetected()
        => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    public virtual bool IsWallDetected()
        => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    public virtual Collider2D IsPlayerDetected()
        => Physics2D.OverlapCircle(playerCheck.position, playerCheckRadius, whatIsPlayer);

    public bool IsWallBetweenPlayer()
    {
        Vector2 start = transform.position;
        Vector2 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.position);

        RaycastHit2D hit = Physics2D.Raycast(start, direction, distance, whatIsGround);

        return hit.collider != null;
    }
    #endregion

    #region [Gizmos]
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
        Gizmos.DrawLine(transform.position, player.position);
    }
    #endregion

    #region [Flip]
    public virtual void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public virtual void FlipController(float xInput)
    {
        if (xInput > 0 && !facingRight) Flip();
        else if (xInput < 0 && facingRight) Flip();
    }
    #endregion

    #region [Velocity Control]
    public void SetZeroVelocity()
    {
        rb.linearVelocity = Vector2.zero;
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }
    #endregion
}