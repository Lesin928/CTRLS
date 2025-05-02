using UnityEngine;
using UnityEngine.UI;

public class RockManager : MonoBehaviour
{
    // �̱��� ����
    public static RockManager Instance;

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




    /// <summary>
    /// ���� ���� ��ư�� ������ �� ȣ��Ǵ� �Լ� 
    /// </summary>
    public void OnStartButtonPressed()
    {
        RockManager.Instance.StartGame(); // ���� ����
    }


    /// <summary>
    /// ������ ���۵Ǿ����� Ȯ���ϴ� �Լ�
    /// </summary>
    public bool IsGameStarted()
    {
        return gameStarted;
    }




    /// <summary>
    /// �̱��� �ν��Ͻ��� �����ϴ� �Լ�
    /// </summary>
    void Awake()
    {
        Instance = this;
    }




    /// <summary>
    /// ���� �ʱ�ȭ �� UI ����
    /// </summary>
    void Start()
    {
        timer = survivalTime; // Ÿ�̸� ���� (10�ʷ� ����)
        UpdateHPText(); // ü�� �ؽ�Ʈ ������Ʈ
        HideHearts(); // ��Ʈ UI �ʱ�ȭ (�� ���̰� ����)

        gameOverPanel.SetActive(false); // ���� ���� �г� �����
        gameClearPanel.SetActive(false); // ���� Ŭ���� �г� �����
    }



    /// <summary>
    /// ��Ʈ UI ����� �Լ�
    /// </summary>

    void HideHearts()
    {
        foreach (var heart in hearts)
        {
            heart.gameObject.SetActive(false); // ��� ��Ʈ UI ��Ȱ��ȭ
        }
    }



    /// <summary>
    /// ������ �����ϴ� �Լ� 
    /// </summary>

    public void StartGame()
    {
        gameStarted = true; // ���� ���� ���·� ����
        gameOverPanel.SetActive(false); // ���� ���� �г� �����
        gameClearPanel.SetActive(false); // ���� Ŭ���� �г� �����
        Time.timeScale = 1;  // ������ ������ �ʵ��� ����
    }



    /// <summary>
    /// ���� ����߸��� ������ �ܺο��� ȣ���� �� �ֵ��� �и��� �Լ� 
    /// </summary>
    public void StartSpawning()
    {
        if (gameStarted) // ������ ���۵Ǿ��ٸ�
        {
            ShowHearts(); // ��Ʈ UI �ѱ�
            InvokeRepeating(nameof(SpawnRock), 1f, spawnInterval); // ���� ���� ����
        }
    }



    /// <summary>
    /// ��Ʈ UI�� Ȱ��ȭ�ϴ� �Լ� 
    /// </summary>
    void ShowHearts()
    {
        foreach (var heart in hearts)
        {
            heart.gameObject.SetActive(true); // ��Ʈ UI Ȱ��ȭ
        }
    }



    /// <summary>
    /// �� �����Ӹ��� ȣ��Ǵ� Update �Լ� 
    /// </summary>
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
        if (timer <= 2f)
        {

            CancelInvoke(nameof(SpawnRock));  // ���� ���� ����

        }

        if (timer <= 0f)
        {
            timer = 0f;
            gameClearPanel.SetActive(true);   // ���� Ŭ���� UI ǥ��
            gameStarted = false;              // ���� ����
        }
    }




    /// <summary>
    /// ������ ����߸��� �Լ�
    /// </summary>
    void SpawnRock()
    {
        float xPos = Random.Range(spawnXMin, spawnXMax); // X�� ��ġ ����
        Vector3 spawnPos = new Vector3(xPos, spawnHeight, 0); // ���� ���� ��ġ
        Instantiate(rockPrefab, spawnPos, Quaternion.identity); // ���� ����
    }



    /// <summary>
    /// ��Ʈ UI ������Ʈ �Լ�
    /// </summary>
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


    /// <summary>
    /// �÷��̾ ���ظ� ���� �� ȣ��Ǵ� �Լ�
    /// </summary>
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




    /// <summary>
    /// ü�� �ؽ�Ʈ ������Ʈ �Լ�
    /// </summary>
    void UpdateHPText()
    {
        hpText.text = $"��: {hp}"; // ü�� �ؽ�Ʈ ������Ʈ
    }
}
