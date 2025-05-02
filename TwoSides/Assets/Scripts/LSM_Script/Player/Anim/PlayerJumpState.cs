using UnityEngine;

// TODO: (�߰����� ���ºκ�)
// FIXME: (��ĥ�� ���ºκ�)
// NOTE : (��Ÿ �ۼ�)

/// <summary>
/// ���� ���¸� �����ϴ� Ŭ����
/// </summary>
public class PlayerJumpState : PlayerState
{ 
    public PlayerJumpState(PlayerAnimation _playerAnim, PlayerStateMachine _stateMachine, PlayerObject _playerObject, string _animBoolName)
        : base(_playerAnim, _stateMachine, _playerObject, _animBoolName) { }
    public override void Enter()
    {
        base.Enter();
        if (Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            rb.AddForce(Vector2.up * playerObject.jumpForce, ForceMode2D.Impulse); 
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
