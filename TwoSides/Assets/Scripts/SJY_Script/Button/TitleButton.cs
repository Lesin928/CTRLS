using UnityEngine;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{
    public GameObject playerPrefab;

    public void StartTitle()
    {
        LoadingSceneController.Instance.LoadScene("Title");

        // 타이틀 화면으로 돌아갈때마다 기존 플레이어 삭제
        playerPrefab = GameObject.Find("PlayerSet(Clone)");
        if (playerPrefab != null)
        {
            Destroy(playerPrefab);
        }

        HideMapController.shouldShowHideMap = false;
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ChangeBGM("TitleBGM");
        }

        GameObject go = GameObject.Find("OptionUI(Clone)");
        GameObject Mapbtn = GameObject.Find("HideMap");
        GameObject map = GameObject.Find("MapScrollArea");
        if (go != null)
        {
            if (go.activeSelf)
            {
                go.SetActive(false);
                Mapbtn.SetActive(false);
                map.SetActive(false);
            }
        }
    }
}
