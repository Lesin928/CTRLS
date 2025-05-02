using UnityEngine;
using System.Collections;

// TODO: (추가할일 적는부분)
// FIXME: (고칠거 적는부분)
// NOTE : (기타 작성)

/// <summary>
/// 플레이어가 공격하는 상태를 나타내는 클래스
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
        // 콤보 상태 전이
        if(playerObject.isCombo && playerObject.endAttack)
        {
            playerAnimation.stateMachine.ChangeState(playerAnimation.comboState);
        }

        // 공격중이 아니고 콤보중이 아닐 때 상태 전이
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
