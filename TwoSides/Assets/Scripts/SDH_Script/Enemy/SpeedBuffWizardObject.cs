using UnityEngine;

/// <summary>
/// 諛⑹뼱??踰꾪봽瑜?遺?ы븯??留덈쾿?????ㅻ툕?앺듃 ?대옒?ㅼ엯?덈떎.
/// EnemyObject瑜??곸냽?섎ŉ, 吏??踰붿쐞 ???꾧뎔?먭쾶 諛⑹뼱??踰꾪봽瑜?遺?ы븯??Support ?곹깭瑜?媛吏묐땲??
/// </summary>
public class SpeedBuffWizardObject : EnemyObject
{
    [SerializeField] private float supportRange;    // 吏??踰붿쐞
    [SerializeField] private GameObject buffPrefab; // 踰꾪봽 ?꾨━??

    #region State
    public EnemySupportState supportState { get; private set; } // 吏???곹깭
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // 吏???곹깭 ?몄뒪?댁뒪 珥덇린??(諛⑹뼱??踰꾪봽 ???
        supportState = new EnemySupportState(this, stateMachine, "Idle", supportRange, buffPrefab, BuffType.SPEED);
    }

    protected override void Start()
    {
        base.Start();

        // ?쒖옉 ??吏???곹깭濡?珥덇린??
        stateMachine.Initialize(supportState);
    }

    protected override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// Hit -> Support
    /// </summary>
    public override void ExitPlayerDetection()
    {
        stateMachine.ChangeState(supportState);
    }

    // 吏??踰붿쐞 ?쒓컖??
    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, supportRange);
    }
}
