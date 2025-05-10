using UnityEngine;

public class TitleButton : MonoBehaviour
{
    public GameObject map;
    public GameObject playerPrefab;

    public void StartTitle()
    {
        map = GameObject.Find("MapScrollArea");

        if (map.activeSelf)
        {
            map.SetActive(false);
        }

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
        GameObject Mapbtn = GameObject.Find("HideMap");
        GameObject go = GameObject.Find("OptionUI(Clone)");
        if (go != null)
        {
            if (go.activeSelf)
            {
                go.SetActive(false);
                Mapbtn.SetActive(false);
            }
        }
    }
}
