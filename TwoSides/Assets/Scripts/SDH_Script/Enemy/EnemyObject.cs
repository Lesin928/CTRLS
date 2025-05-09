using UnityEngine;

/// <summary>
/// 이 클래스는 적 캐릭터의 이동, 공격, 충돌 처리, 상태 관리 등을 처리합니다.
/// 적의 상태는 상태 머신으로 관리되며, 기본적인 이동, 추적, 공격 등의 동작을 포함합니다.
/// </summary>
public class EnemyObject : CharacterObject
{
    #region [Move Info]
    [Header("Move Info")]
    public float defaultMoveSpeed; // 기본 이동 속도
    public float chaseSpeed;       // 추적 이동 속도
    #endregion

    #region [Attack Info]
    [Header("Attack Info")]
    public float attackCooldown;                     // 공격 쿨타임
    [HideInInspector] public float lastTimeAttacked; // 마지막으로 공격한 시간
    #endregion

    #region [Collider Info]
    [Header("Collider Info")]
    [SerializeField] protected Transform groundCheck;     // 땅을 체크하는 위치
    [SerializeField] protected float groundCheckDistance; // 땅 체크 거리
    [Space]
    [SerializeField] protected Transform wallCheck;       // 벽을 체크하는 위치
    [SerializeField] protected float wallCheckDistance;   // 벽 체크 거리
    [Space]
    [SerializeField] protected Transform playerCheck;     // 플레이어를 체크하는 위치
    [SerializeField] protected float playerCheckRadius;   // 플레이어 체크 반지름
    [Space]
    public Transform attackCheck;                         // 공격 범위 체크 위치
    public float attackCheckRadius;                       // 공격 범위 체크 반지름
    [Space]
    [SerializeField] protected LayerMask whatIsGround;    // 땅 레이어 마스크
    [SerializeField] protected LayerMask whatIsWall;      // 벽 레이어 마스크
    [SerializeField] protected LayerMask whatIsPlayer;    // 플레이어 레이어 마스크
    public float colliderWidth { get; private set; }      // 콜라이더의 너비
    #endregion

    #region [Flash Effect]
    [Header("Flash Effect")]
    [SerializeField] private EnemyFlashEffect flashEffect; // 적의 플래시 이펙트
    #endregion

    #region [Components]
    public Animator anim { get; private set; }  // 적 캐릭터의 애니메이터
    public Rigidbody2D rb { get; private set; } // 적 캐릭터의 Rigidbody2D
    public Collider2D col { get; private set; } // 적 캐릭터의 Collider2D
    #endregion

    #region [StateMachine]
    public EnemyStateMachine stateMachine { get; private set; } // 적의 상태 머신
    #endregion

    #region [State]
    public EnemyHitState hitState { get; private set; }   // 적이 맞은 상태
    public EnemyDeadState deadState { get; private set; } // 적이 죽은 상태
    #endregion

    #region [Facing]
    public int facingDir { get; private set; } = 1; // 적의 방향 (1: 오른쪽, -1: 왼쪽)
    protected bool facingRight = true;              // 적이 오른쪽을 보고 있는지 여부
    #endregion

    #region [Audio]
    private EnemySoundTrigger soundTrigger; // 적의 소리 트리거
    #endregion


    // 상태 머신 초기화 및 상태 설정
    protected virtual void Awake()
    {
        stateMachine = new EnemyStateMachine();

        // 적의 Hit 상태와 Dead 상태를 초기화
        hitState = new EnemyHitState(this, stateMachine, "Hit");
        deadState = new EnemyDeadState(this, stateMachine, "Dead");
    }

