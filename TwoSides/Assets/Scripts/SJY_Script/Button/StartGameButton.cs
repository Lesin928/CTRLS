using UnityEngine;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    public GameObject playerPrefab;

    public void OnClickStartGame()
    {
        playerPrefab = GameObject.Find("PlayerSet(Clone)");
        if (playerPrefab != null)
        {
            Destroy(playerPrefab);
        }

        //if (GameManager.Instance.playerPrefab != null)
        //{
        //    Destroy(GameManager.Instance.playerPrefab);
        //    Debug.Log("Destroy Prefab");
        //}

        GameManager.Instance.StartNewGame();

        HideMapController.shouldShowHideMap = true;
        if (Map.Instance == null)
        {
            Debug.LogWarning("Map instance is null. Cannot reset map.");
            return;
        }
        Map.Instance.ResetMap();
    }
}
