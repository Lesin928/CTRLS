using UnityEngine;
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
    }
    private void Start()
    {
        // 씬 이동 시 플레이어 위치 초기화
        player.transform.localPosition = Vector3.zero;

        GameObject spawnPoint = GameObject.Find("Starting_Point");
        if (spawnPoint != null && spawnPoint.CompareTag("StartingPoint"))
        {
            transform.position = spawnPoint.transform.position;
        }
    }
}