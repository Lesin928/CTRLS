using UnityEngine;

// TODO: (�߰����� ���ºκ�)
// FIXME: (��ĥ�� ���ºκ�)
// NOTE : (��Ÿ �ۼ�)

/// <summary>
/// �÷��̾ ��ų�� ����ϴ� ���¸� ��Ÿ���� Ŭ����
/// </summary>
public class PlayerSkillState : PlayerState
{
    public PlayerSkillState(PlayerAnimation _playerAnim, PlayerStateMachine _stateMachine, PlayerObject _playerObject, string _animBoolName)
        : base(_playerAnim, _stateMachine, _playerObject, _animBoolName) { }
    public override void Enter()
    {
        base.Enter();
        playerObject.IsSkill = true;
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y); //�̲����� ���� 
    }
    public override void Update()
    {
        base.Update();
        //��ų������ ���� ����
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