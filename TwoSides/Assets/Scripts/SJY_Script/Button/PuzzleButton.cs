using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    public void Onclick()
    {
        string sceneName = "Puzzle";
        int rand = Random.Range(0, 1);  // range �ٲٱ�
        sceneName += rand.ToString();

        LoadingSceneController.Instance.LoadScene(sceneName);
    }
}
