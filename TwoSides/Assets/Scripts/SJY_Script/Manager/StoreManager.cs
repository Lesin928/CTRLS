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

    private List<ItemData> itemDataList = new List<ItemData>();
    private List<ItemData> currentSelection = new List<ItemData>();

    void Start()
    {
        InitializeItems();
        rerollButton.onClick.AddListener(RerollItems);
        exitButton.onClick.AddListener(ExitStore);
        RerollItems();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
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
        currentSelection = GetRandomItems();
        UpdateItemUI();
        GameManager.Instance.AddGold(-10);
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
        Debug.Log($"구매한 아이템: {item.statType.ToString()}");

        GameManager.Instance.AddGold(-50);
        ApplyItemEffect(item);
    }

    void ApplyItemEffect(ItemData item)
    {
        switch (item.statType)
        {
            case StatType.Health:
                Debug.Log($"Health +{item.value}");
                GameManager.Instance.AddHealth(item.value);
                break;
            case StatType.MaxHealth:
                Debug.Log($"MaxHealth +{item.value}");
                GameManager.Instance.AddMaxHealth(item.value);
                break;
        }
    }

    void ExitStore()
    {
        Store.SetActive(false);
        HUDManager.Instance.ResumGame();
    }
}
