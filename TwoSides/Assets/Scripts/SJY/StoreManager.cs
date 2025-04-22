using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public Button[] itemButton;
    public Button rerollButton;
    public Text playerGold;

    private List<ItemData> itemDataList = new List<ItemData>();
    private List<ItemData> currentSelection = new List<ItemData>();

    void Start()
    {
        InitializeItems();
        rerollButton.onClick.AddListener(RerollItems);
        RerollItems();
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
    }

    List<ItemData> GetRandomItems()
    {
        List<ItemData> copy = new List<ItemData>(itemDataList);
        List<ItemData> result = new List<ItemData>();

        for (int i = 0; i < 4; i++)
        {
            int rand = Random.Range(0, copy.Count);
            result.Add(copy[rand]);
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
        }
    }

    void OnItemSelected(ItemData item)
    {
        Debug.Log($"구매한 아이템: {item.statType.ToString()}");

        // 여기서 실제 능력치 적용 (예: 플레이어 객체에 접근)
        ApplyItemEffect(item);
    }

    void ApplyItemEffect(ItemData item)
    {
        // 예시: PlayerStatManager 같은 클래스에 적용
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

    void Update()
    {

    }
}
