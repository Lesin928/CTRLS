using UnityEngine;

// TODO: (추가할일 적는부분)
// FIXME: (고칠거 적는부분)
// NOTE : (기타 작성)

/// <summary>
/// 점프 상태를 관리하는 클래스
/// </summary>
public class PlayerJumpState : PlayerState
{ 
    public PlayerJumpState(PlayerAnimation _playerAnim, PlayerStateMachine _stateMachine, PlayerObject _playerObject, string _animBoolName)
        : base(_playerAnim, _stateMachine, _playerObject, _animBoolName) { }
    public override void Enter()
    {
        playerAnimation.playerSFX.PlayClip(playerAnimation.playerSFX.jumpClip); //대쉬 소리 재생  
        playerObject.IsAttack = false;
        base.Enter();
        if (Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            rb.AddForce(Vector2.up * playerObject.JumpForce, ForceMode2D.Impulse); 
        } 
    } 
    public override void Exit()
    {
        base.Exit();
    } 
    public override void Update()
    { 
        base.Update();
        if (rb.linearVelocityY < 0)
        {
            stateMachine.ChangeState(playerAnimation.airState);
        } 
    }
} 
