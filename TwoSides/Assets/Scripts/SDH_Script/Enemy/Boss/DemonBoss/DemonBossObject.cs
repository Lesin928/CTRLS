using UnityEngine;

/// <summary>
/// DemonBoss 오브젝트의 상태 및 행동을 정의하는 클래스입니다.
/// EnemyObject를 상속하며, 다양한 상태 (Idle, Patrol, Chase, Attack)를 처리합니다.
/// </summary>
public class DemonBossObject : EnemyObject
{
    [Header("Attack Check Transform")]
    [SerializeField] private Transform closeAttackCheck; // 근거리 공격 범위 기준 위치
    [SerializeField] private Transform midAttackCheck;   // 중거리 공격 범위 기준 위치
    [SerializeField] private Transform longAttackCheck;  // 원거리 공격 범위 기준 위치

    [Header("Attack Check Radius")]
    [SerializeField] Vector2 closeAttackCheckSize; // 근거리 공격 범위 크기
    [SerializeField] Vector2 midAttackCheckSize;   // 중거리 공격 범위 크기
    [SerializeField] Vector2 longAttackCheckSize;  // 원거리 공격 범위 크기

    #region State
    public EnemyIdleState idleState { get; private set; }      // 대기 상태
    public EnemyPatrolState patrolState { get; private set; }  // 순찰 상태
    public EnemyChaseState chaseState { get; private set; }    // 추격 상태
    public EnemyAttackState attack1State { get; private set; } // 공격1 상태
    public EnemyAttackState attack2State { get; private set; } // 공격2 상태
    public EnemyAttackState attack3State { get; private set; } // 공격3 상태
    public EnemyAttackState attack4State { get; private set; } // 공격4 상태
    public EnemyAttackState attack5State { get; private set; } // 공격5 상태
    public EnemyAttackState attack6State { get; private set; } // 공격6 상태
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // 상태 객체 생성 및 초기화
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
    /// SlimeBoss -> DemonBoss 전환 시 방향을 초기화합니다.
    /// </summary>
    /// <param name="direction">SlimeBoss의 바라보던 방향</param>
    public void InitFacingDir(int direction)
    {
        if (facingDir != direction)
        {
            Flip(); // 방향 반전
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
    /// 플레이어가 탐지 범위에서 벗어났을 때 순찰 상태로 전환합니다.
    /// </summary>
    public override void ExitPlayerDetection()
    {
        stateMachine.ChangeState(patrolState);
    }

    /// <summary>
    /// 호출 시 적절한 공격 상태로 전환합니다.
    /// 공격 거리에 따라 다양한 공격 패턴이 선택됩니다.
    /// </summary>
    public override void CallAttackState()
    {
        float distanceX = Mathf.Abs(transform.position.x - PlayerManager.instance.player.transform.position.x);
        int rand;

        if (closeRangeDetected())
        {
            stateMachine.ChangeState(attack4State); // 근거리 고정 공격
        }
        else if (midRangeDetected())
        {
            // 중거리: 다양한 공격 중 무작위 선택 (0~4)
            rand = Random.Range(0, 5); // 0, 1, 2, 3, 4
            switch (rand)
            {
                case 0:
                    stateMachine.ChangeState(attack1State);
                    break;
                case 1:
                    stateMachine.ChangeState(attack2State);
                    break;
                case 2:
                    stateMachine.ChangeState(attack3State);
                    break;
                case 3:
                    stateMachine.ChangeState(attack5State);
                    break;
                case 4:
                    stateMachine.ChangeState(attack6State);
                    break;
            }
        }
        else if (longRangeDetected())
        {
            // 원거리: 공격 2, 5, 6 중 랜덤 선택 (0~2)
            rand = Random.Range(0, 3); // 0, 1, 2
            switch (rand)
            {
                case 0:
                    stateMachine.ChangeState(attack2State);
                    break;
                case 1:
                    stateMachine.ChangeState(attack5State);
                    break;
                case 2:
                    stateMachine.ChangeState(attack6State);
                    break;
            }
        }
    }


    /// <summary>
    /// 호출 시 대기 상태로 전환합니다.
    /// </summary>
    public override void CallIdleState()
    {
        stateMachine.ChangeState(idleState);
    }

    // 근거리 공격 범위 내에 플레이어가 있는지 확인
    public Collider2D closeRangeDetected()
        => Physics2D.OverlapBox(closeAttackCheck.position, closeAttackCheckSize, 0f, whatIsPlayer);

    // 중거리 공격 범위 내에 플레이어가 있는지 확인
    public Collider2D midRangeDetected()
        => Physics2D.OverlapBox(midAttackCheck.position, midAttackCheckSize, 0f, whatIsPlayer);

    // 원거리 공격 범위 내에 플레이어가 있는지 확인
    public Collider2D longRangeDetected()
        => Physics2D.OverlapBox(longAttackCheck.position, longAttackCheckSize, 0f, whatIsPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        // 공격 범위를 Scene 뷰에 시각적으로 표시
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(closeAttackCheck.position, closeAttackCheckSize);
        Gizmos.DrawWireCube(midAttackCheck.position, midAttackCheckSize);
        Gizmos.DrawWireCube(longAttackCheck.position, longAttackCheckSize);
    }
}
