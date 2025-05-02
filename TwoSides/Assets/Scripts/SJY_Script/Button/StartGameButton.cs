using UnityEngine;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    public void OnClickStartGame()
    {
        Debug.Log("Start Game Button Clicked!");
        GameManager.Instance.StartNewGame();


        Map.Instance.ResetMap();
    }
}
