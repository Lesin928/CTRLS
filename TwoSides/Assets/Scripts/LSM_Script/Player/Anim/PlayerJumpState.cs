using UnityEngine;
// TODO: (추가할일 적는부분)
// FIXME: (고칠거 적는부분)
// NOTE : (기타 작성)
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
            rb.AddForce(Vector2.up * 7f, ForceMode2D.Impulse); //7f는 playerobhect.jumpForce 구현해서 수정
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
