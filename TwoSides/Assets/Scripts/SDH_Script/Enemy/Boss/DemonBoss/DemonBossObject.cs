using UnityEngine;

/// <summary>
/// DemonBoss 오브젝트의 상태 및 행동을 정의한 클래스입니다.
/// EnemyObject를 상속하며, 상태 머신을 통해 Idle, Patrol, Chase, Attack 상태를 관리합니다.
/// </summary>
public class DemonBossObject : EnemyObject
{
    [Header("Attack Check Transform")]
    [SerializeField] private Transform closeAttackCheck;
    [SerializeField] private Transform midAttackCheck;
    [SerializeField] private Transform longAttackCheck;

    [Header("Attack Check Radius")]
    [SerializeField] Vector2 closeAttackCheckSize;
    [SerializeField] Vector2 midAttackCheckSize;
    [SerializeField] Vector2 longAttackCheckSize;

    #region State
    public EnemyIdleState idleState { get; private set; }     // 대기 상태
    public EnemyPatrolState patrolState { get; private set; } // 순찰 상태
    public EnemyChaseState chaseState { get; private set; }   // 추격 상태
    public EnemyAttackState attack1State { get; private set; } // 공격 상태
    public EnemyAttackState attack2State { get; private set; } // 공격 상태
    public EnemyAttackState attack3State { get; private set; } // 공격 상태
    public EnemyAttackState attack4State { get; private set; } // 공격 상태
    public EnemyAttackState attack5State { get; private set; } // 공격 상태
    public EnemyAttackState attack6State { get; private set; } // 공격 상태
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // 상태 인스턴스 초기화
        idleState = new EnemyIdleState(this, stateMachine, "Idle");
        patrolState = new EnemyPatrolState(this, stateMachine, "Move");
        chaseState = new EnemyChaseState(this, stateMachine, "Move");
        attack1State = new EnemyAttackState(this, stateMachine, "Attack1");
        attack2State = new EnemyAttackState(this, stateMachine, "Attack2");
        attack3State = new EnemyAttackState(this, stateMachine, "Attack3");
        attack4State = new EnemyAttackState(this, stateMachine, "Attack4");
        attack5State = new EnemyAttackState(this, stateMachine, "Attack5");
        attack6State = new EnemyAttackState(this, stateMachine, "Attack6");
    }

    protected override void Start()
    {
        base.Start();

        // 시작 시 Idle 상태로 초기화
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// SlimeBoss -> DemonBoss 전환시 방향 초기화합니다.
    /// </summary>
    /// <param name="direction">SlimeBoss의 방향값</param>
    public void InitFacingDir(int direction)
    {
        if (facingDir != direction)
        {
            Flip(); // 방향이 다르면 뒤집기
        }
    }

    /// <summary>
    /// 플레이어가 탐지되었을 때 추격 상태로 전환합니다.
    /// </summary>
    public override void EnterPlayerDetection()
    {
        stateMachine.ChangeState(chaseState);
    }

    /// <summary>
    /// 플레이어를 탐지하지 못할 경우 순찰 상태로 전환합니다.
    /// </summary>
    public override void ExitPlayerDetection()
    {
        stateMachine.ChangeState(patrolState);
    }

    /// <summary>
    /// 호출 시 공격 상태로 전환합니다.
    /// 공격 패턴을 랜덤으로 호출합니다.
    /// </summary>
    public override void CallAttackState()
    {
        float distanceX = Mathf.Abs(transform.position.x - PlayerManager.instance.player.transform.position.x);
        float rand = Random.value; // 0.0f ~ 1.0f 사이의 float

        if (closeRangeDetected())
            stateMachine.ChangeState(attack4State);
        else if (midRangeDetected())
        {
            if (rand < 0.20f)
                stateMachine.ChangeState(attack1State);
            else if (rand < 0.40f)
                stateMachine.ChangeState(attack2State);
            else if (rand < 0.60f)
                stateMachine.ChangeState(attack3State);
            else if (rand < 0.80f)
                stateMachine.ChangeState(attack5State);
            else
                stateMachine.ChangeState(attack6State);
        }
        else if (longRangeDetected())
        {
            if (rand < 0.33f)
                stateMachine.ChangeState(attack2State);
            else if (rand < 0.66f)
                stateMachine.ChangeState(attack5State);
            else
                stateMachine.ChangeState(attack6State);
        }
    }

    /// <summary>
    /// 호출 시 대기 상태로 전환합니다.
    /// </summary>
    public override void CallIdleState()
    {
        stateMachine.ChangeState(idleState);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(closeAttackCheck.position, closeAttackCheckSize);
        Gizmos.DrawWireCube(midAttackCheck.position, midAttackCheckSize);
        Gizmos.DrawWireCube(longAttackCheck.position, longAttackCheckSize);
    }

    public Collider2D closeRangeDetected()
        => Physics2D.OverlapBox(closeAttackCheck.position, closeAttackCheckSize, 0f, whatIsPlayer);

    public Collider2D midRangeDetected()
        => Physics2D.OverlapBox(midAttackCheck.position, midAttackCheckSize, 0f, whatIsPlayer);

    public Collider2D longRangeDetected()
        => Physics2D.OverlapBox(longAttackCheck.position, longAttackCheckSize, 0f, whatIsPlayer);

}
