using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// HUD 관리하는 스크립트
// 켜고 끄기, UI 업데이트, TimeScale 관리
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

        HideHUD();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Title" || SceneManager.GetActiveScene().name == "TimeLine")
            HideHUD();

        if (isTrackingTime)
        {
            playTime += Time.unscaledDeltaTime;
            UpdateTimeText();
        }
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

    public void InitHUD()
    {
        maxHealth = GameManager.Instance.playerMaxHealth;
        health = GameManager.Instance.playerHealth;
        gold = GameManager.Instance.playerGold;

        UpdateHUD();
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

    public void SetGold(int value)
    {
        gold = value;
        UpdateHUD();
    }

    private void UpdateHUD()
    {
        if (healthSlider != null)
        {
            healthSlider.value = health;
            healthSlider.maxValue = maxHealth;
        }

        if (goldText != null)
            goldText.text = $"{gold}";
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

    public void ResumTrackingTime()
    {
        isTrackingTime = true;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        HUDManager.Instance?.HideHUD();
        HUDManager.Instance?.PauseTrackingTime();
    }

    public void ResumGame()
    {
        Time.timeScale = 1f;
        HUDManager.Instance?.ShowHUD();
        HUDManager.Instance?.ResumTrackingTime();
    }

    private void UpdateTimeText()
    {
        int minutes = Mathf.FloorToInt(playTime / 60f);
        int seconds = Mathf.FloorToInt(playTime % 60f);
        timeText.text = $"{minutes:00}:{seconds:00}";
    }
}
