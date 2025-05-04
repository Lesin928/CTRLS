using UnityEngine;

/// <summary>
/// ���� ���� ���̸� �����ϴ� ������Ʈ �ӽ� Ŭ����
/// </summary>
public class EnemyStateMachine
{
    public EnemyState currentState { get; private set; } // ���� Ȱ��ȭ�� ����
    public bool isAttacking;

    /// <summary>
    /// ���� �ӽ��� �ʱ� ���·� �����ϰ� ���¸� ���Խ�Ŵ
    /// </summary>
    /// <param name="startState">�ʱ� ����</param>
    public void Initialize(EnemyState startState)
    {
        currentState = startState;
        currentState.Enter();
    }

    /// <summary>
    /// ���� ���¿��� ���ο� ���·� �����ϰ� ���Խ�Ŵ
    /// </summary>
    /// <param name="newState">������ �� ����</param>
    public void ChangeState(EnemyState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
