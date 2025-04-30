using UnityEngine;

public class BattleButton : MonoBehaviour
{
    public void Onclick()
    {
        string sceneName = "Battle";
        int rand = Random.Range(0, 1);
        sceneName += rand.ToString();

        LoadingSceneController.Instance.LoadScene(sceneName);
    }
}
