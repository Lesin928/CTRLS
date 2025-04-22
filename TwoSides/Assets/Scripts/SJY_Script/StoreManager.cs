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

            int index = i; // Ŭ���� ���� ����
            itemButton[i].onClick.RemoveAllListeners();
            itemButton[i].onClick.AddListener(() => OnItemSelected(currentSelection[index]));
        }
    }

    void OnItemSelected(ItemData item)
    {
        Debug.Log($"������ ������: {item.statType.ToString()}");

        // ���⼭ ���� �ɷ�ġ ���� (��: �÷��̾� ��ü�� ����)
        ApplyItemEffect(item);
    }

    void ApplyItemEffect(ItemData item)
    {
        // ����: PlayerStatManager ���� Ŭ������ ����
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
