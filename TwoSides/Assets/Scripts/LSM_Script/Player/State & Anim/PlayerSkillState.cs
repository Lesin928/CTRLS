using UnityEngine;

// TODO: (추가할일 적는부분)
// FIXME: (고칠거 적는부분)
// NOTE : (기타 작성)

/// <summary>
/// 플레이어가 스킬을 사용하는 상태를 나타내는 클래스
/// </summary>
public class PlayerSkillState : PlayerState
{
    public PlayerSkillState(PlayerAnimation _playerAnim, PlayerStateMachine _stateMachine, PlayerObject _playerObject, string _animBoolName)
        : base(_playerAnim, _stateMachine, _playerObject, _animBoolName) { }
    public override void Enter()
    {
        base.Enter();
        playerObject.IsSkill = true;
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y); //미끄러짐 방지 
    }
    public override void Update()
    {
        base.Update();
        //스킬에서의 상태 전이
        if (!playerObject.IsSkill)
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
        playerObject.IsSkill = false;
        base.Exit();
    }

}