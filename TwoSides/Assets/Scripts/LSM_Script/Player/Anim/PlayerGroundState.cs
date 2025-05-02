using UnityEngine;

// TODO: (추가할일 적는부분)
// FIXME: (고칠거 적는부분)
// NOTE : (기타 작성)

/// <summary>
/// 플레이어가 땅에 있는 상태를 나타내는 클래스
/// </summary>
public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(PlayerAnimation _playerAnim, PlayerStateMachine _stateMachine, PlayerObject _playerObject, string _animBoolName)
        : base(_playerAnim, _stateMachine, _playerObject, _animBoolName) { }

    public override void Enter()
    {
        base.Enter();
    } 
    public override void Update()
    { 
        base.Update();
        if (!playerObject.IsDashing)
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
