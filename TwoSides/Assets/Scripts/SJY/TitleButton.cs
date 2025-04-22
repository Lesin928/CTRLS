using UnityEngine;

public class TitleButton : MonoBehaviour
{
    public void StartTitle()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }
}
