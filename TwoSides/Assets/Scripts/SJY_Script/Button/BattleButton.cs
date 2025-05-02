using UnityEngine;

public class BattleButton : MonoBehaviour
{
    public GameObject map; // ���� �� �ص� ��

    private void Awake()
    {
        if (map == null)
            map = GameObject.Find("MapScrollArea"); // �̸� ��Ȯ�� ��ġ�ؾ� ��
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
