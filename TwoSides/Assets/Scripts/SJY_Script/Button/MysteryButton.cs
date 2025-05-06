using UnityEngine;

public class MysteryButton : MonoBehaviour
{
    public GameObject map;
    private void Awake()
    {
        if (map == null)
            map = GameObject.Find("MapScrollArea"); // �̸� ��Ȯ�� ��ġ�ؾ� ��
    }
    public void Onclick()
    {
        string sceneName = "Mystery";
        int rand = Random.Range(0, 10);  // range �ٲٱ�
        sceneName += rand.ToString();
        map.SetActive(false);
        GameManager.Instance.isClear = false;
        Mapbutton.Instance.clearOn = false;
        LoadingSceneController.Instance.LoadScene(sceneName);
    }
}
