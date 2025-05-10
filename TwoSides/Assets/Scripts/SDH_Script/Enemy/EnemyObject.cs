using UnityEngine;

/// <summary>
/// ???대옒?ㅻ뒗 ??罹먮┃?곗쓽 ?대룞, 怨듦꺽, 異⑸룎 泥섎━, ?곹깭 愿由??깆쓣 泥섎━?⑸땲??
/// ?곸쓽 ?곹깭???곹깭 癒몄떊?쇰줈 愿由щ릺硫? 湲곕낯?곸씤 ?대룞, 異붿쟻, 怨듦꺽 ?깆쓽 ?숈옉???ы븿?⑸땲??
/// </summary>
public class EnemyObject : CharacterObject
{
    #region [Move Info]
    [Header("Move Info")]
    public float defaultMoveSpeed; // 湲곕낯 ?대룞 ?띾룄
    public float chaseSpeed;       // 異붿쟻 ?대룞 ?띾룄
    #endregion

    #region [Attack Info]
    [Header("Attack Info")]
    public float attackCooldown;                     // 怨듦꺽 荑⑦???
    [HideInInspector] public float lastTimeAttacked; // 留덉?留됱쑝濡?怨듦꺽???쒓컙
    #endregion

    #region [Collider Info]
    [Header("Collider Info")]
    [SerializeField] protected Transform groundCheck;     // ?낆쓣 泥댄겕?섎뒗 ?꾩튂
    [SerializeField] protected float groundCheckDistance; // ??泥댄겕 嫄곕━
    [Space]
    [SerializeField] protected Transform wallCheck;       // 踰쎌쓣 泥댄겕?섎뒗 ?꾩튂
    [SerializeField] protected float wallCheckDistance;   // 踰?泥댄겕 嫄곕━
    [Space]
    [SerializeField] protected Transform playerCheck;     // ?뚮젅?댁뼱瑜?泥댄겕?섎뒗 ?꾩튂
    [SerializeField] protected float playerCheckRadius;   // ?뚮젅?댁뼱 泥댄겕 諛섏?由?
    [Space]
    public Transform attackCheck;                         // 怨듦꺽 踰붿쐞 泥댄겕 ?꾩튂
    public float attackCheckRadius;                       // 怨듦꺽 踰붿쐞 泥댄겕 諛섏?由?
    [Space]
    [SerializeField] protected LayerMask whatIsGround;    // ???덉씠??留덉뒪??
    [SerializeField] protected LayerMask whatIsWall;      // 踰??덉씠??留덉뒪??
    [SerializeField] protected LayerMask whatIsPlayer;    // ?뚮젅?댁뼱 ?덉씠??留덉뒪??
    public float colliderWidth { get; private set; }      // 肄쒕씪?대뜑???덈퉬
    #endregion

    #region [Flash Effect]
    [Header("Flash Effect")]
    [SerializeField] private EnemyFlashEffect flashEffect; // ?곸쓽 ?뚮옒???댄럺??
    #endregion

    #region [Components]
    public Animator anim { get; private set; }  // ??罹먮┃?곗쓽 ?좊땲硫붿씠??
    public Rigidbody2D rb { get; private set; } // ??罹먮┃?곗쓽 Rigidbody2D
    public Collider2D col { get; private set; } // ??罹먮┃?곗쓽 Collider2D
    #endregion

    #region [StateMachine]
    public EnemyStateMachine stateMachine { get; private set; } // ?곸쓽 ?곹깭 癒몄떊
    #endregion

    #region [State]
    public EnemyHitState hitState { get; private set; }   // ?곸씠 留욎? ?곹깭
    public EnemyDeadState deadState { get; private set; } // ?곸씠 二쎌? ?곹깭
    #endregion

    #region [Facing]
    public int facingDir { get; private set; } = 1; // ?곸쓽 諛⑺뼢 (1: ?ㅻⅨ履? -1: ?쇱そ)
    protected bool facingRight = true;              // ?곸씠 ?ㅻⅨ履쎌쓣 蹂닿퀬 ?덈뒗吏 ?щ?
    #endregion

