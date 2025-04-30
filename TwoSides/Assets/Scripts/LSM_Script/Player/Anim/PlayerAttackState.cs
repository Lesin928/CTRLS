using UnityEngine;
// TODO: (추가할일 적는부분)
// FIXME: (고칠거 적는부분)
// NOTE : (기타 작성)
public class PlayerAttackState : PlayerGroundState
{
    public PlayerAttackState(PlayerAnimation _playerAnim, PlayerStateMachine _stateMachine, string _animBoolName)
        : base(_playerAnim, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    /*


    public override void Update()
    {
        base.Update();


        player.SetVelocity(xInput * player.moveSpeed, rb.linearVelocityY);

        if (xInput == 0 || player.IsWallDetected())
            stateMachine.ChangeState(player.idleState);



    }
    */

    public override void Exit()
    {
        base.Exit();
    }




}
