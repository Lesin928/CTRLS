using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
// TODO: (추가할일 적는부분)
// FIXME: (고칠거 적는부분)
// NOTE : (기타 작성)
// 플레이어가 공중에 떠있을 때의 상태를 정의하는 클래스
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
        //대쉬가 끝났는데 공중에 남아있으면 상태 전환   
    }
    public override void Exit()
    {
        base.Exit();
    }
}
