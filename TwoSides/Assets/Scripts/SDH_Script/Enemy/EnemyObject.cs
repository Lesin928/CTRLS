using UnityEngine;

/// <summary>
/// ???????삳뮉 ??筌?Ŧ??怨쀬벥 ??猷? ?⑤벀爰? ?겸뫖猷?筌ｌ꼶?? ?怨밴묶 ?온???源놁뱽 筌ｌ꼶???몃빍??
/// ?怨몄벥 ?怨밴묶???怨밴묶 ?믩챷???곗쨮 ?온?귐됰┷筌? 疫꿸퀡??怨몄뵥 ??猷? ?곕뗄?? ?⑤벀爰??源놁벥 ??덉삂????釉??몃빍??
/// </summary>
public class EnemyObject : CharacterObject
{
    #region [Move Info]
    [Header("Move Info")]
    public float defaultMoveSpeed; // 疫꿸퀡????猷???얜즲
    public float chaseSpeed;       // ?곕뗄????猷???얜즲
    #endregion

    #region [Attack Info]
    [Header("Attack Info")]
    public float attackCooldown;                     // ?⑤벀爰??묅뫂???
    [HideInInspector] public float lastTimeAttacked; // 筌띾뜆?筌띾맩?앮에??⑤벀爰????볦퍢
    #endregion

    #region [Collider Info]
    [Header("Collider Info")]
    [SerializeField] protected Transform groundCheck;     // ??놁뱽 筌ｋ똾寃??롫뮉 ?袁⑺뒄
    [SerializeField] protected float groundCheckDistance; // ??筌ｋ똾寃?椰꾧퀡??
    [Space]
    [SerializeField] protected Transform wallCheck;       // 甕곗럩??筌ｋ똾寃??롫뮉 ?袁⑺뒄
    [SerializeField] protected float wallCheckDistance;   // 甕?筌ｋ똾寃?椰꾧퀡??
    [Space]
    [SerializeField] protected Transform playerCheck;     // ???쟿??곷선??筌ｋ똾寃??롫뮉 ?袁⑺뒄
    [SerializeField] protected float playerCheckRadius;   // ???쟿??곷선 筌ｋ똾寃?獄쏆꼷???
    [Space]
    public Transform attackCheck;                         // ?⑤벀爰?甕곕뗄??筌ｋ똾寃??袁⑺뒄
    public float attackCheckRadius;                       // ?⑤벀爰?甕곕뗄??筌ｋ똾寃?獄쏆꼷???
    [Space]
    [SerializeField] protected LayerMask whatIsGround;    // ????됱뵠??筌띾뜆???
    [SerializeField] protected LayerMask whatIsWall;      // 甕???됱뵠??筌띾뜆???
    [SerializeField] protected LayerMask whatIsPlayer;    // ???쟿??곷선 ??됱뵠??筌띾뜆???
    public float colliderWidth { get; private set; }      // ?꾩뮆????????덊돩
    #endregion

    #region [Flash Effect]
    [Header("Flash Effect")]
    [SerializeField] private EnemyFlashEffect flashEffect; // ?怨몄벥 ???삋????꾨읃??
    #endregion

    #region [Components]
    public Animator anim { get; private set; }  // ??筌?Ŧ??怨쀬벥 ?醫딅빍筌롫뗄???
    public Rigidbody2D rb { get; private set; } // ??筌?Ŧ??怨쀬벥 Rigidbody2D
    public Collider2D col { get; private set; } // ??筌?Ŧ??怨쀬벥 Collider2D
    #endregion

    #region [StateMachine]
    public EnemyStateMachine stateMachine { get; private set; } // ?怨몄벥 ?怨밴묶 ?믩챷??
    #endregion

    #region [State]
    public EnemyHitState hitState { get; private set; }   // ?怨몄뵠 筌띿쉸? ?怨밴묶
    public EnemyDeadState deadState { get; private set; } // ?怨몄뵠 雅뚯럩? ?怨밴묶
    #endregion

    #region [Facing]
    public int facingDir { get; private set; } = 1; // ?怨몄벥 獄쎻뫚堉?(1: ??삘뀲筌? -1: ??긱걹)
    protected bool facingRight = true;              // ?怨몄뵠 ??삘뀲筌잛럩??癰귣떯????덈뮉筌왖 ???
    #endregion

