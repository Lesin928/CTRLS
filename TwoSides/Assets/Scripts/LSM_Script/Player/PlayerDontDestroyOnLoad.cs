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
            Debug.Log("�ߺ� �÷��̾� ����, Destroy");   
            //Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
