using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;

    public Text goldText;
    public Image goldImage;
    public Text timeText;
    public Image timeImage;
    public Slider healthSlider;

    private float playTime = 0f;
    private bool isTrackingTime = false;

    private float health;
    private float maxHealth;
    private int gold;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        canvasGroup = GetComponent<CanvasGroup>();
    }

    public static void Init()
    {
        if (Instance != null) return;

        UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<GameObject>("HUDManager").Completed += handle =>
        {
            if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                Instantiate(handle.Result);
            }
            else
            {
                Debug.LogError("Failed to load HUDManager");
            }
        };
    }

    private void Update()
    {
        if (isTrackingTime)
        {
            playTime += Time.unscaledDeltaTime;
            UpdateTimeText();
        }
    }

    public void InitHUD()
    {
        maxHealth = GameManager.Instance.maxHealth;
        health = GameManager.Instance.playerHealth;
        gold = GameManager.Instance.playerGold;

        UpdateHUD();

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            StartCoroutine(FadeInHUD());
        }
    }

    public void ShowHUD() =>
        Instance?.gameObject.SetActive(true);

    public void HideHUD() =>
        Instance?.gameObject.SetActive(false);

    public void SetHealth(float value)
    {
        health = value;
        UpdateHUD();
        Debug.Log($"Health: {health}/{maxHealth}");
    }

    public void SetMaxHealth(float value)
    {
        maxHealth = value;
        healthSlider.maxValue = value;
        UpdateHUD();
        Debug.Log($"Health: {health}/{maxHealth}");
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateHUD();
    }

    private void UpdateHUD()
    {
        if (healthSlider != null)
            healthSlider.value = health;
        if (goldText != null) goldText.text = $"Gold: {gold}";
    }

    private IEnumerator FadeInHUD()
    {
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    public void StartTrackingTime()
    {
        playTime = 0f;
        isTrackingTime = true;
    }

    public void PauseTrackingTime()
    {
        isTrackingTime = false;
    }

    public void ResumeTrackingTime()
    {
        isTrackingTime = true;
    }

    private void UpdateTimeText()
    {
        int minutes = Mathf.FloorToInt(playTime / 60f);
        int seconds = Mathf.FloorToInt(playTime % 60f);
        timeText.text = $"Time: {minutes:00}:{seconds:00}";
    }
}
