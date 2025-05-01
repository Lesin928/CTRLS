using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

// TODO:
// FIXME:
// NOTE:

/// <summary>
/// 적 오브젝트의 기본 동작을 정의하는 클래스.
/// 이동, 감지, 공격, 상태 전이 등의 기능을 포함함.
/// </summary>
public class EnemyObject : CharacterObject
{
    #region [Move Info]
    [Header("Move Info")]
    //public float moveSpeed;        // 현재 이동 속도
    public float defaultMoveSpeed; // 기본 이동 속도
    public float chaseSpeed;       // 추격 시 속도
    #endregion

    #region [Attack Info]
    [Header("Attack Info")]
    public float attackCooldown;                     // 공격 쿨타임 간격
    [HideInInspector] public float lastTimeAttacked; // 마지막 공격 시간
    #endregion

    #region [Collider Info]
    [Header("Collider Info")]
    [SerializeField] protected Transform groundCheck;     // 지면 감지 위치
    [SerializeField] protected float groundCheckDistance; // 지면 감지 거리
    [SerializeField] protected Transform wallCheck;       // 벽 감지 위치
    [SerializeField] protected float wallCheckDistance;   // 벽 감지 거리
    [SerializeField] protected Transform playerCheck;     // 플레이어 감지 위치
    [SerializeField] protected float playerCheckRadius;   // 플레이어 감지 반경
    public Transform attackCheck;                         // 공격 판정 위치
    public float attackCheckRadius;                       // 공격 판정 반경
    [Space]
    [SerializeField] protected LayerMask whatIsGround; // 지면 레이어
    [SerializeField] protected LayerMask whatIsPlayer; // 플레이어 레이어
    #endregion

    #region [Components]
    public Animator anim { get; private set; }  // 애니메이터 컴포넌트
    public Rigidbody2D rb { get; private set; } // Rigidbody2D 컴포넌트
    #endregion

    #region [StateMachine]
    public EnemyStateMachine stateMachine { get; private set; } // 스테이트 머신
    #endregion

    #region [State]
    public EnemyHitState hitState { get; private set; }   // 피격 상태
    public EnemyDeadState deadState { get; private set; } // 죽음 상태
    #endregion

    #region [Facing]
    public int facingDir { get; private set; } = 1; // 현재 바라보는 방향 (1: 오른쪽, -1: 왼쪽)
    protected bool facingRight = true;              // 오른쪽 바라보고 있는지 여부
    #endregion

    // 스테이트 머신 초기화
    protected virtual void Awake()
    {
        stateMachine = new EnemyStateMachine();

        // 상태 인스턴스를 초기화
        hitState = new EnemyHitState(this, stateMachine, "Hit");
        deadState = new EnemyDeadState(this, stateMachine, "Dead");
    }

    // 컴포넌트 초기화
    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // 현재 상태 갱신
    protected virtual void Update()
    {
        stateMachine.currentState?.Update();

        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(10);
        }
    }

    /// <summary>
    /// 플레이어 감지 상태 진입 시 호출됩니다.
    /// </summary>
    public virtual void EnterPlayerDetection()
    {
    }

    /// <summary>
    /// 플레이어 감지 상태 종료 시 호출됩니다.
    /// </summary>
    public virtual void ExitPlayerDetection()
    {
    }

    /// <summary>
    /// 공격(Attack) 상태로 전환을 요청합니다.
    /// </summary>
    public virtual void CallAttackState()
    {
    }

    /// <summary>
    /// 대기(Idle) 상태로 전환 요청을 합니다.
    /// </summary>
    public virtual void CallIdleState()
    {
    }

    #region [Animation Event]
    /// <summary>
    /// 애니메이션 이벤트가 종료되었음을 상태 머신에 알립니다.
    /// </summary>
    public virtual void AnimationFinishTrigger()
        => stateMachine.currentState?.AnimationFinishTrigger();
    #endregion

    #region [Detection]
    /// <summary>
    /// 지면이 감지되었는지 판단합니다.
    /// </summary>
    /// <returns>지면이 감지되면 true, 아니면 false를 반환합니다.</returns>
    public virtual bool IsGroundDetected()
        => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    /// <summary>
    /// 벽이 감지되었는지 판단합니다.
    /// </summary>
    /// <returns>벽이 감지되면 true, 아니면 false를 반환합니다.</returns>
    public virtual bool IsWallDetected()
        => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    /// <summary>
    /// 플레이어가 감지되었는지 검사합니다.
    /// </summary>
    /// <returns>플레이어와 충돌한 Collider2D 객체를 반환하거나, 감지되지 않은 경우 null을 반환합니다.</returns>
    public virtual Collider2D IsPlayerDetected()
        => Physics2D.OverlapCircle(playerCheck.position, playerCheckRadius, whatIsPlayer);

    /// <summary>
    /// 플레이어와 오브젝트 사이에 벽이 있는지 여부를 판단합니다.
    /// </summary>
    /// <returns>벽이 감지되면 true, 아니면 false를 반환합니다.</returns>
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
    /// 오브젝트의 방향을 전환합니다.
    /// (좌우 반전 수행)
    /// </summary>
    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    /// <summary>
    /// 입력된 x값에 따라 오브젝트의 방향을 전환합니다.
    /// </summary>
    /// <param name="xInput">이동 입력 값 (양수 -> 오른쪽, 음수 -> 왼쪽)</param>
    private void FlipController(float xInput)
    {
        if (xInput > 0 && !facingRight) Flip();
        else if (xInput < 0 && facingRight) Flip();
    }
    #endregion

    #region [Velocity Control]
    /// <summary>
    /// Rigidbody의 속도를 0으로 설정합니다.
    /// </summary>
    public void SetZeroVelocity()
    {
        rb.linearVelocity = Vector2.zero;
    }

    /// <summary>
    /// Rigidbody의 속도를 설정하고, 입력 값에 따라 방향을 조정합니다.
    /// </summary>
    /// <param name="xVelocity">X 축 속도 값</param>
    /// <param name="yVelocity">Y 축 속도 값</param>
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }
    #endregion

    #region [Gizmos]
    // 감지 및 공격 영역 시각화
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
