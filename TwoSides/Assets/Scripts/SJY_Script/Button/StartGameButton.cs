using UnityEngine;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    public void OnClickStartGame()
    {
        GameManager.Instance.StartNewGame();


        if (Map.Instance == null)
        {
            Debug.LogWarning("Map instance is null. Cannot reset map.");
            return;
        }
        Map.Instance.ResetMap();
    }
}
