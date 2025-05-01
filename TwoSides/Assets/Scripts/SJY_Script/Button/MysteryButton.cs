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
        int rand = Random.Range(0, 1);  // range �ٲٱ�
        sceneName += rand.ToString();
        map.SetActive(false);
        LoadingSceneController.Instance.LoadScene(sceneName);
    }
}
