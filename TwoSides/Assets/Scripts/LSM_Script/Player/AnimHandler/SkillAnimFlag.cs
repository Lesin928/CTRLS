using UnityEngine;

// TODO: (�߰����� ���ºκ�)
// FIXME: (��ĥ�� ���ºκ�)
// NOTE : (��Ÿ �ۼ�)

/// <summary>
/// ��ų �ִϸ��̼��� �̺�Ʈ ��鷯
/// </summary>
public class SkillAnimFlag : MonoBehaviour
{ 
    public PlayerObject playerObject { get; private set; }
    private void Awake()
    {
        playerObject = GetComponentInParent<PlayerObject>();
    }
    /// <summary>
    /// ��ų ����� ������ �޽����� �����ϴ� �޼���
    /// ������ �ִϸ��̼� �̺�Ʈ�� ����Ͽ��� ��.
    /// </summary>
    public void SkillkFinished()
    { 
        playerObject.IsSkill = false;
    } 
}
