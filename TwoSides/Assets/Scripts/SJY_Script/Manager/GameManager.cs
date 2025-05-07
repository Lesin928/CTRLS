using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentStage = 1;
    public int maxStage = 15;

    public List<StageData> stageDataList;
    public StageData currentStageData;
    private int deadMonsterCount = 0;
    public bool isClear = false;

    public PlayerObject playerObject;

    #region PlayerStat
    [Header("PlayerStat")]
    public int playerGold;
    public float playerHealth;
    public float playerMaxHealth;
    public float playerArmor;
    public float playerAttack;
    public float playerAttackSpeed;
    public float playerMoveSpeed;
    public float playerCritical;
    public float playerCriticalDamage;
    #endregion

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
            HUDManager.Instance.HideHUD();
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

    public void InitStageData(string sceneName)
    {
        deadMonsterCount = 0;
        isClear = false;

        currentStageData = stageDataList.Find(data => data.stageName == sceneName);

        if (currentStageData == null)
            Debug.LogWarning($"[GameManager] {sceneName}에 해당하는 StageData가 없습니다.");
        else
            Debug.Log($"[GameManager] {sceneName} 스테이지 데이터 초기화 완료 - 몬스터 수: {currentStageData.monsterCount}");

        if (currentStageData.stageName.Contains("Mystery")
            || currentStageData.stageName.Contains("Puzzle")
            || currentStageData.stageName.Contains("Store"))
        {
            OnStageClear();
        }

        if (currentStageData.stageName == "Battle0")
        {
            GameObject playerSet = GameObject.Find("PlayerSet");
            if (playerSet == null)
            {
                Debug.LogError("PlayerSet not found in scene");
                return;
            }

            playerObject = playerSet.GetComponentInChildren<PlayerObject>();
            if (playerObject == null)
            {
                Debug.LogError("PlayerObject not found in PlayerSet hierarchy");
                return;
            }
        }
    }

    public void StartNewGame()
    {
        Debug.Log("Start New Game");
        SetUpPlayerStats();
        currentStage = 1;
        isClear = false;

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

        //LoadStage(currentStage);
        LoadingSceneController.Instance.LoadScene("Battle0");


    }
    private void SetUpPlayerStats()
    {
        playerMaxHealth = 100;
        playerHealth = playerMaxHealth;
        playerAttack = 5f;
        playerArmor = 3f;
        playerAttackSpeed = 1f;
        playerCritical = 0.1f;
        playerCriticalDamage = 2f;

        playerObject.MaxHp = playerMaxHealth;
        playerObject.CurrentHp = playerHealth;
        playerObject.Armor = playerArmor;
        playerObject.Attack = playerAttack;
        playerObject.AttackSpeed = playerAttackSpeed;
        playerObject.Critical = playerCritical;
        playerObject.CriticalDamage = playerCriticalDamage;

        //추후 MoveSpeed 세팅

        playerGold = 0;
    }

    public void OnStageClear()
    {
        if (isClear)
            isClear = !isClear;
        else
        {
            isClear = true;
            currentStage++;

            if (currentStage > maxStage)
            {
                GameClear();
            }
        }
    }

    public void OnMonsterDead()
    {
        if (currentStageData == null) return;

        deadMonsterCount++;
        if (deadMonsterCount >= currentStageData.monsterCount)
        {
            OnStageClear();
            Debug.Log("스테이지 클리어");
        }
    }

    public void GameOver()
    {
        HUDManager.Instance.HideHUD();
        HideMapController.shouldShowHideMap = false;
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
        HideMapController.shouldShowHideMap = false;
        StartCoroutine(GameClearRoutine());
    }

    IEnumerator GameClearRoutine()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("GameClear");
        yield return new WaitUntil(() => op.isDone);
    }

    #region PlayerStat
    public void SetGold(int value)
    {
        playerGold += value;

        if (playerGold <= 0)
            playerGold = 0;
        HUDManager.Instance.SetGold(value);
    }

    public void TakeDamage()
    {
        playerHealth = playerObject.CurrentHp;
        HUDManager.Instance.SetHealth(playerHealth);

        if (playerHealth <= 0)
        {
            GameOver();
        }
    }

    public void SetHealth(float value)
    {
        playerHealth += value;
        playerObject.CurrentHp = playerHealth;

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
        playerObject.MaxHp = playerMaxHealth;
        HUDManager.Instance.SetMaxHealth(playerMaxHealth);

        if (playerHealth >= playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
            playerObject.CurrentHp = playerHealth;
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
        playerObject.Armor = playerArmor;
    }

    public void SetPlayerAttack(float value)
    {
        playerAttack += value;
        playerObject.Attack = playerAttack;
    }

    public void SetPlayerAttackSpeed(float value)
    {
        playerAttackSpeed += value;
        playerObject.AttackSpeed = playerAttackSpeed;
    }

    public void SetPlayerMoveSpeed(float value)
    {
        playerMoveSpeed += value;
        playerObject.MoveSpeed = playerMoveSpeed;
    }

    public void SetPlayerCritical(float value)
    {
        playerCritical += value;
        playerObject.Critical = playerCritical;
    }

    public void SetPlayerCriticalDamage(float value)
    {
        playerCriticalDamage += value;
        playerObject.CriticalDamage = playerCriticalDamage;
    }
    #endregion

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

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartNewGame();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(isClear);
            OnStageClear();
            Debug.Log(isClear);
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
