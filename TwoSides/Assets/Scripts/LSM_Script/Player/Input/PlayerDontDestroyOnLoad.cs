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
    private PlayerDontDestroyOnLoad p;
    //�÷��̾� ������Ʈ
    private GameObject player;

    private void Awake()
    {
        p = FindAnyObjectByType<PlayerDontDestroyOnLoad>();
        //�ڽ� ������Ʈ�� �÷��̾� ������Ʈ�� Transform ������Ʈ�� ������
        player = GameObject.Find("Player");

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
        // �� �̵� �� �÷��̾� ��ġ �ʱ�ȭ
        player.transform.localPosition = Vector3.zero;

        GameObject spawnPoint = GameObject.Find("Starting_Point");
        if (spawnPoint != null && spawnPoint.CompareTag("StartingPoint"))
        {
            transform.position = spawnPoint.transform.position;
        }
    }
}