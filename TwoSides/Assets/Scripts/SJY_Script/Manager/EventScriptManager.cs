using System.Collections.Generic;
using UnityEngine;

public class EventScriptManager : MonoBehaviour
{
    public static EventScriptManager Instance;

    Dictionary<int, string[]> EventScript;
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
        GenerateScript();
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

    private void Start()
    {
        isFirstTime = true;
        idCheck = new bool[maxScriptCount];
    }

    private void GenerateScript()
    {
        EventScript.Add(0, new string[] { "임시 이벤트1",
            "1. maxHp +10, hp -10\n\n2. maxHp -10, hp +10"});
        EventScript.Add(1, new string[] { "임시 이벤트2",
            "1. maxHp +30, hp -30\n\n2. maxHp -30, hp +30"});
        EventScript.Add(2, new string[] { "임시 이벤트3",
            "1. test1\n\n2. test2"});
        EventScript.Add(100, new string[] { "이곳은 튜토리얼 테스트입니다.",
            "1스테이지에서 열립니다",
            "마지막 문장입니다"});
    }

    public string GetEventScript(int id, int scriptIndex)
    {
        if (scriptIndex == EventScript[id].Length)
        {
            //idCheck[id] = true;
            return null;
        }
        else
            return EventScript[id][scriptIndex];
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

    public void EventResultUpdate(int id, int num)
    {
        switch (id)
        {
            case 0:
                if (num == 1)
                {
                    GameManager.Instance.SetMaxHealth(10);
                    GameManager.Instance.SetHealth(-10);
                }
                else
                {
                    GameManager.Instance.SetMaxHealth(10);
                    GameManager.Instance.SetHealth(10);
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
    }
}
