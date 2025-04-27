using UnityEngine;
using UnityEngine.UI;

public class GameManager1 : MonoBehaviour
{
    public static GameManager1 Instance;

    public GameObject rockPrefab;
    public float spawnInterval = 1.0f;
    public Transform spawnAreaTop;
    public int hp = 3;
    public Text hpText;
    public GameObject gameOverPanel;
    public GameObject gameClearPanel;
    public float survivalTime = 20f;

    private float timer;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        InvokeRepeating(nameof(SpawnRock), 1f, spawnInterval);
        timer = survivalTime;
        UpdateHPText();
    }

    void Update()
    {
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
        float xPos = Random.Range(-spawnAreaTop.localScale.x / 2f, spawnAreaTop.localScale.x / 2f);
        Vector3 spawnPos = new Vector3(xPos, spawnAreaTop.position.y, 0);
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
        hpText.text = $"¢¾: {hp}";
    }
}
