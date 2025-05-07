using UnityEngine;

// TODO: (�߰����� ���ºκ�)
// FIXME: (��ĥ�� ���ºκ�)
// NOTE : (��Ÿ �ۼ�)

/// <summary>
/// ���� ����Ʈ �ִϸ��̼��� �̺�Ʈ ��鷯
/// </summary>
public class AttackAnimFlag : MonoBehaviour
{
    public PlayerObject playerObject { get; private set; }
    private void Awake()
    {
        playerObject = GetComponentInParent<PlayerObject>();
    }

    /// <summary>
    /// ù ��° ������ ������ �޽��� �����ϴ� �޼���
    /// ������ �ִϸ��̼� �̺�Ʈ�� ����Ͽ��� ��.
    /// </summary>
    public void FirstAttackFinished()
    {
        if (playerObject.attackCollider1.activeSelf)
        {
            playerObject.attackCollider1.SetActive(false);
        }
        playerObject.EndAttack = true;
    }

    /// <summary>
    /// �� ��° ������ ������ �޽��� �����ϴ� �޼���
    /// ������ �ִϸ��̼� �̺�Ʈ�� ����Ͽ��� ��.
    /// </summary>
    public void SecondAttackFinished()
    {
        if (playerObject.attackCollider2.activeSelf)
        { 
            playerObject.attackCollider2.SetActive(false); 
        }
          
    }
}
