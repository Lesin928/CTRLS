using UnityEngine;
using UnityEngine.UI;

public class GameManager1 : MonoBehaviour
{
    public static GameManager1 Instance;

    public GameObject rockPrefab;
    public float spawnInterval = 1.0f;

    [Header("Spawn Settings")]
    public float spawnHeight = 5f; // 떨어지는 높이 (Y축)
    public float spawnXMin = -3f;  // X 최소값
    public float spawnXMax = 3f;   // X 최대값

    [Header("Player Settings")]
    public int hp = 3;
    public Text hpText;

    [Header("UI Panels")]
    public GameObject gameOverPanel;
    public GameObject gameClearPanel;

    [Header("Game Settings")]
    public float survivalTime = 20f;

    private float timer;
    private bool gameStarted = false; // 게임 시작 여부


    public void OnStartButtonPressed()
    {
        GameManager1.Instance.StartGame();
    }

    public bool IsGameStarted()
    {
        return gameStarted;
    }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        timer = survivalTime;
        UpdateHPText();
    }

    // 게임 시작 버튼을 눌렀을 때 호출되는 함수
    public void StartGame()
    {
        gameStarted = true; // 게임 시작
        gameOverPanel.SetActive(false);
        gameClearPanel.SetActive(false);
        Time.timeScale = 1;  // 게임이 멈추지 않도록
    }

    // 바위 떨어뜨리는 시작을 외부에서 호출할 수 있도록 따로 분리
    public void StartSpawning()
    {
        if (gameStarted)  // 게임이 시작된 경우에만 바위가 떨어지도록
        {
            InvokeRepeating(nameof(SpawnRock), 1f, spawnInterval);
        }
    }

    void Update()
    {
        if (!gameStarted) return;  // 게임이 시작되지 않으면 아무 일도 일어나지 않게

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            CancelInvoke(nameof(SpawnRock));
            gameClearPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void SpawnRock()
    {
        float xPos = Random.Range(spawnXMin, spawnXMax);
        Vector3 spawnPos = new Vector3(xPos, spawnHeight, 0);
        Instantiate(rockPrefab, spawnPos, Quaternion.identity);
    }

    public void TakeDamage()
    {
        hp--;
        UpdateHPText();
        if (hp <= 0)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void UpdateHPText()
    {
        hpText.text = $"♥: {hp}";
    }
}
