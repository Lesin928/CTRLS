using UnityEngine;
using UnityEngine.SceneManagement;

public class InputBlockerManager : MonoBehaviour
{
    private static InputBlockerManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SetKeyboardBlock(SceneManager.GetActiveScene().name);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetKeyboardBlock(scene.name);
    }

    void SetKeyboardBlock(string sceneName)
    {
        if (sceneName == "Title" || sceneName == "GameOver" || sceneName == "GameClear")
        {
            InputBlocker.blockKeyboardInput = true;
        }
        else
        {
            InputBlocker.blockKeyboardInput = false;
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}