using UnityEngine;

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
