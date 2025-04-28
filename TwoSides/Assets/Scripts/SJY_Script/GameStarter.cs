using UnityEngine;

public class GameStarter : MonoBehaviour
{
    private static bool initialized = false;

    private void Awake()
    {
        if (initialized) return;
        initialized = true;

        GameManager.Init();
        HUDManager.Init();
        StatUIManager.Init();
        OptionUIManager.Init();
    }
}