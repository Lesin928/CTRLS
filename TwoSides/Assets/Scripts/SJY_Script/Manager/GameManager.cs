using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentStage = 1;
    public int maxStage = 3;

    public int playerGold;
    public float playerHealth;
    public float playerMaxHealth;
    public float playerArmor;
    public float playerAttack;
    public float playerAttackSpeed;
    public float playerMoveSpeed;
    public float playerCritical;
    public float playerCriticalDamage;

    //enum StageType
    //{
    //    BATTLE,
    //    EVENT,
    //    STORE,
    //    BOSS
    //};

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
        playerMaxHealth = 100;
        playerHealth = playerMaxHealth;
        playerGold = 0;
        currentStage = 1;

        if (SceneManager.GetActiveScene().name == "Title")
        {
            HUDManager.Instance.HideHUD();
        }
    }

    public static void Init()
    {
        if (Instance != null) return;

        Addressables.LoadAssetAsync<GameObject>("GameManager").Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject obj = Instantiate(handle.Result);
            }
            else
            {
                Debug.LogError("Failed to load GameManager");
            }
        };
    }

    public void StartNewGame()
    {
        Debug.Log("Start New Game");

        playerMaxHealth = 100;
        playerHealth = playerMaxHealth;
        playerGold = 0;
        currentStage = 1;

        //HUDManager
        if (HUDManager.Instance != null)
        {
            HUDManager.Instance.ShowHUD();
            HUDManager.Instance.InitHUD();

            HUDManager.Instance.StartTrackingTime();
        }

        //StatUIManager
        if (StatUIManager.Instance != null)
        {
            StatUIManager.Instance.InitStatWindow();
        }

        //EventScriptManager
        if (EventScriptManager.Instance != null)
        {
            Destroy(EventScriptManager.Instance.gameObject);
        }
        EventScriptManager.Init();

        AudioManager.Instance.ChangeBGM("IngameBGM");

        LoadStage(currentStage);
    }

    public void LoadStage(int stageIndex)
    {
        string sceneName = $"Stage_{stageIndex}";
        LoadingSceneController.Instance.LoadScene(sceneName);
    }

    public void OnStageClear()
    {
        // 여기서 Map 이 나와야함
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

    public void SetGold(int value)
    {
        playerGold += value;

        if (playerGold <= 0)
            playerGold = 0;
        HUDManager.Instance.SetGold(value);
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

    public void SetHealth(float value)
    {
        playerHealth += value;

        if (playerHealth >= playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
        }

        if (playerHealth <= 0)
        {
            GameOver();
        }

        HUDManager.Instance.SetHealth(playerHealth);
    }

    public void SetMaxHealth(float value)
    {
        playerMaxHealth += value;
        HUDManager.Instance.SetMaxHealth(playerMaxHealth);

        if (playerHealth >= playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
            HUDManager.Instance.SetHealth(playerHealth);
        }

        if (playerMaxHealth <= 0)
        {
            GameOver();
        }
    }

    public void SetPlayerArmor(float value)
    {
        playerArmor += value;
    }

    public void SetPlayerAttack(float value)
    {
        playerAttack += value;
    }

    public void SetPlayerAttackSpeed(float value)
    {
        playerAttackSpeed += value;
    }

    public void SetPlayerMoveSpeed(float value)
    {
        playerMoveSpeed += value;
    }

    public void SetPlayerCritical(float value)
    {
        playerCritical += value;
    }

    public void SetPlayerCriticalDamage(float value)
    {
        playerCriticalDamage += value;
    }

    void Update()
    {
        if (InputBlocker.blockKeyboardInput) return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetGold(10);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            SetHealth(10);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartNewGame();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            OnStageClear();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            SetMaxHealth(10);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            SetMaxHealth(-10);
        }
    }
}
