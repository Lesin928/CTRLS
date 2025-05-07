using UnityEngine;

public class BattleButton : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject map;
    private bool[] isUsed = new bool[10];
    private void Awake()
    {
        if (map == null)
            map = GameObject.Find("MapScrollArea"); // �̸� ��Ȯ�� ��ġ�ؾ� ��

        isUsed[0] = true;
    }

    public void Onclick()
    {
        string sceneName = "Battle";

        int rand = Random.Range(0, 10);
        while (isUsed[rand])
        {
            rand = Random.Range(0, 10);
        }
        isUsed[rand] = true;

        sceneName += rand.ToString();
        GameManager.Instance.isClear = false;
        Mapbutton.Instance.clearOn = false;
        map.SetActive(false);
        LoadingSceneController.Instance.LoadScene(sceneName);
    }
}
