using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    public static StatManager Instance;

    public GameObject statWindow;

    public Text healthText;
    public Text attackText;
    public Text goldText;

    private bool isOpen = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void Init()
    {
        if (Instance != null) return;

        UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<GameObject>("StatManager").Completed += handle =>
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleStatWindow();
        }
    }

    public void InitStatWindow()
    {
        isOpen = false;
        statWindow.SetActive(false);
    }

    public void ToggleStatWindow()
    {
        isOpen = !isOpen;
        statWindow.SetActive(isOpen);

        if (isOpen)
        {
            Time.timeScale = 0f;
            HUDManager.Instance?.HideHUD();
            HUDManager.Instance?.PauseTrackingTime();
            UpdateStatText();
        }
        else
        {
            Time.timeScale = 0f;
            HUDManager.Instance?.ShowHUD();
            HUDManager.Instance?.ResumeTrackingTime();
        }
    }

    private void UpdateStatText()
    {
        var gm = GameManager.Instance;

        healthText.text = $"HP : {gm.playerHealth} / {gm.maxHealth}";
        attackText.text = $"Attack : {gm.playerAttack}";
        goldText.text = $"Gold : {gm.playerGold}";
    }
}
