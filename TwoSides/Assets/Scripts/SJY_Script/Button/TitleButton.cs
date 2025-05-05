using UnityEngine;

public class TitleButton : MonoBehaviour
{
    public void StartTitle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");

        HUDManager.Instance.InitHUD();
        HUDManager.Instance.HideHUD();
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
