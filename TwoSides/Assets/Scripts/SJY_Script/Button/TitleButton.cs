using UnityEngine;

public class TitleButton : MonoBehaviour
{
    public void StartTitle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ChangeBGM("TitleBGM");
        }
    }
}
