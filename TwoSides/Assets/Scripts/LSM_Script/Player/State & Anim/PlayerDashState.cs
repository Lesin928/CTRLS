using UnityEngine;

// TODO: (�߰����� ���ºκ�)
// FIXME: (��ĥ�� ���ºκ�)
// NOTE : (��Ÿ �ۼ�)

/// <summary>
/// �÷��̾ �뽬�ϴ� ���¸� ��Ÿ���� Ŭ����
/// </summary>
public class PlayerDashState : PlayerState
{
    public PlayerDashState (PlayerAnimation _playerAnim, PlayerStateMachine _stateMachine, PlayerObject _playerObject, string _animBoolName)
        : base(_playerAnim, _stateMachine, _playerObject, _animBoolName) { }
    public override void Enter()
    {
        base.Enter(); 
    }
    public override void Update()
    {
        base.Update(); 
        //�뽬������ ���� ����
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
        base.Exit(); 
    }

}