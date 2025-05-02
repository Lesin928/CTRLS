using UnityEngine;

public class BattleButton : MonoBehaviour
{
    public GameObject map; // 연결 안 해도 됨

    private void Awake()
    {
        if (map == null)
            map = GameObject.Find("MapScrollArea"); // 이름 정확히 일치해야 함
    }
    public void Onclick()
    {
        string sceneName = "Battle";
        int rand = Random.Range(0, 10);
        sceneName += rand.ToString();
        map.SetActive(false);
        LoadingSceneController.Instance.LoadScene(sceneName);
    }
}
