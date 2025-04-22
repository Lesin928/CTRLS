using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentStage = 1;
    public int maxStage = 3;
    public int playerGold;
    public float playerHealth;
    public float maxHealth;
    public float playerAttack;

    //public int stageType;

    enum StageType
    {
        BATTLE,
        EVENT,
        STORE,
        BOSS
    };

    private void Awake()
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
        if (SceneManager.GetActiveScene().name == "Title")
        {
            HUDManager.Instance.HideHUD();
        }
    }

    public void StartNewGame()
    {
        Debug.Log("Start New Game");

        maxHealth = 100;
        playerHealth = maxHealth;
        playerGold = 0;
        currentStage = 1;

        //HUDManager
        if (HUDManager.Instance != null)
        {

            HUDManager.Instance.ShowHUD();
            HUDManager.Instance.InitHUD();


            HUDManager.Instance.StartTrackingTime();

        }

        //StatManager
        if (StatManager.Instance != null)
        {
            StatManager.Instance.InitStatWindow();
        }

        //EventScriptManager
        if (EventScriptManager.Instance != null)
        {
            Destroy(EventScriptManager.Instance.gameObject);
        }

        GameObject prefab = Resources.Load<GameObject>("EventScriptManager");
        if (prefab != null)
        {
            Instantiate(prefab);
        }
        else
        {
            Debug.LogError("EventScriptManager 프리팹 확인");
        }

        LoadStage(currentStage);
    }

    public void LoadStage(int stageIndex)
    {
        string sceneName = $"Stage_{stageIndex}";
        LoadingSceneController.Instance.LoadScene(sceneName);
    }

    public void OnStageClear()
    {
        /*
        스테이지를 클리어하면 맵이 열리고,
        맵의 아이콘을 클릭하면 아이콘의 StageType을 받아서
        LoadStage(cuurentStage, stageType); 으로 바꾸기
         */

        if (currentStage < maxStage)
        {
            currentStage++;
            LoadStage(currentStage);
        }
        else
        {
            GameClear();
        }
    }

    public void GameOver()
    {
        HUDManager.Instance.HideHUD();
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("GameOver");
        yield return new WaitUntil(() => op.isDone);
    }

    public void GameClear()
    {
        HUDManager.Instance.HideHUD();
        StartCoroutine(GameClearRoutine());
    }

    IEnumerator GameClearRoutine()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("GameClear");
        yield return new WaitUntil(() => op.isDone);
    }

    public void AddGold(int amount)
    {
        playerGold += amount;
        HUDManager.Instance.AddGold(amount);
    }

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
        HUDManager.Instance.SetHealth(playerHealth);

        if (playerHealth <= 0)
        {
            GameOver();
        }
    }

    public void AddHealth(float amount)
    {
        playerHealth += amount;

        if (playerHealth >= maxHealth)
        {
            playerHealth = maxHealth;
        }

        HUDManager.Instance.SetHealth(playerHealth);
    }

    public void AddMaxHealth(float amount)
    {
        maxHealth += amount;
        HUDManager.Instance.SetMaxHealth(maxHealth);
    }

    public void MinusMaxHealth(float amount)
    {
        maxHealth -= amount;
        HUDManager.Instance.SetMaxHealth(maxHealth);

        if (playerHealth >= maxHealth)
        {
            playerHealth = maxHealth;
        }

        if (maxHealth <= 0)
        {
            GameOver();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddGold(10);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            AddHealth(10);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("CurrentStage : Stage_" + currentStage
                        + "\nMaxStage : " + maxStage);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            OnStageClear();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            AddMaxHealth(10);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            MinusMaxHealth(10);
        }

        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    SceneManager.LoadScene("GameClear");
        //}
    }
}
