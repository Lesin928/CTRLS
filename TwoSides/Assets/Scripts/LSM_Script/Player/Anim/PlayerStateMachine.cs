using UnityEngine;

// TODO: (�߰����� ���ºκ�)
// FIXME: (��ĥ�� ���ºκ�)
// NOTE : (��Ÿ �ۼ�)

/// <summary>
/// ���� �ӽ��� �����ϴ� Ŭ����
/// </summary>
public class PlayerStateMachine
{
    //������ ���¸� ��Ÿ���� ����
    public PlayerState currentState { get; private set; }

    /// <summary>
    /// ���� �ӽ��� �ʱ�ȭ�ϴ� �޼���
    /// </summary>
    /// <param name="_startState">�ʱ� ���·� ������ ����</param>
    public void Initialize(PlayerState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    /// <summary>
    /// ���¸� �����ϴ� �޼���
    /// </summary>
    /// <param name="_newState">���� �� ���� ����</param>
    public void ChangeState(PlayerState _newState)
    { 
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }

}
