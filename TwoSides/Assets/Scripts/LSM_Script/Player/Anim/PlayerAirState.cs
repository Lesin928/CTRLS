using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
// TODO: (�߰����� ���ºκ�)
// FIXME: (��ĥ�� ���ºκ�)
// NOTE : (��Ÿ �ۼ�)
// �÷��̾ ���߿� ������ ���� ���¸� �����ϴ� Ŭ����
public class PlayerAirState : PlayerState
{
    public PlayerAirState(PlayerAnimation _playerAnim, PlayerStateMachine _stateMachine, string _animBoolName) : base(_playerAnim, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
     
    public override void Update()
    {
        base.Update();
         

        //�÷��̾ �ٴڿ� ������ Idle ���·� ��ȯ 

        /*
        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);

        if (xInput != 0)
            player.SetVelocity(player.moveSpeed * 0.8f * xInput, rb.linearVelocityY);
        */
    }
    public override void Exit()
    {
        base.Exit();
    }
}
