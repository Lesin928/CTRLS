using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    public Map mapScript;

    public void OnClickStartGame()
    {
        GameManager.Instance.StartNewGame();

        Map.Instance.ResetMap();

        //GameObject go = GameObject.Find("MAPCanvas");

        //Map component = go.GetComponentInChildren<Map>();

        ////mapScript = go.GetComponent<Map>();

        //if (component != null)
        //{
        //    component.ResetMap();
        //}
        //else
        //{
        //    Debug.LogError("Map script not found!");
        //}
    }
}
