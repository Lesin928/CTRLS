using UnityEngine;

public class MysteryButton : MonoBehaviour
{
    public void Onclick()
    {
        string sceneName = "Mystery";
        int rand = Random.Range(0, 1);  // range �ٲٱ�
        sceneName += rand.ToString();

        LoadingSceneController.Instance.LoadScene(sceneName);
    }
}
