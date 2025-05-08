using UnityEngine;

/// <summary>
/// Sicklezeep 적 오브젝트의 상태 및 행동을 정의한 클래스입니다.
/// EnemyObject를 상속하며, 상태 머신을 통해 Idle, Patrol, Chase, Attack 상태를 관리합니다.
/// </summary>
public class SicklezeepObject : EnemyObject
{
    [Header("Collider Info")]
    [SerializeField] private Transform dashAttackCheck; // 대시 공격 감지 위치
    [SerializeField] Vector2 dashAttackBoxSize; // 감지 박스 크기 조절

    private int currentAttackIndex = 0;

    #region State
    public EnemyIdleState idleState { get; private set; }     // 대기 상태
    public EnemyPatrolState patrolState { get; private set; } // 순찰 상태
    public EnemyChaseState chaseState { get; private set; }   // 추격 상태
    public EnemyAttackState attack1State { get; private set; } // 공격 상태
    public EnemyAttackState attack2State { get; private set; } // 공격 상태
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // 상태 인스턴스를 초기화
        idleState = new EnemyIdleState(this, stateMachine, "Idle");
        patrolState = new EnemyPatrolState(this, stateMachine, "Move");
        chaseState = new EnemyChaseState(this, stateMachine, "Move");
        attack1State = new EnemyAttackState(this, stateMachine, "Attack1");
        attack2State = new EnemyAttackState(this, stateMachine, "Attack2");
    }

    protected override void Start()
    {
        base.Start();

        // 시작 시 기본 상태를 Idle로 설정
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// 플레이어가 탐지되었을 때 추격 상태로 전환합니다.
    /// </summary>
    public override void EnterPlayerDetection()
    {
        if (IsDashAttackDetected() && Time.time >= lastTimeAttacked + attackCooldown)
        {
            lastTimeAttacked = Time.time;
            CallAttackState();
        }    
        else
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
    /// 호출 시 앞으로 빠르게 이동(대시)한 뒤 공격 상태로 전환합니다.
    /// </summary>
    public override void CallAttackState()
    {
        // 대시처럼 이동: 플레이어 위치 방향으로 stopOffset만큼 떨어진 지점까지 이동
        float stopOffset = 1f; // 플레이어와의 최소 거리
        float rawDistance = PlayerManager.instance.player.transform.position.x - transform.position.x;

        // 방향이 다르면 Flip
        if (Mathf.Sign(rawDistance) != facingDir)
        {
            Flip();
        }

        // 이동해야 할 거리 = 전체 거리 - 최소 간격
        float dashDistance = Mathf.Abs(rawDistance) - stopOffset;
        dashDistance = Mathf.Max(0, dashDistance); // 음수 방지

        // 방향을 고려한 벡터 (좌우 자동 계산)
        Vector2 dashVector = new Vector2(facingDir * dashDistance, 0f);
        transform.position += (Vector3)dashVector;

        // 공격 상태로 전환
        switch (currentAttackIndex)
        {
            case 0:
                stateMachine.ChangeState(attack1State);
                break;
            case 1:
                stateMachine.ChangeState(attack2State);
                break;
        }

        // 다음 공격 인덱스로 순환
        currentAttackIndex = (currentAttackIndex + 1) % 2;
    }

    /// <summary>
    /// 호출 시 대기 상태로 전환합니다.
    /// </summary>
    public override void CallIdleState()
    {
        stateMachine.ChangeState(idleState);
    }

    /// <summary>
    /// 대시 방향으로 직선 공격 판정
    /// </summary>
    private bool IsDashAttackDetected()
        => Physics2D.OverlapBox(dashAttackCheck.position, dashAttackBoxSize, 0f, whatIsPlayer) != null;

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;

        // 박스 중심 위치와 크기
        Vector2 center = dashAttackCheck.position;
        Vector2 size = dashAttackBoxSize;

        // 사각형 기즈모 그리기
        Gizmos.DrawWireCube(center, size);
    }
}
