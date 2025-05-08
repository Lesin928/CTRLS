using UnityEngine;
using UnityEngine.SceneManagement;
// TODO: (추가할일 적는부분)
// FIXME: (고칠거 적는부분)
// NOTE : (기타 작성)

/// <summary>
/// 이 클래스를 가진 오브젝트는 씬이 바뀌어도 파괴되지 않음
/// 플레이어에게만 사용하는 클래스
/// </summary>
public class PlayerDontDestroyOnLoad : MonoBehaviour
{
    private PlayerDontDestroyOnLoad p;
    //플레이어 오브젝트
    private GameObject player;

    private void Awake()
    {
        p = FindAnyObjectByType<PlayerDontDestroyOnLoad>();
        //자식 오브젝트중 플레이어 오브젝트의 Transform 컴포넌트를 가져옴
        player = GameObject.Find("Player");

        // 씬 이동 시 중복 방지
        if (p != null && p != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (this == null)
        {
            Debug.LogError("PlayerDontDestroyOnLoad 인스턴스가 null입니다!");
            return;
        }

        player.transform.localPosition = Vector3.zero;

        GameObject spawnPoint = GameObject.Find("Starting_Point");
        if (spawnPoint == null)
            Debug.LogError("Starting_Point 오브젝트를 찾을 수 없습니다.");
        if (spawnPoint != null && spawnPoint.CompareTag("StartingPoint"))
        {
            Debug.Log("Starting_Point 오브젝트를 찾았습니다.");
            transform.position = spawnPoint.transform.position;
        }
    }
}