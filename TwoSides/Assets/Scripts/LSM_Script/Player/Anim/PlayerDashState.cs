using UnityEngine;
// TODO: (�߰����� ���ºκ�)
// FIXME: (��ĥ�� ���ºκ�)
// NOTE : (��Ÿ �ۼ�)
public class PlayerDashState : PlayerState
{
    public PlayerDashState (PlayerAnimation _playerAnim, PlayerStateMachine _stateMachine, string _animBoolName) : base(_playerAnim, _stateMachine, _animBoolName)
    {

    } 
    /*
    public override void Enter()
    {
        base.Enter();

        player.skill.clone.CreateClone(player.transform, Vector3.zero);

        stateTimer = player.dashDuration;
    }


    public override void Update()
    {
        base.Update();

        if (!player.IsGroundDetected() && player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlide);


        player.SetVelocity(player.dashSpeed * player.dashDir, 0);

        if (stateTimer < 0)
            stateMachine.ChangeState(player.idleState);
    }


    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, rb.linearVelocityY);

    } 
    */
}