    // 필요한 컴포넌트들 초기화
    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>(); // 애니메이션 컴포넌트 초기화
        rb = GetComponent<Rigidbody2D>(); // 리지드바디 컴포넌트 초기화
        flashEffect = GetComponentInChildren<EnemyFlashEffect>(); // 플래시 효과 컴포넌트 초기화
        col = GetComponent<Collider2D>(); // 콜라이더 컴포넌트 초기화
        soundTrigger = GetComponentInChildren<EnemySoundTrigger>(); // 소리 트리거 컴포넌트 초기화
        colliderWidth = col.bounds.size.x; // 콜라이더의 가로 길이 저장
    }

    // 상태 머신 업데이트
    protected virtual void Update()
    {
        stateMachine.currentState?.Update(); // 현재 상태의 업데이트 호출
    }

    /// <summary>
    /// 플레이어 탐지 상태로 진입할 때 호출되는 함수
    /// </summary>
    public virtual void EnterPlayerDetection()
    {
    }

    /// <summary>
    /// 플레이어 탐지 상태에서 벗어날 때 호출되는 함수
    /// </summary>
    public virtual void ExitPlayerDetection()
    {
    }

    /// <summary>
    /// 공격 상태로 전환하는 함수
    /// </summary>
    public virtual void CallAttackState()
    {
    }

    /// <summary>
    /// 대기 상태로 전환하는 함수
    /// </summary>
    public virtual void CallIdleState()
    {
    }

    public virtual void CallChaseState()
    {
    }

    #region [Animation Event]
    /// <summary>
    /// 애니메이션이 끝났을 때 호출되는 함수
    /// </summary>
    public virtual void AnimationFinishTrigger()
        => stateMachine.currentState?.AnimationFinishTrigger();
    #endregion

    #region [Detection]
    /// <summary>
    /// 바닥을 감지하는 함수
    /// </summary>
    /// <returns>바닥이 감지되면 true, 아니면 false</returns>
    public virtual bool IsGroundDetected()
        => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround).collider != null;

    /// <summary>
    /// 벽을 감지하는 함수
    /// </summary>
    /// <returns>벽이 감지되면 true, 아니면 false</returns>
    public virtual bool IsWallDetected()
        => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsWall).collider != null;

    /// <summary>
    /// 플레이어를 감지하는 함수
    /// </summary>
    /// <returns>플레이어가 감지되면 해당 Collider2D를 반환, 아니면 null</returns>
    public virtual Collider2D IsPlayerDetected()
        => Physics2D.OverlapCircle(playerCheck.position, playerCheckRadius, whatIsPlayer);

    /// <summary>
    /// 공격이 가능한 범위에 플레이어가 있는지 감지하는 함수
    /// </summary>
    /// <returns>플레이어가 감지되면 해당 Collider2D를 반환, 아니면 null</returns>
    public virtual Collider2D IsAttackDetectable()
        => Physics2D.OverlapCircle(attackCheck.position, attackCheckRadius, whatIsPlayer);

    /// <summary>
    /// 벽과 플레이어 사이에 장애물이 있는지 확인하는 함수
    /// </summary>
    /// <returns>벽 사이에 플레이어가 있으면 true, 아니면 false</returns>
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
    /// 적의 방향을 반전시키는 함수
    /// </summary>
    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0); // 객체를 Y축을 기준으로 180도 회전
    }

    // 입력 값에 따라 적의 방향을 전환하는 함수
    private void FlipController(float xInput)
    {
        if (xInput > 0 && !facingRight) Flip();
        else if (xInput < 0 && facingRight) Flip();
    }
    #endregion

    #region [Velocity Control]
    /// <summary>
    /// Rigidbody의 속도를 0으로 설정하는 함수
    /// </summary>
    public void SetZeroVelocity()
    {
        rb.linearVelocity = Vector2.zero;
    }

    /// <summary>
    /// Rigidbody의 속도를 설정하는 함수
    /// </summary>
    /// <param name="xVelocity">X축 속도</param>
    /// <param name="yVelocity">Y축 속도</param>
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity); // X축 입력 값에 따라 적의 방향을 전환
    }
    #endregion

    #region [Gizmos]
    // Gizmos로 각종 감지 범위와 경로를 시각적으로 표시
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

    /// <summary>
    /// 적이 피해를 받을 때 호출되는 함수
    /// </summary>
    /// <param name="_damage">입력된 피해 값</param>
    public override void TakeDamage(float _damage)
    {
        // 피해 값에 따라 현재 체력 감소
        CurrentHp -= (float)((Mathf.Pow(_damage, 2f) / ((double)Armor + (double)_damage)));

        if (soundTrigger != null)
            soundTrigger.PlayHitSound(); // 맞을 때 소리 재생

        flashEffect.Flash(); // 플래시 효과 실행

        if (CurrentHp <= 0)  // 체력이 0 이하로 떨어지면 죽음 상태로 전환
        {
            CurrentHp = 0;
            stateMachine.ChangeState(deadState);
        }
        else // 체력이 남아 있으면 히트 상태로 전환
        {
            if (!stateMachine.isAttacking)
                stateMachine.ChangeState(hitState);
        }
    }

    // 적이 죽었을 때 호출되는 함수 (EnemyObject에서는 사용 X)
    protected override void Die()
    {
    }

}
