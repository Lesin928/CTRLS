using UnityEngine;
using System.Collections;
using static UnityEngine.RuleTile.TilingRuleOutput;

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
        playerObject.attackCollider1.SetActive(true); //������ ����
        playerObject.IsAttack = true;
        if (playerObject.IsGroundDetected())
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y); //�̲����� ����
            //���߿��� �ٴڿ� ������ ���߰� �ϴ°� ���� ������? �����丵 ����
        }        
        if (playerObject.MoveInput.x != 0 && playerObject.IsGroundDetected()) //���� �� �̵�
        {
            playerObject.transform.position += new Vector3(playerAnimation.Getfacing() * 1f, 0f, 0f);
        }
    }  
  public override void Update()
    { 
        base.Update(); 
        // �޺� ���� ����
        if(playerObject.IsCombo && playerObject.EndAttack)
        {
            playerAnimation.stateMachine.ChangeState(playerAnimation.comboState);
        } 
        // �������� �ƴϰ� �޺����� �ƴ� �� ���� ����
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
        playerObject.attackCollider1.SetActive(false); //������ ����
    } 
}
