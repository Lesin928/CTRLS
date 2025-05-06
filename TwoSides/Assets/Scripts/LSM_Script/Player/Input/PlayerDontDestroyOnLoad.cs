using UnityEngine;
// TODO: (�߰����� ���ºκ�)
// FIXME: (��ĥ�� ���ºκ�)
// NOTE : (��Ÿ �ۼ�)

/// <summary>
/// �� Ŭ������ ���� ������Ʈ�� ���� �ٲ� �ı����� ����
/// �÷��̾�Ը� ����ϴ� Ŭ����
/// </summary>
public class PlayerDontDestroyOnLoad : MonoBehaviour
{
    public PlayerDontDestroyOnLoad p;

    private void Awake()
    {
        p = FindAnyObjectByType<PlayerDontDestroyOnLoad>();
        
        // �� �̵� �� �ߺ� ����
        if (p != null && p != this)
        { 
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
