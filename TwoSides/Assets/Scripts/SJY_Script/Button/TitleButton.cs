using UnityEngine;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{
    public GameObject playerPrefab;

    public void StartTitle()
    {
        LoadingSceneController.Instance.LoadScene("Title");

        playerPrefab = GameObject.Find("PlayerSet(Clone)");

        if (playerPrefab != null)
        {
            Destroy(playerPrefab);
        }

        if (HUDManager.Instance != null)
        {
            Debug.Log(HUDManager.Instance.gameObject.activeSelf + "1");
        }

        if (HUDManager.Instance != null)
        {
            Debug.Log(HUDManager.Instance.gameObject.activeSelf + "2");
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
