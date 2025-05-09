using UnityEngine;

// TODO: (�߰����� ���ºκ�)
// FIXME: (��ĥ�� ���ºκ�)
// NOTE : (��Ÿ �ۼ�)

/// <summary>
/// ��� ���¸� ��Ÿ���� Ŭ����
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
