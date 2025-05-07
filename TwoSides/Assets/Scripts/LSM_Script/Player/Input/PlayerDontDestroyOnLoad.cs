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

    private void Start()
    {
        //�÷��̾� ��ġ �ʱ�ȭ 
        GameObject spawnPoint = GameObject.Find("Starting_Point");
        if (spawnPoint != null && spawnPoint.CompareTag("StartingPoint"))
        {
            transform.position = spawnPoint.transform.position;
        }
    }
}
