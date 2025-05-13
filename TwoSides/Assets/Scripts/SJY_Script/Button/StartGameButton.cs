using UnityEngine;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    public GameObject playerPrefab;

    public void OnClickStartGame()
    {
        //새 게임이 시작될때마다 기존 플레이어 삭제
        playerPrefab = GameObject.Find("PlayerSet(Clone)");
        if (playerPrefab != null)
        {
            Destroy(playerPrefab);
        }

        GameManager.Instance.StartTimeLine();
        HUDManager.Instance.ResumGame();

        if (Map.Instance != null)
        {
            Map.Instance.ResetMap();
            Map.Instance.battleNum = 0;
            Map.Instance.puzzleNum = 0;
        }
    }
}
