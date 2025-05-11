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

    [SerializeField] private int reRollPrice;

    void Start()
    {
        if (GameManager.Instance.isStoreReset)
        {
            ResetPrice();
            GameManager.Instance.isStoreReset = false;
        }

        reRollPrice = 5;

        rerollButton.onClick.AddListener(RerollItems);
        exitButton.onClick.AddListener(ExitStore);

        GameManager.Instance.OnStageClear();
    }

    public void OpenStore()
    {
        RerollItems();
        //UpdateItemUI();

        Store.SetActive(true);
        HUDManager.Instance.PauseGame();
        Debug.Log("Store");

        isStoreOpen = true;
    }

    void RerollItems()
    {
        if (isStoreOpen)
        {
            if (GameManager.Instance.playerGold - reRollPrice < 0)
                return;
            else
                GameManager.Instance.SetGold(-reRollPrice);
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
            Text[] text = itemButton[i].GetComponentsInChildren<Text>();
            var icon = itemButton[i].transform.Find("IconImage").GetComponent<Image>();

            text[0].text = $"{item.statType.ToString()}";
            text[1].text = $"{item.price.ToString()}";
            icon.sprite = item.icon;

            int index = i; // 클로저 문제 방지
            itemButton[i].onClick.RemoveAllListeners();
            itemButton[i].onClick.AddListener(() => OnItemSelected(currentSelection[index]));

            playerGold.text = $"Gold : {GameManager.Instance.playerGold}";
        }
    }

    void OnItemSelected(ItemData item)
    {
        if (GameManager.Instance.playerGold - item.price < 0)
            return;

        Debug.Log($"구매한 아이템: {item.statType.ToString()}");

        GameManager.Instance.SetGold(-item.price);

        item.price += 50;
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

    public void ResetPrice()
    {
        Debug.Log("Reset!");
        foreach (var item in itemDataList)
        {
            item.price = 100;
        }
    }

    void ExitStore()
    {
        isStoreOpen = false;
        Store.SetActive(false);
        HUDManager.Instance.ResumGame();
    }
}
