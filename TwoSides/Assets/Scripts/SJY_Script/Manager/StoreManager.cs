using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public GameObject Store;
    public Button[] itemButton;
    public Button rerollButton;
    public Text playerGold;
    public Button exitButton;
    public bool isStoreOpen;

    public List<ItemData> itemDataList;
    private List<ItemData> currentSelection = new List<ItemData>();

    void Start()
    {
        isStoreOpen = false;

        rerollButton.onClick.AddListener(RerollItems);
        exitButton.onClick.AddListener(ExitStore);

        GameManager.Instance.isClear = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && !isStoreOpen)
        {
            OpenStore();
        }
    }

    public void OpenStore()
    {
        RerollItems();
        Store.SetActive(true);
        HUDManager.Instance.PauseGame();
        Debug.Log("Store");

        isStoreOpen = true;
    }

    void RerollItems()
    {
        if (isStoreOpen)
        {
            if (GameManager.Instance.playerGold - 10 < 0)
                return;
            else
                GameManager.Instance.SetGold(-10);
        }

        currentSelection = GetRandomItems();
        UpdateItemUI();
    }

    List<ItemData> GetRandomItems()
    {
        List<ItemData> copy = new List<ItemData>(itemDataList);
        List<ItemData> result = new List<ItemData>();

        for (int i = result.Count; i < 5; i++)
        {
            int rand = Random.Range(0, copy.Count);
            result.Add(copy[rand]);
            copy.RemoveAt(rand);
        }

        return result;
    }

    void UpdateItemUI()
    {
        for (int i = 0; i < itemButton.Length; i++)
        {
            var item = currentSelection[i];
            var text = itemButton[i].GetComponentInChildren<Text>();
            var icon = itemButton[i].transform.Find("IconImage").GetComponent<Image>();

            text.text = $"{item.statType.ToString()}";
            icon.sprite = item.icon;

            int index = i; // 클로저 문제 방지
            itemButton[i].onClick.RemoveAllListeners();
            itemButton[i].onClick.AddListener(() => OnItemSelected(currentSelection[index]));

            playerGold.text = $"Gold : {GameManager.Instance.playerGold}";
        }
    }

    void OnItemSelected(ItemData item)
    {
        if (GameManager.Instance.playerGold - 50 < 0)   //임시로 50    경고음 추가하기
            return;

        Debug.Log($"구매한 아이템: {item.statType.ToString()}");

        GameManager.Instance.SetGold(-50);
        ApplyItemEffect(item);
        UpdateItemUI();
    }

    void ApplyItemEffect(ItemData item)
    {
        switch (item.statType)
        {
            case StatType.Health:
                Debug.Log($"Health +{item.value}");
                GameManager.Instance.SetHealth(item.value);
                break;
            case StatType.MaxHealth:
                Debug.Log($"MaxHealth +{item.value}");
                GameManager.Instance.SetMaxHealth(item.value);
                break;
            case StatType.Armor:
                Debug.Log($"Armor +{item.value}");
                GameManager.Instance.SetPlayerArmor(item.value);
                break;
            case StatType.Attack:
                Debug.Log($"Attack +{item.value}");
                GameManager.Instance.SetPlayerAttack(item.value);
                break;
            case StatType.AttackSpeed:
                Debug.Log($"AttackSpeed +{item.value}");
                GameManager.Instance.SetPlayerAttackSpeed(item.value);
                break;
            case StatType.MoveSpeed:
                Debug.Log($"MoveSpeed +{item.value}");
                GameManager.Instance.SetPlayerMoveSpeed(item.value);
                break;
            case StatType.Critical:
                Debug.Log($"Critical +{item.value}");
                GameManager.Instance.SetPlayerCritical(item.value);
                break;
            case StatType.CriticalDamage:
                Debug.Log($"CriticalDamage +{item.value}");
                GameManager.Instance.SetPlayerCriticalDamage(item.value);
                break;
        }
    }

    void ExitStore()
    {
        isStoreOpen = false;
        Store.SetActive(false);
        HUDManager.Instance.ResumGame();
    }
}
