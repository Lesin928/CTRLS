using UnityEngine;

// TODO: (추가할일 적는부분)
// FIXME: (고칠거 적는부분)
// NOTE : (기타 작성)

/// <summary>
/// 플레이어의 패링 상태를 나타내는 클래스
/// </summary>
public class PlayerParryState : PlayerState
{
    public PlayerParryState(PlayerAnimation _playerAnim, PlayerStateMachine _stateMachine, PlayerObject _playerObject, string _animBoolName)
        : base(_playerAnim, _stateMachine, _playerObject, _animBoolName) { }


    public override void Enter()
    {        
        base.Enter();
    }
    public override void Update()
    {
        //현재 상태 디버그
        Debug.Log("ParryState");
        base.Update(); 
        if (!playerObject.IsCanParry)
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
        base.Exit();
    }


}