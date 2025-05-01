using UnityEngine;
// TODO: (추가할일 적는부분)
// FIXME: (고칠거 적는부분)
// NOTE : (기타 작성)
public class PlayerDashState : PlayerState
{
    public PlayerDashState (PlayerAnimation _playerAnim, PlayerStateMachine _stateMachine, PlayerObject _playerObject, string _animBoolName) : base(_playerAnim, _stateMachine, _playerObject, _animBoolName)
    { 
    }  
    
    public override void Enter()
    {
        base.Enter();
        /*
        player.skill.clone.CreateClone(player.transform, Vector3.zero);

        stateTimer = player.dashDuration;*/
    }


    public override void Update()
    {
        base.Update(); 
        if (!playerObject.GetDashing())
        { 
            if (playerObject.IsGroundDetected())
                stateMachine.ChangeState(playerAnimation.idleState);
            else
                stateMachine.ChangeState(playerAnimation.airState);
        }   
        /*
        if (!player.IsGroundDetected() && player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlide);


        player.SetVelocity(player.dashSpeed * player.dashDir, 0);

        if (stateTimer < 0)
            stateMachine.ChangeState(player.idleState);*/
    }


    public override void Exit()
    { 
        base.Exit();

        //player.SetVelocity(0, rb.linearVelocityY);
    }

}