using UnityEngine;
// TODO: (�߰����� ���ºκ�)
// FIXME: (��ĥ�� ���ºκ�)
// NOTE : (��Ÿ �ۼ�)
public class PlayerJumpState : PlayerState
{ 
    public PlayerJumpState(PlayerAnimation _playerAnim, PlayerStateMachine _stateMachine, string _animBoolName) : base(_playerAnim, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            rb.AddForce(Vector2.up * 7f, ForceMode2D.Impulse); //7f�� playerobhect.jumpForce �����ؼ� ����
        }
        //rb.linearVelocity = new Vector2(rb.linearVelocityX, playerObject.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (rb.linearVelocityY < 0)
            stateMachine.ChangeState(playerAnimation.airState); 
    }
} 
