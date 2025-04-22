using UnityEngine;

public class GameStarter : MonoBehaviour
{
    private static bool initialized = false;

    private void Awake()
    {
        if (initialized) return;
        initialized = true;

        // GameManager
        if (GameManager.Instance == null)
        {
            GameObject gmPrefab = Resources.Load<GameObject>("GameManager");
            if (gmPrefab != null)
                Instantiate(gmPrefab);
            else
                Debug.LogError("GameManager is null");
        }

        // HUDManager
        if (HUDManager.Instance == null)
        {
            GameObject hudPrefab = Resources.Load<GameObject>("HUDManager");
            if (hudPrefab != null)
                Instantiate(hudPrefab);
            else
                Debug.LogError("HUDManager is null");
        }

        // StatManager
        if (StatManager.Instance == null)
        {
            GameObject statPrefab = Resources.Load<GameObject>("StatManager");
            if (statPrefab != null)
                Instantiate(statPrefab);
            else
                Debug.LogError("StatManager is null");
        }
    }
}
