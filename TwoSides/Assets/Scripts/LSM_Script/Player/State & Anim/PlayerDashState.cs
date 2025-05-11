using UnityEngine; 

// TODO: (추가할일 적는부분)
// FIXME: (고칠거 적는부분)
// NOTE : (기타 작성)

/// <summary>
/// 플레이어가 대쉬하는 상태를 나타내는 클래스
/// </summary>
public class PlayerDashState : PlayerState
{
    public PlayerDashState (PlayerAnimation _playerAnim, PlayerStateMachine _stateMachine, PlayerObject _playerObject, string _animBoolName)
        : base(_playerAnim, _stateMachine, _playerObject, _animBoolName) { } 

    public override void Enter()
    {
        base.Enter();  
        playerAnimation.playerSFX.PlayClip(playerAnimation.playerSFX.dashClip); //대쉬 소리 재생   
        playerObject.IsInvincibility = true; //대쉬 중 무적
        playerObject.IsDashing = true;
        //회피 가능이면 극한회피
        if (playerObject.IsEvasion)
        {
            playerAnimation.GetComponent<VignetteControllerURP>().TriggerVignette(); 
            playerAnimation.GetComponent<ParryAfterImage>().StartDodgeEffect(); //잔상
            //회피 연출
            playerObject.IsCanParry = true; //패링 가능 
        }
    } 
    public override void Update()
    { 
        base.Update();
        //대쉬에서의 상태 전이 
        if (!playerObject.IsDashing)
        {
            if (playerObject.IsGroundDetected()) 
            {
                if (playerObject.MoveInput.x == 0) 
                {
                    stateMachine.ChangeState(playerAnimation.idleState);
                }
                else if (playerObject.MoveInput.x != 0)
                {
                    stateMachine.ChangeState(playerAnimation.moveState);
                }
            }
            else if (!playerObject.IsGroundDetected())
            {
                stateMachine.ChangeState(playerAnimation.airState);
            }                
        }  
    }


    public override void Exit()
    {        
        playerObject.IsInvincibility = false; //대쉬 중 무적 해제
        playerObject.IsEvasion = false; 
        base.Exit();
    } 








}