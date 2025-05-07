using UnityEngine;

public class MysteryButton : MonoBehaviour
{
    public GameObject map;
    private bool[] isUsed = new bool[10];

    private void Awake()
    {
        if (map == null)
            map = GameObject.Find("MapScrollArea"); // 이름 정확히 일치해야 함

        isUsed[0] = true;
    }
    public void Onclick()
    {
        string sceneName = "Mystery";

        int rand = Random.Range(0, 10);
        while (isUsed[rand])
        {
            rand = Random.Range(0, 10);
        }
        isUsed[rand] = true;

        sceneName += rand.ToString();
        map.SetActive(false);
        GameManager.Instance.isClear = false;
        Mapbutton.Instance.clearOn = false;
        //Map.Instance.doorConnected = false;
        LoadingSceneController.Instance.LoadScene(sceneName);
    }
}
