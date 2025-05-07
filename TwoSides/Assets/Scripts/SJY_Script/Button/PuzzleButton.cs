using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    public GameObject map;
    private bool[] isUsed = new bool[3];
    private void Awake()
    {
        if (map == null)
            map = GameObject.Find("MapScrollArea"); // 이름 정확히 일치해야 함
    }
    public void Onclick()
    {
        string sceneName = "Puzzle";

        int rand = Random.Range(0, 3);
        while (isUsed[rand])
        {
            rand = Random.Range(0, 3);
        }
        isUsed[rand] = true;

        sceneName += rand.ToString();
        map.SetActive(false);
        GameManager.Instance.isClear = false;
        Mapbutton.Instance.clearOn = false;
        LoadingSceneController.Instance.LoadScene(sceneName);
    }
}
