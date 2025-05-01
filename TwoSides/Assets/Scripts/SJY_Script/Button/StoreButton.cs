using UnityEngine;

public class StoreButton : MonoBehaviour
{
    public void Onclick()
    {
        string sceneName = "Store";
        //int rand = Random.Range(0, 1);  // range ¹Ù²Ù±â
        //sceneName += rand.ToString();

        LoadingSceneController.Instance.LoadScene(sceneName);
    }
}
