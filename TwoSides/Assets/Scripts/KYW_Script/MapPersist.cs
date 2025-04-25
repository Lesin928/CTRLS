using UnityEngine;

public class MapPersist : MonoBehaviour
{
    private static GameObject instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this.gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // 중복 제거
        }
    }
}
