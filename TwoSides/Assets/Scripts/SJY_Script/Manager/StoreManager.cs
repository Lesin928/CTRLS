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

    private List<ItemData> itemDataList = new List<ItemData>();
    private List<ItemData> currentSelection = new List<ItemData>();

    void Start()
    {
        isStoreOpen = false;

        InitializeItems();
        rerollButton.onClick.AddListener(RerollItems);
        exitButton.onClick.AddListener(ExitStore);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && !isStoreOpen)
        {
            isStoreOpen = true;
            RerollItems();
            Store.SetActive(true);
            HUDManager.Instance.PauseGame();
            Debug.Log("Store");
        }
    }

    void InitializeItems()
    {
        itemDataList = new List<ItemData>()
        {
            new ItemData(StatType.Health, 10f),
            new ItemData(StatType.MaxHealth, 10f),
            new ItemData(StatType.Armor, 10f),
            new ItemData(StatType.Attack, 10f),
            new ItemData(StatType.AttackSpeed, 10f),
            new ItemData(StatType.MoveSpeed, 10f),
            new ItemData(StatType.Critical, 10f),
            new ItemData(StatType.CriticalDamage, 10f),
        };
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
            text.text = $"{item.statType.ToString()}";

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
                GameManager.Instance.SetMaxHealth(item.value);
                break;
            case StatType.Attack:
                Debug.Log($"Attack +{item.value}");
                GameManager.Instance.SetMaxHealth(item.value);
                break;
            case StatType.AttackSpeed:
                Debug.Log($"AttackSpeed +{item.value}");
                GameManager.Instance.SetMaxHealth(item.value);
                break;
            case StatType.MoveSpeed:
                Debug.Log($"MoveSpeed +{item.value}");
                GameManager.Instance.SetMaxHealth(item.value);
                break;
            case StatType.Critical:
                Debug.Log($"Critical +{item.value}");
                GameManager.Instance.SetMaxHealth(item.value);
                break;
            case StatType.CriticalDamage:
                Debug.Log($"CriticalDamage +{item.value}");
                GameManager.Instance.SetMaxHealth(item.value);
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
