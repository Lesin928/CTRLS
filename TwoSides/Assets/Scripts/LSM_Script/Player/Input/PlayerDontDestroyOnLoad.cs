using UnityEngine;
using UnityEngine.SceneManagement;
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

    public void ResetSpawnPosition()
    {
        player.transform.localPosition = Vector3.zero;

        GameObject spawnPoint = GameObject.Find("Starting_Point");
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
            Debug.Log("Starting Point Position: " + spawnPoint.transform.position);
            Debug.Log("Player position set to Starting_Point.");
        }
        else
        {
            Debug.LogError("Starting_Point not found.");
        }
    }
}