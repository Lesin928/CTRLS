using System.Collections.Generic;
using UnityEngine;

public class EventScriptManager : MonoBehaviour
{
    public static EventScriptManager Instance;

    public List<EventData> eventList;

    private Dictionary<int, string[]> EventScript;
    private bool[] idCheck;
    private int maxScriptCount = 3;
    private bool isFirstTime;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        EventScript = new Dictionary<int, string[]>();
        foreach (var eventData in eventList)
        {
            EventScript[eventData.id] = eventData.scripts;
        }
    }

    private void Start()
    {
        isFirstTime = true;
        idCheck = new bool[maxScriptCount];
    }

    public static void Init()
    {
        if (Instance != null) return;

        UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<GameObject>("EventScriptManager").Completed += handle =>
        {
            if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                Instantiate(handle.Result);
            }
            else
            {
                Debug.LogError("Failed to load EventScriptManager");
            }
        };
    }

    public string GetEventScript(int id, int scriptIndex)
    {
        if (scriptIndex == EventScript[id].Length)
        {
            return null;
        }
        else
            return EventScript[id][scriptIndex];
    }

    public int GetMaxScriptCount(int id)
    {
        return EventScript[id].Length;
    }

    public int GetScriptId()
    {
        int id;

        if (isFirstTime)
        {
            isFirstTime = false;
            id = Random.Range(0, maxScriptCount);
        }
        else
        {
            do
            {
                id = Random.Range(0, maxScriptCount);
            } while (idCheck[id]);
        }

        idCheck[id] = true;
        return id;
    }

    public string EventResultUpdate(int id, int num)
    {
        string resultText = "tmpText";



        switch (id)
        {
            case 0:
                if (num == 1)
                {
                    GameManager.Instance.SetHealth(10);
                    resultText = "고픈 배가 조금은 찼다.\t이성 +10 체력 +10";
                }
                else
                {
                    resultText = "남의 것을 빼앗고 싶진 않다.\t감성 +20";
                }
                break;
            case 1:
                if (num == 1)
                {
                    GameManager.Instance.SetMaxHealth(30);
                    GameManager.Instance.SetHealth(-30);
                }
                else
                {
                    GameManager.Instance.SetMaxHealth(30);
                    GameManager.Instance.SetHealth(30);
                }
                break;
            case 2:
                if (num == 1)
                {
                    Debug.Log("test1");
                }
                else
                {
                    Debug.Log("test2");
                }
                break;
        }

        return resultText;
    }
}
