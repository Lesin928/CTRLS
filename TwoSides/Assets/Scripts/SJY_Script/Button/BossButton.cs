using UnityEngine;

public class BossButton : MonoBehaviour
{
    public GameObject map;
    private void Awake()
    {
        if (map == null)
            map = GameObject.Find("MapScrollArea"); // 이름 정확히 일치해야 함
    }
    public void Onclick()
    {
        string sceneName = "Boss";
        map.SetActive(false);
        GameManager.Instance.isClear = false;
        Mapbutton.Instance.activeButton = false;
        Map.Instance.doorConnected = false;
        LoadingSceneController.Instance.LoadScene(sceneName);
    }
}