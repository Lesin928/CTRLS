using UnityEngine;
using UnityEngine.UI;

public class GameManager1 : MonoBehaviour
{
    // �̱��� ����
    public static GameManager1 Instance;

    // ���� ���� ������
    public GameObject rockPrefab; // ���� ������
    public float spawnInterval = 1.0f; // ���� ���� ����

    [Header("Spawn Settings")]
    public float spawnHeight = 5f; // �������� ������ Y ��ġ
    public float spawnXMin = -3f;  // ���� ������ X �ּҰ�
    public float spawnXMax = 3f;   // ���� ������ X �ִ밪

    [Header("Player Settings")]
    public int hp = 3; // �÷��̾��� ü��
    public Text hpText; // ü�� ǥ�ÿ� �ؽ�Ʈ UI

    [Header("UI Panels")]
    public GameObject gameOverPanel; // ���� ���� �г�
    public GameObject gameClearPanel; // ���� Ŭ���� �г�

    [Header("Game Settings")]
    public float survivalTime = 10f; // ���� ��ǥ �ð� (Ŭ���� ������ 10�ʷ� ����)

    private float timer; // Ÿ�̸�
    private bool gameStarted = false; // ���� ���� ����

    [Header("Heart Images")]
    public Image[] hearts; // ü���� ��Ÿ���� ��Ʈ UI �̹��� �迭

    // ���� ���� ��ư�� ������ �� ȣ��Ǵ� �Լ�
    public void OnStartButtonPressed()
    {
        GameManager1.Instance.StartGame(); // ���� ����
    }

    // ������ ���۵Ǿ����� Ȯ���ϴ� �Լ�
    public bool IsGameStarted()
    {
        return gameStarted;
    }

    // �̱��� �ν��Ͻ��� �����ϴ� �Լ�
    void Awake()
    {
        Instance = this;
    }

    // ���� �ʱ�ȭ �� UI ����
    void Start()
    {
        timer = survivalTime; // Ÿ�̸� ���� (10�ʷ� ����)
        UpdateHPText(); // ü�� �ؽ�Ʈ ������Ʈ
        HideHearts(); // ��Ʈ UI �ʱ�ȭ (�� ���̰� ����)

        gameOverPanel.SetActive(false); // ���� ���� �г� �����
        gameClearPanel.SetActive(false); // ���� Ŭ���� �г� �����
    }

    // ��Ʈ UI ����� �Լ�
    void HideHearts()
    {
        foreach (var heart in hearts)
        {
            heart.gameObject.SetActive(false); // ��� ��Ʈ UI ��Ȱ��ȭ
        }
    }

    // ������ �����ϴ� �Լ�
    public void StartGame()
    {
        gameStarted = true; // ���� ���� ���·� ����
        gameOverPanel.SetActive(false); // ���� ���� �г� �����
        gameClearPanel.SetActive(false); // ���� Ŭ���� �г� �����
        Time.timeScale = 1;  // ������ ������ �ʵ��� ����
    }

    // ���� ����߸��� ������ �ܺο��� ȣ���� �� �ֵ��� �и��� �Լ�
    public void StartSpawning()
    {
        if (gameStarted) // ������ ���۵Ǿ��ٸ�
        {
            ShowHearts(); // ��Ʈ UI �ѱ�
            InvokeRepeating(nameof(SpawnRock), 1f, spawnInterval); // ���� ���� ����
        }
    }

    // ��Ʈ UI�� Ȱ��ȭ�ϴ� �Լ�
    void ShowHearts()
    {
        foreach (var heart in hearts)
        {
            heart.gameObject.SetActive(true); // ��Ʈ UI Ȱ��ȭ
        }
    }

    // �� �����Ӹ��� ȣ��Ǵ� Update �Լ�
    void Update()
    {
        // ���� ������ ���� Ŭ���� ���¿��� ���� �Է� üũ
        if ((gameOverPanel.activeSelf || gameClearPanel.activeSelf))
        {
            if (Input.GetKeyDown(KeyCode.Return)) // ���� Ű �Է� ��
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

        // Ÿ�̸Ӹ� �ٿ����� ���� �ð� �帧
        timer -= Time.deltaTime;

        // === ���� Ŭ���� ���� �߰� ===
        if (timer <= 0f)
        {
            timer = 0f;
            gameClearPanel.SetActive(true);   // ���� Ŭ���� UI ǥ��
            CancelInvoke(nameof(SpawnRock));  // ���� �������� �� ����
            gameStarted = false;              // ���� ����
        }
    }


    // ������ ����߸��� �Լ�
    void SpawnRock()
    {
        float xPos = Random.Range(spawnXMin, spawnXMax); // X�� ��ġ ����
        Vector3 spawnPos = new Vector3(xPos, spawnHeight, 0); // ���� ���� ��ġ
        Instantiate(rockPrefab, spawnPos, Quaternion.identity); // ���� ����
    }

    // ��Ʈ UI ������Ʈ �Լ�
    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < hp)
                hearts[i].enabled = true;  // ��Ʈ UI ��
            else
                hearts[i].enabled = false; // ��Ʈ UI ��
        }
    }

    // �÷��̾ ���ظ� ���� �� ȣ��Ǵ� �Լ�
    public void TakeDamage()
    {
        Debug.Log("���� �浹!"); // ����� �޽���
        hp--; // ü�� ����
        UpdateHPText(); // ü�� �ؽ�Ʈ ������Ʈ
        UpdateHearts(); // ��Ʈ UI ������Ʈ

        if (hp <= 0)
        {
            gameOverPanel.SetActive(true); // ���� ���� �г� ǥ��
        }
    }

    // ü�� �ؽ�Ʈ ������Ʈ �Լ�
    void UpdateHPText()
    {
        hpText.text = $"��: {hp}"; // ü�� �ؽ�Ʈ ������Ʈ
    }
}
