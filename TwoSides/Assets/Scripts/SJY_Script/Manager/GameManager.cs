using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using UnityEngine.Rendering;
using JetBrains.Annotations;
using UnityEngine.UI;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

// 게임 상태와 플레이어 상태 관리를 위한 스크립트트
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentStage = 1;
    public int maxStage = 15;

    public List<StageData> stageDataList;
    public StageData currentStageData;
    private int deadMonsterCount = 0;
    public bool isClear = false;
    public bool isTutoSkip = false;

    public GameObject playerPrefab;
    public PlayerObject playerObject;
    public GameObject go;

    public GameObject fadeCanvasPrefab;

    public GameObject hitEffectUIPrefab;
    private Image hitEffectImage;

    public bool isStoreReset;
    public Dictionary<StatType, int> itemPriceMap = new Dictionary<StatType, int>();

    private bool isClearFuncCheck = true;

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
    public float playerjumpForce;
    public float playerDashForce;

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
        currentStage = 1;

        GameObject hitEffect = Instantiate(hitEffectUIPrefab);
        hitEffectImage = hitEffect.GetComponentInChildren<Image>();
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

    //스테이지마다 다른 데이터 초기화
    public void InitStageData(string sceneName)
    {
        deadMonsterCount = 0;
        isClear = false;

        // 스테이지마다 정해진 몬스터 수를 가져옴
        currentStageData = stageDataList.Find(data => data.stageName == sceneName);

        if (currentStageData == null)
            Debug.LogWarning($"[GameManager] {sceneName}에 해당하는 StageData가 없습니다.");
        else
            Debug.Log($"[GameManager] {sceneName} 스테이지 데이터 초기화 완료 - 몬스터 수: {currentStageData.monsterCount}");

        // 게임 시작시 플레이어 생성
        if (currentStageData.stageName == "Tutorial")
        {
            GameObject spawnPoint = GameObject.Find("Starting_Point");

            go = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);

            playerObject = go.GetComponentInChildren<PlayerObject>();
            if (playerObject == null)
            {
                Debug.LogError("PlayerObject not found in PlayerSet hierarchy");
                return;
            }

            SetUpPlayerStats();
        }

        // 플레이어 스폰위치 지정정
        var playerObj = go.GetComponent<PlayerDontDestroyOnLoad>();
        if (playerObj != null)
            playerObj.ResetSpawnPosition();
    }

    public void StartTimeLine()
    {
        HUDManager.Instance.HideHUD();

        LoadingSceneController.Instance.LoadScene("TimeLine");
    }

    // GameStart, ReStart 할때마다 필요한 모든 정보 초기화화
    public void StartNewGame()
    {
        Debug.Log("Start New Game");

        currentStage = 1;
        isClear = false;
        isStoreReset = true;
        isClearFuncCheck = true;

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

        HideMapController.shouldShowHideMap = true;

        LoadingSceneController.Instance.LoadScene("Tutorial");
    }

    private void SetUpPlayerStats()
    {
        playerMaxHealth = 100;
        playerHealth = playerMaxHealth;
        playerAttack = 10f;
        playerArmor = 5f;
        playerAttackSpeed = 1f;
        playerCritical = 0.1f;
        playerCriticalDamage = 2f;
        playerMoveSpeed = 7f;
        playerjumpForce = 13f;
        playerDashForce = 15f;

        playerObject.MaxHp = playerMaxHealth;
        playerObject.CurrentHp = playerHealth;
        playerObject.Armor = playerArmor;
        playerObject.Attack = playerAttack;
        playerObject.AttackSpeed = playerAttackSpeed;
        playerObject.Critical = playerCritical;
        playerObject.CriticalDamage = playerCriticalDamage;
        playerObject.MoveSpeed = playerMoveSpeed;
        playerObject.JumpForce = playerjumpForce;
        playerObject.DashForce = playerDashForce;

        playerGold = 100;

        HUDManager.Instance.SetMaxHealth(playerMaxHealth);
        HUDManager.Instance.SetHealth(playerHealth);
        HUDManager.Instance.SetGold(playerGold);
    }

    public void OnStageClear()
    {
        isClear = true;
        currentStage++;

        if (Map.Instance.LEVEL == 16 && Map.Instance.doorConnected)
        {
            GameClear();
        }
    }

    //방에 있는 모든 몬스터를 처치해야지 넘어가게 하기 위한 함수
    //몬스터가 죽을때마다 호출되어 확인함함
    public void OnMonsterDead()
    {
        if (currentStageData == null) return;

        deadMonsterCount++;
        Debug.Log($"Dead Monster Count: {deadMonsterCount}");
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
        GameObject map = GameObject.Find("MapScrollArea");
        if (map != null)
        {
            if (map.activeSelf)
            {
                map.SetActive(false);
            }
        }
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(1f);

        GameObject fadeObj = Instantiate(fadeCanvasPrefab);
        FadeController fade = fadeObj.GetComponent<FadeController>();

        AudioManager.Instance.ChangeBGM("OverBGM");
        yield return StartCoroutine(fade.FadeOut());

        AsyncOperation op = SceneManager.LoadSceneAsync("GameOver");
        yield return new WaitUntil(() => op.isDone);
    }

    public void GameClear()
    {
        if (isClearFuncCheck)
        {
            isClearFuncCheck = false;
            HUDManager.Instance.HideHUD();
            HideMapController.shouldShowHideMap = false;
            StartCoroutine(GameClearRoutine());
        }
    }

    IEnumerator GameClearRoutine()
    {
        yield return new WaitForSeconds(1f);

        AudioManager.Instance.ChangeBGM("ClearBGM");

        AsyncOperation op = SceneManager.LoadSceneAsync("GameClear");
        yield return new WaitUntil(() => op.isDone);
    }

    // 새로운 게임이 시작될때마다 아이템 가격 초기화
    public int GetItemPrice(StatType type)
    {
        if (!itemPriceMap.ContainsKey(type))
            itemPriceMap[type] = 100; // 기본 가격

        return itemPriceMap[type];
    }

    // 아이템 구매시 가격 증가
    public void IncreaseItemPrice(StatType type, int amount)
    {
        if (!itemPriceMap.ContainsKey(type))
            itemPriceMap[type] = 100;

        itemPriceMap[type] += amount;
    }

    #region PlayerStat
    public void SetGold(int value)
    {
        playerGold += value;

        if (playerGold <= 0)
            playerGold = 0;
        HUDManager.Instance.SetGold(playerGold);
    }

    public void TakeDamage(float hp)
    {
        Debug.Log(hp);
        playerHealth = hp;

        HUDManager.Instance.SetHealth(playerHealth);

        StartCoroutine(HitEffectCoroutine());

        if (playerHealth <= 0)
        {
            GameOver();
        }
    }

    IEnumerator HitEffectCoroutine()
    {
        Debug.Log("HitEffectCoroutine Start");

        Color color = hitEffectImage.color;
        color.a = 0.5f;

        float duration = 0.3f;
        float timer = 0.0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0.5f, 0f, timer / duration);
            hitEffectImage.color = color;
            yield return null;
        }
    }

    public void SetHealth(float value)
    {
        playerHealth += value;
        playerObject.CurrentHp = playerHealth;

        if (playerHealth >= playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
            playerObject.CurrentHp = playerHealth;
        }

        HUDManager.Instance.SetHealth(playerHealth);

        if (playerHealth <= 0)
        {
            GameOver();
        }
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
        //키입력 차단단
        if (InputBlocker.blockKeyboardInput) return;

        if (Input.GetKeyDown(KeyCode.G))
        {
            SetGold(1000);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(isClear);
            OnStageClear();
            Debug.Log(isClear);
        }
    }
}
