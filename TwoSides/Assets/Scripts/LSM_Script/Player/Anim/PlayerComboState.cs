using UnityEngine;
using System.Collections;
using static UnityEngine.RuleTile.TilingRuleOutput;

// TODO: (�߰����� ���ºκ�)
// FIXME: (��ĥ�� ���ºκ�)
// NOTE : (��Ÿ �ۼ�)

/// <summary>
/// �÷��̾ �߰� �����ϴ� ���¸� ��Ÿ���� Ŭ����
/// </summary>
public class PlayerComboState : PlayerState
{
    public PlayerComboState(PlayerAnimation _playerAnim, PlayerStateMachine _stateMachine, PlayerObject _playerObject, string _animBoolName)
        : base(_playerAnim, _stateMachine, _playerObject, _animBoolName) { }
    public override void Enter()
    {
        base.Enter();
        playerObject.attackCollider1.SetActive(false);
        playerObject.attackCollider2.SetActive(true); 
        if (playerObject.MoveInput.x != 0)
        {
            playerObject.transform.position += new Vector3(playerAnimation.Getfacing() * 1f, 0f, 0f);
        }
    } 
    public override void Update()
    {
        base.Update();  
        // �������� �ƴ� �� ��������
        if (!playerObject.isAttack && !playerObject.isCombo)
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
        playerObject.attackCollider2.SetActive(false);
        base.Exit();
    } 
}
