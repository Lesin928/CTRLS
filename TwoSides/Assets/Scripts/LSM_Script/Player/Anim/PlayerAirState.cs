using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
// TODO: (�߰����� ���ºκ�)
// FIXME: (��ĥ�� ���ºκ�)
// NOTE : (��Ÿ �ۼ�)
// �÷��̾ ���߿� ������ ���� ���¸� �����ϴ� Ŭ����
public class PlayerAirState : PlayerState
{
    public PlayerAirState(PlayerAnimation _playerAnim, PlayerStateMachine _stateMachine, PlayerObject _playerObject, string _animBoolName) : base(_playerAnim, _stateMachine, _playerObject, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
     
    public override void Update()
    {
        base.Update();       
        //�뽬�� �����µ� ���߿� ���������� ���� ��ȯ   
    }
    public override void Exit()
    {
        base.Exit();
    }
}
