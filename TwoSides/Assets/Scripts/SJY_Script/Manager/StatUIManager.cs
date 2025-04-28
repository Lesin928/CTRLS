using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class StatUIManager : MonoBehaviour
{
    public static StatUIManager Instance;

    public GameObject statWindow;

    public Text healthText;
    public Text attackText;
    public Text attackSpeedText;
    public Text criticalText;
    public Text criticalDamageText;
    public Text armorText;
    public Text moveSpeedText;
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

        UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<GameObject>("StatUIManager").Completed += handle =>
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
            HUDManager.Instance.PauseGame();
            UpdateStatText();
        }
        else
        {
            HUDManager.Instance.ResumGame();
        }
    }

    private void UpdateStatText()
    {
        var gm = GameManager.Instance;

        healthText.text = $"HP : {gm.playerHealth} / {gm.maxHealth}";
        attackText.text = $"Attack : {gm.playerAttack}";
        attackSpeedText.text = $"AttackSpeed : {gm.playerAttackSpeed}%";
        criticalText.text = $"Critical : {gm.playerCritical}%";
        criticalDamageText.text = $"CriticalDamage : {gm.playerCriticalDamage}%";
        armorText.text = $"Armor : {gm.playerArmor}";
        moveSpeedText.text = $"MoveSpeed : {gm.playerMoveSpeed}%";
        goldText.text = $"Gold : {gm.playerGold}";
    }
}
