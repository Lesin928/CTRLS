using UnityEngine;

public class FullScreenButton : MonoBehaviour
{
    public void OnClickFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
