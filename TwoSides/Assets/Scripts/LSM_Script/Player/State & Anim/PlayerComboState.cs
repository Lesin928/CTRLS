using UnityEngine;
using System.Collections;
using static UnityEngine.RuleTile.TilingRuleOutput;

// TODO: (추가할일 적는부분)
// FIXME: (고칠거 적는부분)
// NOTE : (기타 작성)

/// <summary>
/// 플레이어가 추가 공격하는 상태를 나타내는 클래스
/// </summary>
public class PlayerComboState : PlayerState
{
    public PlayerComboState(PlayerAnimation _playerAnim, PlayerStateMachine _stateMachine, PlayerObject _playerObject, string _animBoolName)
        : base(_playerAnim, _stateMachine, _playerObject, _animBoolName) { }
    public override void Enter()
    {
        base.Enter();
        playerObject.EndAttack = false;
        playerObject.attackCollider1.SetActive(false); //이전 공격 판정 비활성화
        playerObject.attackCollider2.SetActive(true); //콤보 데미지 판정
        if (playerObject.MoveInput.x != 0 && playerObject.IsGroundDetected()) //공격시 이동
        {   
            playerObject.transform.position += new Vector3(playerAnimation.Getfacing() * 1f, 0f, 0f); 
        }
    } 
    public override void Update()
    { 
        base.Update();
        // 공격중이 아니고 콤보중이 아닐 때 상태 전이
        if (!playerObject.IsAttack && !playerObject.IsCombo)
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
        playerObject.attackCollider2.SetActive(false); //콤보 공격 판정 비활성화
        base.Exit();
    } 
}
