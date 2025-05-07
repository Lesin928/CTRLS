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

        GameManager.Instance.StartNewGame();

        HideMapController.shouldShowHideMap = true;

        if (Map.Instance != null)
            Map.Instance.ResetMap();
    }
}