    #region [Audio]
    private EnemySoundTrigger soundTrigger; // ?怨몄벥 ???봺 ?紐꺿봺椰?
    #endregion


    // ?怨밴묶 ?믩챷???λ뜃由??獄??怨밴묶 ??쇱젟
    protected virtual void Awake()
    {
        stateMachine = new EnemyStateMachine();

        // ?怨몄벥 Hit ?怨밴묶?? Dead ?怨밴묶???λ뜃由??
        hitState = new EnemyHitState(this, stateMachine, "Hit");
        deadState = new EnemyDeadState(this, stateMachine, "Dead");
    }

    // ?袁⑹뒄???뚮똾猷??곕뱜???λ뜃由??
    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>(); // ?醫딅빍筌롫뗄????뚮똾猷??곕뱜 ?λ뜃由??
        rb = GetComponent<Rigidbody2D>(); // ?귐???뺤뺍???뚮똾猷??곕뱜 ?λ뜃由??
        flashEffect = GetComponentInChildren<EnemyFlashEffect>(); // ???삋????ｋ궢 ?뚮똾猷??곕뱜 ?λ뜃由??
        col = GetComponent<Collider2D>(); // ?꾩뮆??????뚮똾猷??곕뱜 ?λ뜃由??
        soundTrigger = GetComponentInChildren<EnemySoundTrigger>(); // ???봺 ?紐꺿봺椰??뚮똾猷??곕뱜 ?λ뜃由??
        colliderWidth = col.bounds.size.x; // ?꾩뮆??????揶쎛嚥?疫뀀챷??????
    }

    // ?怨밴묶 ?믩챷????낅쑓??꾨뱜
    protected virtual void Update()
    {
        stateMachine.currentState?.Update(); // ?袁⑹삺 ?怨밴묶????낅쑓??꾨뱜 ?紐꾪뀱
    }

    /// <summary>
    /// ???쟿??곷선 ?癒? ?怨밴묶嚥?筌욊쑴??????紐꾪뀱??롫뮉 ??λ땾
    /// </summary>
    public virtual void EnterPlayerDetection()
    {
    }

    /// <summary>
    /// ???쟿??곷선 ?癒? ?怨밴묶?癒?퐣 甕곗щ선?????紐꾪뀱??롫뮉 ??λ땾
    /// </summary>
    public virtual void ExitPlayerDetection()
    {
    }

    /// <summary>
    /// ?⑤벀爰??怨밴묶嚥??袁れ넎??롫뮉 ??λ땾
    /// </summary>
    public virtual void CallAttackState()
    {
    }

    /// <summary>
    /// ??疫??怨밴묶嚥??袁れ넎??롫뮉 ??λ땾
    /// </summary>
    public virtual void CallIdleState()
    {
    }

    public virtual void CallChaseState()
    {
    }

    #region [Animation Event]
    /// <summary>
    /// ?醫딅빍筌롫뗄???륁뵠 ??멸텢?????紐꾪뀱??롫뮉 ??λ땾
    /// </summary>
    public virtual void AnimationFinishTrigger()
        => stateMachine.currentState?.AnimationFinishTrigger();
    #endregion

    #region [Detection]
    /// <summary>
    /// 獄쏅뗀???揶쏅Ŋ???롫뮉 ??λ땾
    /// </summary>
    /// <returns>獄쏅뗀???揶쏅Ŋ???롢늺 true, ?袁⑤빍筌?false</returns>
    public virtual bool IsGroundDetected()
        => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround).collider != null;

    /// <summary>
    /// 甕곗럩??揶쏅Ŋ???롫뮉 ??λ땾
    /// </summary>
    /// <returns>甕곗럩??揶쏅Ŋ???롢늺 true, ?袁⑤빍筌?false</returns>
    public virtual bool IsWallDetected()
        => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsWall).collider != null;

    /// <summary>
    /// ???쟿??곷선??揶쏅Ŋ???롫뮉 ??λ땾
    /// </summary>
    /// <returns>???쟿??곷선揶쎛 揶쏅Ŋ???롢늺 ????Collider2D??獄쏆꼹?? ?袁⑤빍筌?null</returns>
    public virtual Collider2D IsPlayerDetected()
        => Physics2D.OverlapCircle(playerCheck.position, playerCheckRadius, whatIsPlayer);

    /// <summary>
    /// ?⑤벀爰??揶쎛?館釉?甕곕뗄??????쟿??곷선揶쎛 ??덈뮉筌왖 揶쏅Ŋ???롫뮉 ??λ땾
    /// </summary>
    /// <returns>???쟿??곷선揶쎛 揶쏅Ŋ???롢늺 ????Collider2D??獄쏆꼹?? ?袁⑤빍筌?null</returns>
    public virtual Collider2D IsAttackDetectable()
        => Physics2D.OverlapCircle(attackCheck.position, attackCheckRadius, whatIsPlayer);

    /// <summary>
    /// 甕곗럡?????쟿??곷선 ??????關釉룩눧?깆뵠 ??덈뮉筌왖 ?類ㅼ뵥??롫뮉 ??λ땾
    /// </summary>
    /// <returns>甕?????????쟿??곷선揶쎛 ??됱몵筌?true, ?袁⑤빍筌?false</returns>
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
    /// ?怨몄벥 獄쎻뫚堉??獄쏆꼷???쀪텕????λ땾
    /// </summary>
    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0); // 揶쏆빘猿쒐몴?Y?곕벡??疫꿸퀣???곗쨮 180?????읈
    }

    // ??낆젾 揶쏅?肉??怨뺤뵬 ?怨몄벥 獄쎻뫚堉???袁れ넎??롫뮉 ??λ땾
    private void FlipController(float xInput)
    {
        if (xInput > 0 && !facingRight) Flip();
        else if (xInput < 0 && facingRight) Flip();
    }
    #endregion

    #region [Velocity Control]
    /// <summary>
    /// Rigidbody????얜즲??0??곗쨮 ??쇱젟??롫뮉 ??λ땾
    /// </summary>
    public void SetZeroVelocity()
    {
        rb.linearVelocity = Vector2.zero;
    }

    /// <summary>
    /// Rigidbody????얜즲????쇱젟??롫뮉 ??λ땾
    /// </summary>
    /// <param name="xVelocity">X????얜즲</param>
    /// <param name="yVelocity">Y????얜즲</param>
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity); // X????낆젾 揶쏅?肉??怨뺤뵬 ?怨몄벥 獄쎻뫚堉???袁れ넎
    }
    #endregion

    #region [Gizmos]
    // Gizmos嚥?揶쏄낯伊?揶쏅Ŋ? 甕곕뗄??? 野껋럥以덄몴???볦퍟?怨몄몵嚥???뽯뻻
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
    /// ?怨몄뵠 ??노퉸??獄쏆룇?????紐꾪뀱??롫뮉 ??λ땾
    /// </summary>
    /// <param name="_damage">??낆젾????노퉸 揶?/param>
    public override void TakeDamage(float _damage)
    {
        if (CurrentHp <= 0) return;

        // ??노퉸 揶쏅?肉??怨뺤뵬 ?袁⑹삺 筌ｋ???揶쏅Ŋ??
        CurrentHp -= (float)((Mathf.Pow(_damage, 2f) / ((double)Armor + (double)_damage)));

        if (soundTrigger != null)
            soundTrigger.PlayHitSound(); // 筌띿쉸???????봺 ??源?

        flashEffect.Flash(); // ???삋????ｋ궢 ??쎈뻬

        if (CurrentHp <= 0)  // 筌ｋ????0 ??꾨릭嚥???λ선筌왖筌?雅뚯럩???怨밴묶嚥??袁れ넎
        {
            CurrentHp = 0;
            stateMachine.ChangeState(deadState);
        }
        else // 筌ｋ??????λ툡 ??됱몵筌???딅뱜 ?怨밴묶嚥??袁れ넎
        {
            if (!stateMachine.isAttacking)
                stateMachine.ChangeState(hitState);
        }
    }

    // ?怨몄뵠 雅뚯럩肉?????紐꾪뀱??롫뮉 ??λ땾 (EnemyObject?癒?퐣??????X)
    protected override void Die()
    {
    }

}
