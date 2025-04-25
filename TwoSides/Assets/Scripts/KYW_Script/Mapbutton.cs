using UnityEngine;
using UnityEngine.SceneManagement;

public class Mapbutton : MonoBehaviour
{
    private bool setting = false;
    public GameObject map;

    public void Scene1()
    {
        SceneManager.LoadScene("KYW_MAP");
    }
    public void Scene2()
    {
        SceneManager.LoadScene("babo");
    }
    public void mapsetting()
    {
        map.SetActive(setting);
        setting = !setting;
    }
}
