using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    public Map mapScript;

    public void OnClickStartGame()
    {
        GameManager.Instance.StartNewGame();

        Map.Instance.ResetMap();
    }
}
