using UnityEngine;
using System.Collections;
using static UnityEngine.RuleTile.TilingRuleOutput;

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
        playerObject.attackCollider1.SetActive(true); //데미지 판정
        playerObject.IsAttack = true;
        if (playerObject.IsGroundDetected())
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y); //미끄러짐 방지
            //공중에서 바닥에 닿으면 멈추게 하는게 좋지 않을까? 리팩토링 예정
        }        
        if (playerObject.MoveInput.x != 0 && playerObject.IsGroundDetected()) //공격 중 이동
        {
            playerObject.transform.position += new Vector3(playerAnimation.Getfacing() * 1f, 0f, 0f);
        }
    }  
  public override void Update()
    { 
        base.Update(); 
        // 콤보 상태 전이
        if(playerObject.IsCombo && playerObject.EndAttack)
        {
            playerAnimation.stateMachine.ChangeState(playerAnimation.comboState);
        } 
        // 공격중이 아니고 콤보중이 아닐 때 상태 전이
        if (!playerObject.IsCombo && !playerObject.IsAttack)
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
        playerObject.attackCollider1.SetActive(false); //데미지 판정
    } 
}
