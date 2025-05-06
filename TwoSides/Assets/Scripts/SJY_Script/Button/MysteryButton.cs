using UnityEngine;

public class MysteryButton : MonoBehaviour
{
    public GameObject map;
    private void Awake()
    {
        if (map == null)
            map = GameObject.Find("MapScrollArea"); // 이름 정확히 일치해야 함
    }
    public void Onclick()
    {
        string sceneName = "Mystery";
        int rand = Random.Range(0, 10);  // range 바꾸기
        //int rand = 3      test용
        sceneName += rand.ToString();
        map.SetActive(false);
        GameManager.Instance.isClear = false;
        Mapbutton.Instance.clearOn = true;
        LoadingSceneController.Instance.LoadScene(sceneName);
    }
}
