using UnityEngine;

/// <summary>
/// 적의 상태 전이를 제어하는 스테이트 머신 클래스
/// </summary>
public class EnemyStateMachine
{
    public EnemyState currentState { get; private set; } // 현재 활성화된 상태
    public bool isAttacking;

    /// <summary>
    /// 상태 머신을 초기 상태로 설정하고 상태를 진입시킴
    /// </summary>
    /// <param name="startState">초기 상태</param>
    public void Initialize(EnemyState startState)
    {
        currentState = startState;
        currentState.Enter();
    }

    /// <summary>
    /// 현재 상태에서 새로운 상태로 전이하고 진입시킴
    /// </summary>
    /// <param name="newState">전이할 새 상태</param>
    public void ChangeState(EnemyState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
