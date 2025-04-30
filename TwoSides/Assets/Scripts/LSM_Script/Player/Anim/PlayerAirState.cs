using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
// TODO: (추가할일 적는부분)
// FIXME: (고칠거 적는부분)
// NOTE : (기타 작성)
// 플레이어가 공중에 떠있을 때의 상태를 정의하는 클래스
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
         

        //플레이어가 바닥에 닿으면 Idle 상태로 전환 

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
