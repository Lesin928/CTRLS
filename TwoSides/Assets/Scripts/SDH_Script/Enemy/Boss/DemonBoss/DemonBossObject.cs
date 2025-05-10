using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// DemonBoss는 EnemyObject를 상속받는 보스 몬스터이다.
/// EnemyObject에서 제공하는 상태 시스템을 기반으로 Idle, Patrol, Chase, Attack 상태를 가진다.
/// </summary>
public class DemonBossObject : EnemyObject
{
    [Header("Attack Check Transform")]
    [SerializeField] private Transform closeAttackCheck; // 근거리 공격 판정 위치
    [SerializeField] private Transform midAttackCheck;   // 중거리 공격 판정 위치
    [SerializeField] private Transform longAttackCheck;  // 원거리 공격 판정 위치

    [Header("Attack Check Radius")]
    [SerializeField] Vector2 closeAttackCheckSize; // 근거리 공격 범위
    [SerializeField] Vector2 midAttackCheckSize;   // 중거리 공격 범위
    [SerializeField] Vector2 longAttackCheckSize;  // 원거리 공격 범위

    [SerializeField] private GameObject healthBar; // 체력바 프리팹
    private Slider slider;                         // 체력바 내부 슬라이더

    #region State
    public EnemyIdleState idleState { get; private set; }      // 대기 상태
    public EnemyPatrolState patrolState { get; private set; }  // 순찰 상태
    public EnemyChaseState chaseState { get; private set; }    // 추적 상태
    public EnemyAttackState attack1State { get; private set; } // 공격 1
    public EnemyAttackState attack2State { get; private set; } // 공격 2
    public EnemyAttackState attack3State { get; private set; } // 공격 3
    public EnemyAttackState attack4State { get; private set; } // 공격 4 (근거리)
    public EnemyAttackState attack5State { get; private set; } // 공격 5
    public EnemyAttackState attack6State { get; private set; } // 공격 6
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // 상태 초기화
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

        // 체력바 생성 및 초기화
        GameObject healthBar = Instantiate(this.healthBar);
        slider = healthBar.GetComponentInChildren<Slider>();
        slider.maxValue = MaxHp;
        slider.value = CurrentHp;

        // 초기 상태는 Idle
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        slider.value = CurrentHp; // 체력 갱신
    }

    /// <summary>
    /// SlimeBoss에서 이동한 경우를 대비해 방향을 보정한다.
    /// </summary>
    public void InitFacingDir(int direction)
    {
        if (facingDir != direction)
        {
            Flip(); // 방향 전환
        }
    }

    /// <summary>
    /// 플레이어를 탐지했을 때 추적 상태로 전환
    /// </summary>
    public override void EnterPlayerDetection()
    {
        stateMachine.ChangeState(chaseState);
    }

    /// <summary>
    /// 플레이어를 더 이상 탐지하지 않을 경우 순찰 상태로 전환
    /// </summary>
    public override void ExitPlayerDetection()
    {
        stateMachine.ChangeState(patrolState);
    }

    /// <summary>
    /// 플레이어와의 거리 조건에 따라 공격 상태 전환
    /// </summary>
    public override void CallAttackState()
    {
        float distanceX = Mathf.Abs(transform.position.x - PlayerManager.instance.player.transform.position.x);
        int rand;

        if (closeRangeDetected())
        {
            stateMachine.ChangeState(attack4State); // 근거리 전용 공격
        }
        else if (midRangeDetected())
        {
            // 중거리 공격: 랜덤하게 1~3, 5~6 중 선택
            rand = Random.Range(0, 5);
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
            // 원거리 공격: 2, 5, 6 중 하나 선택
            rand = Random.Range(0, 3);
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
    /// 대기 상태로 전환
    /// </summary>
    public override void CallIdleState()
    {
        stateMachine.ChangeState(idleState);
    }

    // 근거리 탐지 판정
    public Collider2D closeRangeDetected()
        => Physics2D.OverlapBox(closeAttackCheck.position, closeAttackCheckSize, 0f, whatIsPlayer);

    // 중거리 탐지 판정
    public Collider2D midRangeDetected()
        => Physics2D.OverlapBox(midAttackCheck.position, midAttackCheckSize, 0f, whatIsPlayer);

    // 원거리 탐지 판정
    public Collider2D longRangeDetected()
        => Physics2D.OverlapBox(longAttackCheck.position, longAttackCheckSize, 0f, whatIsPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        // 디버깅을 위해 Scene에서 공격 범위 확인
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(closeAttackCheck.position, closeAttackCheckSize);
        Gizmos.DrawWireCube(midAttackCheck.position, midAttackCheckSize);
        Gizmos.DrawWireCube(longAttackCheck.position, longAttackCheckSize);
    }
}
