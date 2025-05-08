using UnityEngine;

public class BattleButton : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject map;
    private bool[] isUsed = new bool[10];
    private void Awake()
    {
        if (map == null)
            map = GameObject.Find("MapScrollArea"); // 이름 정확히 일치해야 함
    }

    public void Onclick()
    {
        string sceneName = "Battle";

        //int rand = Random.Range(0, 10);
        //while (isUsed[rand])
        //{
        //    rand = Random.Range(0, 10);
        //}
        //isUsed[rand] = true;

        //int rand = 0;
        //while (isUsed[rand])
        //{
        //    rand++;
        //}
        //isUsed[rand] = true;


        sceneName += Map.Instance.battleNum.ToString();
        if (Map.Instance.battleNum < 9)
            Map.Instance.battleNum++;

        GameManager.Instance.isClear = false;
        Mapbutton.Instance.activeButton = false;
        Map.Instance.doorConnected = false;
        map.SetActive(false);
        LoadingSceneController.Instance.LoadScene(sceneName);
    }
}
