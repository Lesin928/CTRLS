using UnityEngine;

public class MysteryButton : MonoBehaviour
{
    public void Onclick()
    {
        string sceneName = "Mystery";
        int rand = Random.Range(0, 1);  // range ¹Ù²Ù±â
        sceneName += rand.ToString();

        LoadingSceneController.Instance.LoadScene(sceneName);
    }
}
