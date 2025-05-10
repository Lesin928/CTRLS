using UnityEngine;

/// <summary>
/// EnemyObject는 적 캐릭터의 기본 동작과 속성을 정의하는 클래스입니다.
/// 이 클래스는 이동, 공격, 충돌 감지, 애니메이션 및 상태 관리 기능을 포함합니다.
/// </summary>
public class EnemyObject : CharacterObject
{
    #region [Move Info]
    [Header("Move Info")]
    public float defaultMoveSpeed; // 기본 이동 속도
    public float chaseSpeed;       // 추적 속도
    #endregion

    #region [Attack Info]
    [Header("Attack Info")]
    public float attackCooldown;                     // 공격 쿨타임
    [HideInInspector] public float lastTimeAttacked; // 마지막으로 공격한 시간
    #endregion

    #region [Collider Info]
    [Header("Collider Info")]
    [SerializeField] protected Transform groundCheck;     // 바닥 감지 위치
    [SerializeField] protected float groundCheckDistance; // 바닥 감지 거리
    [Space]
    [SerializeField] protected Transform wallCheck;       // 벽 감지 위치
    [SerializeField] protected float wallCheckDistance;   // 벽 감지 거리
    [Space]
    [SerializeField] protected Transform playerCheck;     // 플레이어 감지 위치
    [SerializeField] protected float playerCheckRadius;   // 플레이어 감지 반경
    [Space]
    public Transform attackCheck;                         // 공격 감지 위치
    public float attackCheckRadius;                       // 공격 감지 반경
    [Space]
    [SerializeField] protected LayerMask whatIsGround;    // 바닥 레이어
    [SerializeField] protected LayerMask whatIsWall;      // 벽 레이어
    [SerializeField] protected LayerMask whatIsPlayer;    // 플레이어 레이어
    public float colliderWidth { get; private set; }      // Collider의 너비
    #endregion

    #region [Flash Effect]
    [Header("Flash Effect")]
    [SerializeField] private EnemyFlashEffect flashEffect; // 적 캐릭터의 플래시 이펙트
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
    public EnemyHitState hitState { get; private set; }   // 적의 피격 상태
    public EnemyDeadState deadState { get; private set; } // 적의 사망 상태
    #endregion

    #region [Facing]
    public int facingDir { get; private set; } = 1; // 적이 바라보는 방향 (1: 오른쪽, -1: 왼쪽)
    protected bool facingRight = true;              // 적이 오른쪽을 보고 있는지 여부
    #endregion

    #region [Audio]
    private EnemySoundTrigger soundTrigger; // 적의 소리 트리거
    #endregion

    // Enemy 상태 머신을 초기화하고, Hit 및 Dead 상태를 설정합니다.
    protected virtual void Awake()
    {
        stateMachine = new EnemyStateMachine(); // 적의 상태 머신 초기화

        // Hit 상태와 Dead 상태를 정의합니다.
        hitState = new EnemyHitState(this, stateMachine, "Hit"); // 적이 피격되었을 때의 상태
        deadState = new EnemyDeadState(this, stateMachine, "Dead"); // 적이 사망했을 때의 상태
    }

