using UnityEngine;

// TODO: (�߰����� ���ºκ�)
// FIXME: (��ĥ�� ���ºκ�)
// NOTE : (��Ÿ �ۼ�)

/// <summary>
/// �÷��̾ ���߿� ���� ���� ���¸� �����ϴ� Ŭ����
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
        //Air������ ���� ����
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
