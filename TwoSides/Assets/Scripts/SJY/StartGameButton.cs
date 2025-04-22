using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    public void OnClickStartGame()
    {
        GameManager.Instance.StartNewGame();
    }
}
