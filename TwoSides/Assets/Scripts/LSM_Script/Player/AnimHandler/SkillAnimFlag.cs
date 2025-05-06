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

    /// <summary>
    /// ��ų�� �������� �� ����Ǵ� �ż���
    /// </summary>
    public void Strike()
    { 
        if (playerObject.skillCollider.activeSelf)
        {
            playerObject.skillCollider.SetActive(true);
        }
    } 
    /// <summary>
    /// ������Ⱑ ���� �� ����Ǵ� �޼���
    /// </summary>
    public void StrikeFinished()
    {
        if (playerObject.skillCollider.activeSelf)
        {
            playerObject.skillCollider.SetActive(false);
        }

    }
}