    #region [Audio]
    private EnemySoundTrigger soundTrigger; // ?곸쓽 ?뚮━ ?몃━嫄?
    #endregion


    // ?곹깭 癒몄떊 珥덇린??諛??곹깭 ?ㅼ젙
    protected virtual void Awake()
    {
        stateMachine = new EnemyStateMachine();

        // ?곸쓽 Hit ?곹깭? Dead ?곹깭瑜?珥덇린??
        hitState = new EnemyHitState(this, stateMachine, "Hit");
        deadState = new EnemyDeadState(this, stateMachine, "Dead");
    }

    // ?꾩슂??而댄룷?뚰듃??珥덇린??
    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>(); // ?좊땲硫붿씠??而댄룷?뚰듃 珥덇린??
        rb = GetComponent<Rigidbody2D>(); // 由ъ??쒕컮??而댄룷?뚰듃 珥덇린??
        flashEffect = GetComponentInChildren<EnemyFlashEffect>(); // ?뚮옒???④낵 而댄룷?뚰듃 珥덇린??
        col = GetComponent<Collider2D>(); // 肄쒕씪?대뜑 而댄룷?뚰듃 珥덇린??
        soundTrigger = GetComponentInChildren<EnemySoundTrigger>(); // ?뚮━ ?몃━嫄?而댄룷?뚰듃 珥덇린??
        colliderWidth = col.bounds.size.x; // 肄쒕씪?대뜑??媛濡?湲몄씠 ???
    }

    // ?곹깭 癒몄떊 ?낅뜲?댄듃
    protected virtual void Update()
    {
        stateMachine.currentState?.Update(); // ?꾩옱 ?곹깭???낅뜲?댄듃 ?몄텧
    }

    /// <summary>
    /// ?뚮젅?댁뼱 ?먯? ?곹깭濡?吏꾩엯?????몄텧?섎뒗 ?⑥닔
    /// </summary>
    public virtual void EnterPlayerDetection()
    {
    }

    /// <summary>
    /// ?뚮젅?댁뼱 ?먯? ?곹깭?먯꽌 踰쀬뼱?????몄텧?섎뒗 ?⑥닔
    /// </summary>
    public virtual void ExitPlayerDetection()
    {
    }

    /// <summary>
    /// 怨듦꺽 ?곹깭濡??꾪솚?섎뒗 ?⑥닔
    /// </summary>
    public virtual void CallAttackState()
    {
    }

    /// <summary>
    /// ?湲??곹깭濡??꾪솚?섎뒗 ?⑥닔
    /// </summary>
    public virtual void CallIdleState()
    {
    }

    public virtual void CallChaseState()
    {
    }

    #region [Animation Event]
    /// <summary>
    /// ?좊땲硫붿씠?섏씠 ?앸궗?????몄텧?섎뒗 ?⑥닔
    /// </summary>
    public virtual void AnimationFinishTrigger()
        => stateMachine.currentState?.AnimationFinishTrigger();
    #endregion

    #region [Detection]
    /// <summary>
    /// 諛붾떏??媛먯??섎뒗 ?⑥닔
    /// </summary>
    /// <returns>諛붾떏??媛먯??섎㈃ true, ?꾨땲硫?false</returns>
    public virtual bool IsGroundDetected()
        => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround).collider != null;

    /// <summary>
    /// 踰쎌쓣 媛먯??섎뒗 ?⑥닔
    /// </summary>
    /// <returns>踰쎌씠 媛먯??섎㈃ true, ?꾨땲硫?false</returns>
    public virtual bool IsWallDetected()
        => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsWall).collider != null;

    /// <summary>
    /// ?뚮젅?댁뼱瑜?媛먯??섎뒗 ?⑥닔
    /// </summary>
    /// <returns>?뚮젅?댁뼱媛 媛먯??섎㈃ ?대떦 Collider2D瑜?諛섑솚, ?꾨땲硫?null</returns>
    public virtual Collider2D IsPlayerDetected()
        => Physics2D.OverlapCircle(playerCheck.position, playerCheckRadius, whatIsPlayer);

    /// <summary>
    /// 怨듦꺽??媛?ν븳 踰붿쐞???뚮젅?댁뼱媛 ?덈뒗吏 媛먯??섎뒗 ?⑥닔
    /// </summary>
    /// <returns>?뚮젅?댁뼱媛 媛먯??섎㈃ ?대떦 Collider2D瑜?諛섑솚, ?꾨땲硫?null</returns>
    public virtual Collider2D IsAttackDetectable()
        => Physics2D.OverlapCircle(attackCheck.position, attackCheckRadius, whatIsPlayer);

    /// <summary>
    /// 踰쎄낵 ?뚮젅?댁뼱 ?ъ씠???μ븷臾쇱씠 ?덈뒗吏 ?뺤씤?섎뒗 ?⑥닔
    /// </summary>
    /// <returns>踰??ъ씠???뚮젅?댁뼱媛 ?덉쑝硫?true, ?꾨땲硫?false</returns>
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
    /// ?곸쓽 諛⑺뼢??諛섏쟾?쒗궎???⑥닔
    /// </summary>
    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0); // 媛앹껜瑜?Y異뺤쓣 湲곗??쇰줈 180???뚯쟾
    }

    // ?낅젰 媛믪뿉 ?곕씪 ?곸쓽 諛⑺뼢???꾪솚?섎뒗 ?⑥닔
    private void FlipController(float xInput)
    {
        if (xInput > 0 && !facingRight) Flip();
        else if (xInput < 0 && facingRight) Flip();
    }
    #endregion

    #region [Velocity Control]
    /// <summary>
    /// Rigidbody???띾룄瑜?0?쇰줈 ?ㅼ젙?섎뒗 ?⑥닔
    /// </summary>
    public void SetZeroVelocity()
    {
        rb.linearVelocity = Vector2.zero;
    }

    /// <summary>
    /// Rigidbody???띾룄瑜??ㅼ젙?섎뒗 ?⑥닔
    /// </summary>
    /// <param name="xVelocity">X異??띾룄</param>
    /// <param name="yVelocity">Y異??띾룄</param>
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity); // X異??낅젰 媛믪뿉 ?곕씪 ?곸쓽 諛⑺뼢???꾪솚
    }
    #endregion

    #region [Gizmos]
    // Gizmos濡?媛곸쥌 媛먯? 踰붿쐞? 寃쎈줈瑜??쒓컖?곸쑝濡??쒖떆
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
    /// ?곸씠 ?쇳빐瑜?諛쏆쓣 ???몄텧?섎뒗 ?⑥닔
    /// </summary>
    /// <param name="_damage">?낅젰???쇳빐 媛?/param>
    public override void TakeDamage(float _damage)
    {
        // ?쇳빐 媛믪뿉 ?곕씪 ?꾩옱 泥대젰 媛먯냼
        CurrentHp -= (float)((Mathf.Pow(_damage, 2f) / ((double)Armor + (double)_damage)));

        if (soundTrigger != null)
            soundTrigger.PlayHitSound(); // 留욎쓣 ???뚮━ ?ъ깮

        flashEffect.Flash(); // ?뚮옒???④낵 ?ㅽ뻾

        if (CurrentHp <= 0)  // 泥대젰??0 ?댄븯濡??⑥뼱吏硫?二쎌쓬 ?곹깭濡??꾪솚
        {
            CurrentHp = 0;
            stateMachine.ChangeState(deadState);
        }
        else // 泥대젰???⑥븘 ?덉쑝硫??덊듃 ?곹깭濡??꾪솚
        {
            if (!stateMachine.isAttacking)
                stateMachine.ChangeState(hitState);
        }
    }

    // ?곸씠 二쎌뿀?????몄텧?섎뒗 ?⑥닔 (EnemyObject?먯꽌???ъ슜 X)
    protected override void Die()
    {
    }

}
