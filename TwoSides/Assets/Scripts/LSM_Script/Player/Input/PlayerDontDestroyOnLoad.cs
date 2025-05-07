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
    public PlayerDontDestroyOnLoad p;

    private void Awake()
    {
        p = FindAnyObjectByType<PlayerDontDestroyOnLoad>();
        
        // 씬 이동 시 중복 방지
        if (p != null && p != this)
        { 
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
