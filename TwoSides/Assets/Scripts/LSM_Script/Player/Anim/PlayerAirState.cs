using UnityEngine;

// TODO: (추가할일 적는부분)
// FIXME: (고칠거 적는부분)
// NOTE : (기타 작성)

/// <summary>
/// 플레이어가 공중에 있을 때의 상태를 관리하는 클래스
/// </summary>
public class PlayerAirState : PlayerState
{
    public PlayerAirState(PlayerAnimation _playerAnim, PlayerStateMachine _stateMachine, PlayerObject _playerObject, string _animBoolName)
        : base(_playerAnim, _stateMachine, _playerObject, _animBoolName) { }
    public override void Enter()
    {
        base.Enter();
    }
     
    public override void Update()
    {
        base.Update();
        //Air에서의 상태 전이
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
    }
    public override void Exit()
    {
        base.Exit();
    }
}
