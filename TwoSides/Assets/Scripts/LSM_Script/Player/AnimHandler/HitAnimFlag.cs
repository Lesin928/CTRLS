using UnityEngine;

// TODO: (�߰����� ���ºκ�)
// FIXME: (��ĥ�� ���ºκ�)
// NOTE : (��Ÿ �ۼ�)

/// <summary>
/// �ǰ� ����Ʈ �ִϸ��̼��� �̺�Ʈ ��鷯
/// </summary>
public class HitAnimFlag : MonoBehaviour
{
    /// <summary>
    /// ����Ʈ�� ������ ����� �޼���
    /// ������ �ִϸ��̼� �̺�Ʈ�� ����Ͽ��� ��.
    /// </summary>
    public void AnimFinished()
    {
        Destroy(gameObject);
    }
}
