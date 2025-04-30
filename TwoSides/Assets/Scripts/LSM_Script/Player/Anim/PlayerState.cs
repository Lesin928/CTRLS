using UnityEngine;

// TODO: (추가할일 적는부분)
// FIXME: (고칠거 적는부분)
// NOTE : (기타 작성)
public class PlayerState
{
    #region Components //Player의 인스펙터에 있는 컴포넌트들
    protected PlayerStateMachine stateMachine;
    protected PlayerAnimation playerAnimation; 
    protected PlayerObject playerObject;    
    protected PlayerController playerController;    
    protected Rigidbody2D rb;
    #endregion

    #region Variables // 플레이어의 상태를 나타내는 변수들
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
    /// 애니메이션이 끝났을 때 호출되는 메셔드
    /// </summary>
    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }


}
