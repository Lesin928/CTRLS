using UnityEngine;

public class PlayerDontDestroyOnLoad : MonoBehaviour
{
    public PlayerDontDestroyOnLoad p;

    private void Awake()
    {
        p = FindAnyObjectByType<PlayerDontDestroyOnLoad>();
        
        // 씬 이동 시 중복 방지
        if (p != null && p != this)
        {
            Debug.Log("중복 플레이어 감지, Destroy");   
            //Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
