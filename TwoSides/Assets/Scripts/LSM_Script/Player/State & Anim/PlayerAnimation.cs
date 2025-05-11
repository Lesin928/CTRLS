using UnityEngine;

// TODO: (추가할일 적는부분)
// FIXME: (고칠거 적는부분)
// NOTE : (기타 작성)

/// <summary>
/// 플레이어 애니메이션을 관리하는 클래스
/// </summary>
public class PlayerAnimation : MonoBehaviour
{
    #region Components  
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; } 
    public SpriteRenderer sr { get; private set; }

    public AudioSource audioSource { get; private set; } // 오디오 소스 컴포넌트

    public PlayerSFX playerSFX { get; private set; } // 플레이어 SFX 컴포넌트

    #endregion

    #region Playerfacing //플레이어의 방향을 나타내는 변수
    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;
    #endregion

    #region States 
    public PlayerStateMachine stateMachine { get; private set; } // 플레이어의 상태를 관리하는 상태 머신
    public PlayerObject playerObject { get; private set; } // 플레이어의 속성을 관리하는 객체 

    // 플레이어의 상태 
    public PlayerDashState dashState { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerComboState comboState { get; private set; }
    public PlayerSkillState skillState { get; private set; }
    public PlayerDeathState deathState { get; private set; }
    public PlayerParryState parryState { get; private set; }

    #endregion

    protected void Awake()
    {  
        anim = GetComponent<Animator>();
        rb = GetComponentInParent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        playerSFX = GetComponent<PlayerSFX>(); // 플레이어 SFX 컴포넌트 생성

        // 상태 머신 인스턴스 생성
        stateMachine = new PlayerStateMachine();
        playerObject = GetComponentInParent<PlayerObject>(); // 플레이어의 속성을 관리하는 객체 생성

        // 각 상태 인스턴스 생성 (this: 플레이어 객체, stateMachine: 상태 머신, "Idle"/"Move": 상태 이름)
        dashState = new PlayerDashState(this, stateMachine, playerObject, "Dash"); 
        idleState = new PlayerIdleState(this, stateMachine, playerObject, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, playerObject, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, playerObject, "JumpFall");
        airState = new PlayerAirState(this, stateMachine, playerObject, "JumpFall");
        attackState = new PlayerAttackState(this, stateMachine, playerObject, "Attack1");
        comboState = new PlayerComboState(this, stateMachine, playerObject, "Attack2");
        skillState = new PlayerSkillState(this, stateMachine, playerObject, "Skill");
        deathState = new PlayerDeathState(this, stateMachine, playerObject, "Death");
        parryState = new PlayerParryState(this, stateMachine, playerObject, "Parry");
    }
     
    protected void Start()
    { 
        // 게임 시작 시 초기 상태를 대기 상태(idleState)로 설정
        stateMachine.Initialize(idleState); 
    } 
    protected void Update()
    { 
        stateMachine.currentState.Update();  
    }
    #region 플립
    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    public int Getfacing()
    {
        return facingDir;
    }
    public void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if (_x < 0 && facingRight)
            Flip();
    }
    #endregion

}
