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
        // 새로운 게임이 시작되면 가격을 초기화
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
        // 상점이 열릴때 마다 새로운 아이템 가져오기기
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
            if (GameManager.Instance.playerGold - reRollPrice < 0)
                return;
            else
                GameManager.Instance.SetGold(-reRollPrice);
        }

        currentSelection = GetRandomItems();
        UpdateItemUI();
    }

    //ItemData에서 4개를 랜덤으로 가져오는 함수
    // copy는 원본의 복사본
    // result는 랜덤으로 가져올 아이템 리스트트
    List<ItemData> GetRandomItems()
    {
        List<ItemData> copy = new List<ItemData>(itemDataList);
        List<ItemData> result = new List<ItemData>();

        for (int i = result.Count; i < 5; i++)
        {
            int rand = Random.Range(0, copy.Count);
            result.Add(copy[rand]);
            //중복 방지를 위해 가져온 아이템은 복사본에서 제거거
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

            int realPrice = GameManager.Instance.GetItemPrice(item.statType);
            item.price = realPrice;

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

        Debug.Log($"구매한 아이템: {item.statType}");

        GameManager.Instance.SetGold(-item.price);

        // 구매할때마다 아이템 가격 증가
        GameManager.Instance.IncreaseItemPrice(item.statType, 50);

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
        GameManager.Instance.itemPriceMap.Clear();
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
