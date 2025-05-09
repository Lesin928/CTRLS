using UnityEngine;

// TODO: (추가할일 적는부분)
// FIXME: (고칠거 적는부분)
// NOTE : (기타 작성)

/// <summary>
/// 대기 상태를 나타내는 클래스
/// </summary>
public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(PlayerAnimation _playerAnim, PlayerStateMachine _stateMachine, PlayerObject _playerObject, string _animBoolName)
        : base(_playerAnim, _stateMachine, _playerObject, _animBoolName) { }
    public override void Enter()
    {
        base.Enter(); 
    }
    public override void Update()
    { 
        if (!playerObject.IsGroundDetected())
        {
            stateMachine.ChangeState(playerAnimation.airState);
        }

        base.Update(); 
    }
    public override void Exit()
    {
        base.Exit();
    } 

}
