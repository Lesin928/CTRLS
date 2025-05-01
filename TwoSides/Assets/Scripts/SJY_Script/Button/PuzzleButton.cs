using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    public GameObject map;
    private void Awake()
    {
        if (map == null)
            map = GameObject.Find("MapScrollArea"); // �̸� ��Ȯ�� ��ġ�ؾ� ��
    }
    public void Onclick()
    {
        string sceneName = "Puzzle";
        int rand = Random.Range(0, 3);  // range �ٲٱ�
        sceneName += rand.ToString();
        map.SetActive(false);
        LoadingSceneController.Instance.LoadScene(sceneName);
    }
}
