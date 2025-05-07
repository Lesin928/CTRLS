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
        gameStarted = true;
        gameOverPanel.SetActive(false);
        gameClearPanel.SetActive(false);
        Time.timeScale = 1;

        StartSpawning(); // �� �� �߰�!
    }




    /// <summary>
    /// ���� ����߸��� ������ �ܺο��� ȣ���� �� �ֵ��� �и��� �Լ� 
    /// </summary>
    public void StartSpawning()
    {
        if (gameStarted)
        {
            ShowHearts();
            InvokeRepeating(nameof(SpawnRock), 1f, spawnInterval);

            // survivalTime �Ŀ� StopSpawning ȣ�� ����
            Invoke(nameof(StopSpawning), survivalTime);
        }
    }
    void StopSpawning()
    {
        CancelInvoke(nameof(SpawnRock));

        // ���� ���� ���¸� Ŭ���� ó������ ����
        if (hp <= 0)
        {
            Debug.Log("���� ���� �����̹Ƿ� Ŭ���� ó�� �� ��");
            return;
        }

        gameClearPanel.SetActive(true); // Ŭ���� UI ǥ��
        Debug.Log("���� Ŭ���� - �� ���� ����");
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
            clearPanelClosing = true; // �ߺ� ���� ����
            Invoke(nameof(CloseClearPanel), 2f); // 2�� �� �ڵ� ����
            return;
        }
    }

    void CloseClearPanel()
    {
        gameClearPanel.SetActive(false);
        CancelInvoke(nameof(SpawnRock));
        gameStarted = false;
        clearPanelClosing = false; // �ٽ� �ʱ�ȭ
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
    public void TakeDamage2()
    {
        Debug.Log("���� �浹!");
        hp--;
        UpdateHPText();
        UpdateHearts();

        if (hp <= 0)
        {
            gameOverPanel.SetActive(true);

            // Ŭ���� ������ ���� ����� StopSpawning ���
            CancelInvoke(nameof(StopSpawning));

            // ���� ������ ����
            CancelInvoke(nameof(SpawnRock));
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
