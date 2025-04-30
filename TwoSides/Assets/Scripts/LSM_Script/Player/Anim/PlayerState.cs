using UnityEngine;

// TODO: (�߰����� ���ºκ�)
// FIXME: (��ĥ�� ���ºκ�)
// NOTE : (��Ÿ �ۼ�)
public class PlayerState
{
    #region Components //Player�� �ν����Ϳ� �ִ� ������Ʈ��
    protected PlayerStateMachine stateMachine;
    protected PlayerAnimation playerAnimation; 
    protected PlayerObject playerObject;    
    protected PlayerController playerController;    
    protected Rigidbody2D rb;
    #endregion

    #region Variables // �÷��̾��� ���¸� ��Ÿ���� ������
    private string animBoolName;
    protected float xInput;
    protected float yInput;   

    protected float stateTimer;
    protected bool triggerCalled;
    #endregion  

    public PlayerState(PlayerAnimation _playerAnim, PlayerStateMachine _stateMachine, PlayerObject _playerObject, string _animBoolName)
    {
        this.playerAnimation = _playerAnim;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
        this.playerObject = _playerObject;
    } 

    public virtual void Enter()
    {   
        playerAnimation.anim.SetBool(animBoolName, true);
        rb = playerAnimation.rb;
        triggerCalled = false; 
    }

    public virtual void Update()
    { 
        //stateTimer -= Time.deltaTime; 
        playerAnimation.anim.SetFloat("yVelocity", rb.linearVelocityY); 
    }

    public virtual void Exit()
    {
        playerAnimation.anim.SetBool(animBoolName, false);
    }

    /// <summary>
    /// �ִϸ��̼��� ������ �� ȣ��Ǵ� �޼ŵ�
    /// </summary>
    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }


}
