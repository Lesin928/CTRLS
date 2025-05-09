using UnityEngine;

// TODO: (�߰����� ���ºκ�)
// FIXME: (��ĥ�� ���ºκ�)
// NOTE : (��Ÿ �ۼ�)

/// <summary>
/// �÷��̾� �ִϸ��̼��� �����ϴ� Ŭ����
/// </summary>
public class PlayerAnimation : MonoBehaviour
{
    #region Components  
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; } 
    public SpriteRenderer sr { get; private set; }
     
    #endregion

    #region Playerfacing //�÷��̾��� ������ ��Ÿ���� ����
    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;
    #endregion

    #region States 
    public PlayerStateMachine stateMachine { get; private set; } // �÷��̾��� ���¸� �����ϴ� ���� �ӽ�
    public PlayerObject playerObject { get; private set; } // �÷��̾��� �Ӽ��� �����ϴ� ��ü 

    // �÷��̾��� ���� 
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

        // ���� �ӽ� �ν��Ͻ� ����
        stateMachine = new PlayerStateMachine();
        playerObject = GetComponentInParent<PlayerObject>(); // �÷��̾��� �Ӽ��� �����ϴ� ��ü ����

        // �� ���� �ν��Ͻ� ���� (this: �÷��̾� ��ü, stateMachine: ���� �ӽ�, "Idle"/"Move": ���� �̸�)
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
        // ���� ���� �� �ʱ� ���¸� ��� ����(idleState)�� ����
        stateMachine.Initialize(idleState); 
    } 
    protected void Update()
    { 
        stateMachine.currentState.Update();  
    }
    #region �ø�
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
