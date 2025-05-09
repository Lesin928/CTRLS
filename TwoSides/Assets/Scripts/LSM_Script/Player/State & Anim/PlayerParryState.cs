using UnityEngine;

// TODO: (�߰����� ���ºκ�)
// FIXME: (��ĥ�� ���ºκ�)
// NOTE : (��Ÿ �ۼ�)

/// <summary>
/// �÷��̾��� �и� ���¸� ��Ÿ���� Ŭ����
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
        //���� ���� �����
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