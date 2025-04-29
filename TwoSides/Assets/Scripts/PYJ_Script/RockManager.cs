using UnityEngine;
using UnityEngine.UI;

public class GameManager1 : MonoBehaviour
{
    // 싱글턴 패턴
    public static GameManager1 Instance;

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

    // 게임 시작 버튼을 눌렀을 때 호출되는 함수
    public void OnStartButtonPressed()
    {
        GameManager1.Instance.StartGame(); // 게임 시작
    }

    // 게임이 시작되었는지 확인하는 함수
    public bool IsGameStarted()
    {
        return gameStarted;
    }

    // 싱글턴 인스턴스를 설정하는 함수
    void Awake()
    {
        Instance = this;
    }

    // 게임 초기화 및 UI 설정
    void Start()
    {
        timer = survivalTime; // 타이머 설정 (10초로 설정)
        UpdateHPText(); // 체력 텍스트 업데이트
        HideHearts(); // 하트 UI 초기화 (안 보이게 설정)

        gameOverPanel.SetActive(false); // 게임 오버 패널 숨기기
        gameClearPanel.SetActive(false); // 게임 클리어 패널 숨기기
    }

    // 하트 UI 숨기는 함수
    void HideHearts()
    {
        foreach (var heart in hearts)
        {
            heart.gameObject.SetActive(false); // 모든 하트 UI 비활성화
        }
    }

    // 게임을 시작하는 함수
    public void StartGame()
    {
        gameStarted = true; // 게임 시작 상태로 변경
        gameOverPanel.SetActive(false); // 게임 오버 패널 숨기기
        gameClearPanel.SetActive(false); // 게임 클리어 패널 숨기기
        Time.timeScale = 1;  // 게임이 멈추지 않도록 설정
    }

    // 바위 떨어뜨리는 시작을 외부에서 호출할 수 있도록 분리한 함수
    public void StartSpawning()
    {
        if (gameStarted) // 게임이 시작되었다면
        {
            ShowHearts(); // 하트 UI 켜기
            InvokeRepeating(nameof(SpawnRock), 1f, spawnInterval); // 바위 생성 시작
        }
    }

    // 하트 UI를 활성화하는 함수
    void ShowHearts()
    {
        foreach (var heart in hearts)
        {
            heart.gameObject.SetActive(true); // 하트 UI 활성화
        }
    }

    // 매 프레임마다 호출되는 Update 함수
    void Update()
    {
        // 게임 오버나 게임 클리어 상태에서 엔터 입력 체크
        if ((gameOverPanel.activeSelf || gameClearPanel.activeSelf))
        {
            if (Input.GetKeyDown(KeyCode.Return)) // 엔터 키 입력 시
            {
                if (gameOverPanel.activeSelf)
                    gameOverPanel.SetActive(false);
                if (gameClearPanel.activeSelf)
                    gameClearPanel.SetActive(false);

                CancelInvoke(nameof(SpawnRock));
                gameStarted = false;
            }
            return;
        }

        if (!gameStarted)
            return;

        // 타이머를 줄여가며 게임 시간 흐름
        timer -= Time.deltaTime;

        // === 게임 클리어 조건 추가 ===
        if (timer <= 0f)
        {
            timer = 0f;
            gameClearPanel.SetActive(true);   // 게임 클리어 UI 표시
            CancelInvoke(nameof(SpawnRock));  // 바위 떨어지는 것 중지
            gameStarted = false;              // 게임 종료
        }
    }


    // 바위를 떨어뜨리는 함수
    void SpawnRock()
    {
        float xPos = Random.Range(spawnXMin, spawnXMax); // X축 위치 랜덤
        Vector3 spawnPos = new Vector3(xPos, spawnHeight, 0); // 바위 생성 위치
        Instantiate(rockPrefab, spawnPos, Quaternion.identity); // 바위 생성
    }

    // 하트 UI 업데이트 함수
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

    // 플레이어가 피해를 입을 때 호출되는 함수
    public void TakeDamage()
    {
        Debug.Log("바위 충돌!"); // 디버그 메시지
        hp--; // 체력 감소
        UpdateHPText(); // 체력 텍스트 업데이트
        UpdateHearts(); // 하트 UI 업데이트

        if (hp <= 0)
        {
            gameOverPanel.SetActive(true); // 게임 오버 패널 표시
        }
    }

    // 체력 텍스트 업데이트 함수
    void UpdateHPText()
    {
        hpText.text = $"♥: {hp}"; // 체력 텍스트 업데이트
    }
}
