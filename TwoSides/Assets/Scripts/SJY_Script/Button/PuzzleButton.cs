using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    public void Onclick()
    {
        string sceneName = "Puzzle";
        int rand = Random.Range(0, 1);  // range ¹Ù²Ù±â
        sceneName += rand.ToString();

        LoadingSceneController.Instance.LoadScene(sceneName);
    }
}
