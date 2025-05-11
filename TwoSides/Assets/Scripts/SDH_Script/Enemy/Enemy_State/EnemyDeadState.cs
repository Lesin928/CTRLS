using UnityEngine;

/// <summary>
/// 적의 사망 상태를 처리하는 클래스입니다.
/// </summary>
public class EnemyDeadState : EnemyState
{
    // 적 사망 상태일 때의 동작을 정의합니다.
    public EnemyDeadState(EnemyObject enemyBase, EnemyStateMachine stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 적이 죽었을 때 실행되는 함수입니다.
    /// </summary>
    public override void Enter()
    {
        base.Enter();

        // 사망 상태일 때, 적의 속도를 0으로 설정하여 멈추게 만듭니다.
        enemyBase.SetZeroVelocity();

        // 게임 매니저에 적 사망을 알리고, 골드를 증가시킵니다.
        GameManager.Instance.OnMonsterDead();
        GameManager.Instance.SetGold(10);
    }

    /// <summary>
    /// 적이 죽은 후 실행되는 업데이트 함수입니다. 
    /// </summary>
    public override void Update()
    {
        base.Update();

        // 트리거가 호출되면 적 객체를 파괴합니다.
        if (triggerCalled)
            GameObject.Destroy(enemyBase.gameObject);
    }

    /// <summary>
    /// 적이 죽은 상태에서 종료될 때 호출되는 함수입니다.
    /// </summary>
    public override void Exit()
    {
        base.Exit();
    }
}
