using UnityEngine;

public class BossButton : MonoBehaviour
{
    public void Onclick()
    {
        string sceneName = "Boss";
        int rand = Random.Range(0, 1);  // range �ٲٱ�
        sceneName += rand.ToString();

        LoadingSceneController.Instance.LoadScene(sceneName);
    }
}
