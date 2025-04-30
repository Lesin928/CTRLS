using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

// TODO: (�߰����� ���ºκ�)
// FIXME: (��ĥ�� ���ºκ�)
// NOTE : (��Ÿ �ۼ�)

public class PlayerAnimation : MonoBehaviour
{
    #region Components //PlayerAnimation ��ũ��Ʈ�� �ν����Ϳ� �ִ� ������Ʈ��
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    //public EntityFX fx { get; private set; }
    public SpriteRenderer sr { get; private set; }
    #endregion

    #region Playerfacing //�÷��̾��� ������ ��Ÿ���� ����
    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;
    #endregion

    #region States // �÷��̾��� ���¸� �����ϴ� ���� �ӽ�
    public PlayerStateMachine stateMachine { get; private set; }

    // �÷��̾��� ���� 
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

        // ���� �ӽ� �ν��Ͻ� ����
        stateMachine = new PlayerStateMachine();

        // �� ���� �ν��Ͻ� ���� (this: �÷��̾� ��ü, stateMachine: ���� �ӽ�, "Idle"/"Move": ���� �̸�)
        dashState = new PlayerDashState(this, stateMachine, "Dash"); 
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "JumpFall");
        airState = new PlayerAirState(this, stateMachine, "JumpFall");
        attackState = new PlayerAttackState(this, stateMachine, "Attack1");

    }
     
    protected void Start()
    { 
        // ���� ���� �� �ʱ� ���¸� ��� ����(idleState)�� ����
        stateMachine.Initialize(idleState); 
    } 
    protected void Update()
    { 
        stateMachine.currentState.Update();  
    }

    #region �ø�
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
