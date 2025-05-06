using UnityEngine;

public class StoreButton : MonoBehaviour
{
    public GameObject map;
    private void Awake()
    {
        if (map == null)
            map = GameObject.Find("MapScrollArea"); // 이름 정확히 일치해야 함
    }
    public void Onclick()
    {
        string sceneName = "Store";
        map.SetActive(false);
        GameManager.Instance.isClear = false;
        Mapbutton.Instance.clearOn = true;
        LoadingSceneController.Instance.LoadScene(sceneName);
    }
}
