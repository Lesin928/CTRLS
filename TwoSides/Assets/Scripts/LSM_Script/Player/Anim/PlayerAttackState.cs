using UnityEngine;
using System.Collections;

// TODO: (�߰����� ���ºκ�)
// FIXME: (��ĥ�� ���ºκ�)
// NOTE : (��Ÿ �ۼ�)

/// <summary>
/// �÷��̾ �����ϴ� ���¸� ��Ÿ���� Ŭ����
/// </summary>
public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(PlayerAnimation _playerAnim, PlayerStateMachine _stateMachine, PlayerObject _playerObject, string _animBoolName)
        : base(_playerAnim, _stateMachine, _playerObject, _animBoolName) { }
    public override void Enter()
    {
        base.Enter();
        playerObject.attackCollider1.SetActive(true); 
    } 

  public override void Update()
    {
        base.Update(); 
        // �޺� ���� ����
        if(playerObject.isCombo && playerObject.endAttack)
        {
            playerAnimation.stateMachine.ChangeState(playerAnimation.comboState);
        }

        // �������� �ƴϰ� �޺����� �ƴ� �� ���� ����
        if (!playerObject.isCombo && !playerObject.isAttack)
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