    // 각 컴포넌트를 초기화하고 필요한 요소를 설정합니다.
    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>(); // 자식 객체에서 Animator 컴포넌트를 가져옵니다.
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트를 가져옵니다.
        flashEffect = GetComponentInChildren<EnemyFlashEffect>(); // 자식 객체에서 EnemyFlashEffect 컴포넌트를 가져옵니다.
        col = GetComponent<Collider2D>(); // Collider2D 컴포넌트를 가져옵니다.
        soundTrigger = GetComponentInChildren<EnemySoundTrigger>(); // 자식 객체에서 EnemySoundTrigger 컴포넌트를 가져옵니다.
        colliderWidth = col.bounds.size.x; // Collider의 너비를 가져옵니다.
    }

    // 상태 머신의 현재 상태를 업데이트합니다.
    protected virtual void Update()
    {
        stateMachine.currentState?.Update(); // 현재 상태가 존재하면 업데이트 합니다.
    }

    /// <summary>
    /// 플레이어를 감지한 후 실행할 동작을 정의합니다.
    /// </summary>
    public virtual void EnterPlayerDetection()
    {
        // 플레이어를 감지했을 때 실행할 로직을 여기에 구현합니다.
    }

    /// <summary>
    /// 플레이어의 감지가 종료되었을 때 실행할 동작을 정의합니다.
    /// </summary>
    public virtual void ExitPlayerDetection()
    {
        // 플레이어 감지가 종료되었을 때 실행할 로직을 여기에 구현합니다.
    }

    /// <summary>
    /// 공격 상태로 전환합니다.
    /// </summary>
    public virtual void CallAttackState()
    {
        // 공격 상태로 전환할 로직을 여기에 구현합니다.
    }

    /// <summary>
    /// 대기 상태로 전환합니다.
    /// </summary>
    public virtual void CallIdleState()
    {
        // 대기 상태로 전환할 로직을 여기에 구현합니다.
    }

    /// <summary>
    /// 추적 상태로 전환합니다.
    /// </summary>
    public virtual void CallChaseState()
    {
        // 추적 상태로 전환할 로직을 여기에 구현합니다.
    }


    #region [Animation Event]
    /// <summary>
    /// 애니메이션이 종료될 때 호출되는 트리거 함수입니다.
    /// </summary>
    public virtual void AnimationFinishTrigger()
        => stateMachine.currentState?.AnimationFinishTrigger(); // 현재 상태의 애니메이션 종료 트리거 호출
    #endregion

    #region [Detection]
    /// <summary>
    /// 지면을 감지하는 함수입니다.
    /// </summary>
    /// <returns>지면이 감지되면 true, 아니면 false를 반환합니다.</returns>
    public virtual bool IsGroundDetected()
        => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround).collider != null; // Raycast로 지면을 감지

    /// <summary>
    /// 벽을 감지하는 함수입니다.
    /// </summary>
    /// <returns>벽이 감지되면 true, 아니면 false를 반환합니다.</returns>
    public virtual bool IsWallDetected()
        => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsWall).collider != null; // Raycast로 벽을 감지

    /// <summary>
    /// 플레이어를 감지하는 함수입니다.
    /// </summary>
    /// <returns>플레이어가 감지되면 해당 Collider2D 객체를 반환, 아니면 null을 반환합니다.</returns>
    public virtual Collider2D IsPlayerDetected()
        => Physics2D.OverlapCircle(playerCheck.position, playerCheckRadius, whatIsPlayer); // 플레이어를 원형 범위 내에서 감지

    /// <summary>
    /// 공격이 가능한 상태인지 감지하는 함수입니다.
    /// </summary>
    /// <returns>공격이 가능한 상태에서 해당 Collider2D 객체를 반환, 아니면 null을 반환합니다.</returns>
    public virtual Collider2D IsAttackDetectable()
        => Physics2D.OverlapCircle(attackCheck.position, attackCheckRadius, whatIsPlayer); // 공격 범위 내에서 플레이어를 감지

    /// <summary>
    /// 플레이어와 벽 사이에 장애물이 있는지 감지하는 함수입니다.
    /// </summary>
    /// <returns>플레이어와 벽 사이에 장애물이 있으면 true, 없으면 false를 반환합니다.</returns>
    public bool IsWallBetweenPlayer()
    {
        Vector2 start = transform.position; // 현재 위치
        Vector2 direction = (PlayerManager.instance.player.transform.position - transform.position).normalized; // 플레이어 방향
        float distance = Vector2.Distance(transform.position, PlayerManager.instance.player.transform.position); // 플레이어와의 거리

        RaycastHit2D hit = Physics2D.Raycast(start, direction, distance, whatIsGround); // Raycast로 벽을 감지
        return hit.collider != null; // 벽이 있으면 true 반환
    }
    #endregion

    #region [Flip]
    /// <summary>
    /// 적의 방향을 뒤집는 함수입니다.
    /// </summary>
    public void Flip()
    {
        facingDir *= -1; // 방향을 반대로 설정
        facingRight = !facingRight; // 현재 방향을 반대로 설정
        transform.Rotate(0, 180, 0); // Y축을 기준으로 180도 회전하여 방향 전환
    }

    // X 입력에 따라 적의 방향을 뒤집는 함수입니다.
    private void FlipController(float xInput)
    {
        if (xInput > 0 && !facingRight) Flip(); // 오른쪽으로 입력이 있으면 뒤집기
        else if (xInput < 0 && facingRight) Flip(); // 왼쪽으로 입력이 있으면 뒤집기
    }
    #endregion

    #region [Velocity Control]
    /// <summary>
    /// Rigidbody의 속도를 0으로 설정하는 함수입니다.
    /// </summary>
    public void SetZeroVelocity()
    {
        rb.linearVelocity = Vector2.zero; // 속도를 0으로 설정
    }

    /// <summary>
    /// Rigidbody의 속도를 설정하는 함수입니다.
    /// </summary>
    /// <param name="xVelocity">X축 속도</param>
    /// <param name="yVelocity">Y축 속도</param>
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity); // 속도를 설정
        FlipController(xVelocity); // X축 입력에 따라 방향을 반전시킴
    }
    #endregion

    #region [Gizmos]
    // Gizmos를 사용하여 씬 뷰에 감지 범위 등을 표시합니다.
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance); // 지면 감지 범위

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * wallCheckDistance * facingDir); // 벽 감지 범위

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(playerCheck.position, playerCheckRadius); // 플레이어 감지 범위

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius); // 공격 감지 범위

        Gizmos.color = Color.blue;
        if (PlayerManager.instance != null && PlayerManager.instance.player != null)
            Gizmos.DrawLine(transform.position, PlayerManager.instance.player.transform.position); // 플레이어와의 거리 표시
    }
    #endregion

    /// <summary>
    /// 적이 피해를 받을 때 호출되는 함수입니다.
    /// </summary>
    /// <param name="_damage">입은 피해</param>
    public override void TakeDamage(float _damage)
    {
        if (CurrentHp <= 0)  // 체력이 0 이하로 떨어지면 사망 처리
            return;

        // 피해를 입은 만큼 체력을 감소시키며, 방어력을 고려한 피해 계산
        CurrentHp -= (float)((Mathf.Pow(_damage, 2f) / ((double)Armor + (double)_damage)));

        if (soundTrigger != null)
            soundTrigger.PlayHitSound(); // 피격 소리 재생

        flashEffect.Flash(); // 피격 시 플래시 이펙트

        if (CurrentHp <= 0)  // 체력이 0 이하로 떨어지면 사망 처리
        {
            CurrentHp = 0;
            stateMachine.ChangeState(deadState); // 사망 상태로 변경
        }
        else if (!stateMachine.isAttacking && !stateMachine.isBeingHit)
        {
            stateMachine.isBeingHit = true;
            stateMachine.ChangeState(hitState); // 피격 상태로 전환
        }
    }

    // 적이 사망했을 때 호출되는 함수입니다. (EnemyObject에서는 사용 X)
    protected override void Die()
    {
    }

}
