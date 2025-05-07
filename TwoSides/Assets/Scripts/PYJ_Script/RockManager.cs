using UnityEngine;
using UnityEngine.UI;

public class RockManager : MonoBehaviour
{
    // 싱글턴 패턴
    public static RockManager Instance;

    // 바위 관련 변수들
    public GameObject rockPrefab; // 바위 프리팹
    public float spawnInterval = 1.0f; // 바위 생성 간격

    [Header("Spawn Settings")]
    public float spawnHeight = 5f; // 떨어지는 바위의 Y 위치
    public float spawnXMin = -3f;  // 바위 생성될 X 최소값
    public float spawnXMax = 3f;   // 바위 생성될 X 최대값

    [Header("Player Settings")]
    public int hp = 3; // 플레이어의 체력
    public Text hpText; // 체력 표시용 텍스트 UI

    [Header("UI Panels")]
    public GameObject gameOverPanel; // 게임 오버 패널
    public GameObject gameClearPanel; // 게임 클리어 패널

    [Header("Game Settings")]
    public float survivalTime = 10f; // 게임 목표 시간 (클리어 조건을 10초로 설정)

    private float timer; // 타이머
    private bool gameStarted = false; // 게임 시작 여부

    [Header("Heart Images")]
    public Image[] hearts; // 체력을 나타내는 하트 UI 이미지 배열




    /// <summary>
    /// 게임 시작 버튼을 눌렀을 때 호출되는 함수 
    /// </summary>
    public void OnStartButtonPressed()
    {
        RockManager.Instance.StartGame(); // 게임 시작
    }


    /// <summary>
    /// 게임이 시작되었는지 확인하는 함수
    /// </summary>
    public bool IsGameStarted()
    {
        return gameStarted;
    }




    /// <summary>
    /// 싱글턴 인스턴스를 설정하는 함수
    /// </summary>
    void Awake()
    {
        Instance = this;
    }




    /// <summary>
    /// 게임 초기화 및 UI 설정
    /// </summary>
    void Start()
    {
        timer = survivalTime; // 타이머 설정 (10초로 설정)
        UpdateHPText(); // 체력 텍스트 업데이트
        HideHearts(); // 하트 UI 초기화 (안 보이게 설정)

        gameOverPanel.SetActive(false); // 게임 오버 패널 숨기기
        gameClearPanel.SetActive(false); // 게임 클리어 패널 숨기기
    }



    /// <summary>
    /// 하트 UI 숨기는 함수
    /// </summary>

    void HideHearts()
    {
        foreach (var heart in hearts)
        {
            heart.gameObject.SetActive(false); // 모든 하트 UI 비활성화
        }
    }



    /// <summary>
    /// 게임을 시작하는 함수 
    /// </summary>

    public void StartGame()
    {
        gameStarted = true;
        gameOverPanel.SetActive(false);
        gameClearPanel.SetActive(false);
        Time.timeScale = 1;

        StartSpawning(); // 이 줄 추가!
    }




    /// <summary>
    /// 바위 떨어뜨리는 시작을 외부에서 호출할 수 있도록 분리한 함수 
    /// </summary>
    public void StartSpawning()
    {
        if (gameStarted)
        {
            ShowHearts();
            InvokeRepeating(nameof(SpawnRock), 1f, spawnInterval);

            // survivalTime 후에 StopSpawning 호출 예약
            Invoke(nameof(StopSpawning), survivalTime);
        }
    }
    void StopSpawning()
    {
        CancelInvoke(nameof(SpawnRock));

        // 게임 오버 상태면 클리어 처리하지 않음
        if (hp <= 0)
        {
            Debug.Log("게임 오버 상태이므로 클리어 처리 안 함");
            return;
        }

        gameClearPanel.SetActive(true); // 클리어 UI 표시
        Debug.Log("게임 클리어 - 돌 스폰 중지");
    }



    /// <summary>
    /// 하트 UI를 활성화하는 함수 
    /// </summary>
    void ShowHearts()
    {
        foreach (var heart in hearts)
        {
            heart.gameObject.SetActive(true); // 하트 UI 활성화
        }
    }



    /// <summary>
    /// 매 프레임마다 호출되는 Update 함수 
    /// </summary>
    private bool clearPanelClosing = false;

    void Update()
    {
        if (gameOverPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                gameOverPanel.SetActive(false);
                CancelInvoke(nameof(SpawnRock));
                gameStarted = false;
            }
            return;
        }

        if (gameClearPanel.activeSelf && !clearPanelClosing)
        {
            clearPanelClosing = true; // 중복 실행 방지
            Invoke(nameof(CloseClearPanel), 2f); // 2초 후 자동 종료
            return;
        }
    }

    void CloseClearPanel()
    {
        gameClearPanel.SetActive(false);
        CancelInvoke(nameof(SpawnRock));
        gameStarted = false;
        clearPanelClosing = false; // 다시 초기화
    }




    /// <summary>
    /// 바위를 떨어뜨리는 함수
    /// </summary>
    void SpawnRock()
    {
        float xPos = Random.Range(spawnXMin, spawnXMax); // X축 위치 랜덤
        Vector3 spawnPos = new Vector3(xPos, spawnHeight, 0); // 바위 생성 위치
        Instantiate(rockPrefab, spawnPos, Quaternion.identity); // 바위 생성
    }



    /// <summary>
    /// 하트 UI 업데이트 함수
    /// </summary>
    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < hp)
                hearts[i].enabled = true;  // 하트 UI 켬
            else
                hearts[i].enabled = false; // 하트 UI 끔
        }
    }


    /// <summary>
    /// 플레이어가 피해를 입을 때 호출되는 함수
    /// </summary>
    public void TakeDamage2()
    {
        Debug.Log("바위 충돌!");
        hp--;
        UpdateHPText();
        UpdateHearts();

        if (hp <= 0)
        {
            gameOverPanel.SetActive(true);

            // 클리어 방지를 위해 예약된 StopSpawning 취소
            CancelInvoke(nameof(StopSpawning));

            // 바위 생성도 멈춤
            CancelInvoke(nameof(SpawnRock));
        }
    }





    /// <summary>
    /// 체력 텍스트 업데이트 함수
    /// </summary>
    void UpdateHPText()
    {
        hpText.text = $"♥: {hp}"; // 체력 텍스트 업데이트
    }
}
