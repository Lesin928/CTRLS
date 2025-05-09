using UnityEngine;

/// <summary>
/// 적이 죽었을 때의 상태를 처리하는 클래스
/// </summary>
public class EnemyDeadState : EnemyState
{
    // 생성자: 상태머신, 애니메이션 bool 이름을 기반으로 Dead 상태를 초기화
    public EnemyDeadState(EnemyObject enemyBase, EnemyStateMachine stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    /// <summary>
    /// 적이 Dead 상태에 진입할 때 호출
    /// </summary>
    public override void Enter()
    {
        base.Enter();

        // 죽었으므로 적의 속도를 0으로 설정
        enemyBase.SetZeroVelocity();
    }

    /// <summary>
    /// 매 프레임마다 상태를 갱신함. 트리거가 호출되면 오브젝트를 제거함.
    /// </summary>
    public override void Update()
    {
        base.Update();

        // 적 오브젝트 제거 조건 확인
        if (triggerCalled)
            GameObject.Destroy(enemyBase.gameObject);
    }

    /// <summary>
    /// 적이 Dead 상태에서 나갈 때 호출
    /// </summary>
    public override void Exit()
    {
        base.Exit();
    }
}
