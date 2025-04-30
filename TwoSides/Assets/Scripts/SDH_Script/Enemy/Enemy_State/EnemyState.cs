using UnityEngine;

/// <summary>
/// 적의 상태를 정의하는 기본 클래스
/// </summary>
public class EnemyState
{
    protected EnemyStateMachine stateMachine; // 상태 머신 참조
    protected EnemyObject enemyBase;          // 적 오브젝트 기본 정보
    protected Rigidbody2D rb;                 // 적의 Rigidbody2D 컴포넌트
    protected bool triggerCalled;             // 애니메이션 트리거 호출 여부
    protected float stateTimer;               // 상태 지속 시간 타이머
    private string animBoolName;              // 상태 전환 시 사용할 애니메이션 Bool 이름

    // 상태 클래스 생성자
    public EnemyState(EnemyObject enemyBase, EnemyStateMachine stateMachine, string animBoolName)
    {
        this.enemyBase = enemyBase;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    /// <summary>
    /// 상태 진입 시 실행되는 함수
    /// </summary>
    public virtual void Enter()
    {
        triggerCalled = false;
        rb = enemyBase.rb;
        enemyBase.anim.SetBool(animBoolName, true);
    }

    /// <summary>
    /// 매 프레임 상태 업데이트
    /// </summary>
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    /// <summary>
    /// 상태 종료 시 실행되는 함수
    /// </summary>
    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBoolName, false);
    }

    /// <summary>
    /// 애니메이션 종료 시 호출되는 트리거 함수
    /// </summary>
    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
