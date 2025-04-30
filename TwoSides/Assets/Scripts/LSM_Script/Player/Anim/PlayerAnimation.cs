using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

// TODO: (추가할일 적는부분)
// FIXME: (고칠거 적는부분)
// NOTE : (기타 작성)

public class PlayerAnimation : MonoBehaviour
{
    #region Components //PlayerAnimation 스크립트의 인스펙터에 있는 컴포넌트들
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    //public EntityFX fx { get; private set; }
    public SpriteRenderer sr { get; private set; }
    #endregion

    #region Playerfacing //플레이어의 방향을 나타내는 변수
    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;
    #endregion

    #region States // 플레이어의 상태를 관리하는 상태 머신
    public PlayerStateMachine stateMachine { get; private set; }

    // 플레이어의 상태 
    public PlayerDashState dashState { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerAttackState attackState { get; private set; }

    #endregion

    protected void Awake()
    {  
        anim = GetComponent<Animator>();
        rb = GetComponentInParent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        // 상태 머신 인스턴스 생성
        stateMachine = new PlayerStateMachine();

        // 각 상태 인스턴스 생성 (this: 플레이어 객체, stateMachine: 상태 머신, "Idle"/"Move": 상태 이름)
        dashState = new PlayerDashState(this, stateMachine, "Dash"); 
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "JumpFall");
        airState = new PlayerAirState(this, stateMachine, "JumpFall");
        attackState = new PlayerAttackState(this, stateMachine, "Attack1");

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
    public virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }


    public virtual void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if (_x < 0 && facingRight)
            Flip();

    }

    #endregion

}
