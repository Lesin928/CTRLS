using UnityEngine;
using UnityEngine.Windows;
// TODO: (�߰����� ���ºκ�)
// FIXME: (��ĥ�� ���ºκ�)
// NOTE : (��Ÿ �ۼ�)
public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(PlayerAnimation _playerAnim, PlayerStateMachine _stateMachine, string _animBoolName) : base(_playerAnim, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    } 
     
    public override void Update()
    {
        base.Update();
    } 

    public override void Exit()
    {
        base.Exit();
    }
}
