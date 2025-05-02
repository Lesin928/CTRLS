using UnityEngine;

public class BossButton : MonoBehaviour
{
    public GameObject map;
    private void Awake()
    {
        if (map == null)
            map = GameObject.Find("MapScrollArea"); // �̸� ��Ȯ�� ��ġ�ؾ� ��
    }
    public void Onclick()
    {
        string sceneName = "Boss";
        map.SetActive(false);
        LoadingSceneController.Instance.LoadScene(sceneName);
    }
}
