using UnityEngine;
using UnityEngine.UI;

public class GameManager1 : MonoBehaviour
{
    public static GameManager1 Instance;

    public GameObject rockPrefab;
    public float spawnInterval = 1.0f;

    [Header("Spawn Settings")]
    public float spawnHeight = 5f; // �������� ���� (Y��)
    public float spawnXMin = -3f;  // X �ּҰ�
    public float spawnXMax = 3f;   // X �ִ밪

    [Header("Player Settings")]
    public int hp = 3;
    public Text hpText;

    [Header("UI Panels")]
    public GameObject gameOverPanel;
    public GameObject gameClearPanel;

    [Header("Game Settings")]
    public float survivalTime = 20f;

    private float timer;
    private bool gameStarted = false; // ���� ���� ����

    [Header("Heart Images")]
    public Image[] hearts;  // 3��¥�� ��Ʈ �迭
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
        HideHearts(); // << ��Ʈ ������
    }

    void HideHearts()
    {
        foreach (var heart in hearts)
        {
            heart.gameObject.SetActive(false);
        }
    }

    // ���� ���� ��ư�� ������ �� ȣ��Ǵ� �Լ�
    public void StartGame()
    {
        gameStarted = true; // ���� ����
        gameOverPanel.SetActive(false);
        gameClearPanel.SetActive(false);
        Time.timeScale = 1;  // ������ ������ �ʵ���
    }

    // ���� ����߸��� ������ �ܺο��� ȣ���� �� �ֵ��� ���� �и�
    public void StartSpawning()
    {
        if (gameStarted)
        {
            ShowHearts(); // << ��Ʈ ���ֱ�
            InvokeRepeating(nameof(SpawnRock), 1f, spawnInterval);
        }
    }
    void ShowHearts()
    {
        foreach (var heart in hearts)
        {
            heart.gameObject.SetActive(true);
        }
    }


    void Update()
    {
        if (!gameStarted) return;  // ������ ���۵��� ������ �ƹ� �ϵ� �Ͼ�� �ʰ�

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
    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < hp)
                hearts[i].enabled = true;  // ��Ʈ ��
            else
                hearts[i].enabled = false; // ��Ʈ ��
        }
    }

    public void TakeDamage()
    {
        hp--;
        UpdateHPText();
        UpdateHearts();

        if (hp <= 0)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }


    void UpdateHPText()
    {
        hpText.text = $"��: {hp}";
    }
}
