using UnityEditor;
using UnityEngine;

public class EnemyDeadState : EnemyState
{
    public EnemyDeadState(EnemyObject enemyBase, EnemyStateMachine stateMachine, string animBoolName) 
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 상태 진입 시 실행
    /// </summary>
    public override void Enter()
    {
        base.Enter();

        // 정지 상태로 전환
        enemyBase.SetZeroVelocity();
    }

    /// <summary>
    /// 매 프레임 대기 상태 로직 실행
    /// </summary>
    public override void Update()
    {
        Debug.Log("Dead");

        base.Update();

        //GameManager.Instance.OnMonsterDead();

        if (triggerCalled)
            GameObject.Destroy(enemyBase.gameObject);
    }

    /// <summary>
    /// 상태 종료 시 실행
    /// </summary>
    public override void Exit()
    {
        base.Exit();
    }
}
