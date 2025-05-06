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
            return EventScript[id][scriptIndex].Replace("\\n", "\n");
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
        string text = "return text";

        switch (id)
        {
            case 0:
                if (num == 1)
                {
                    GameManager.Instance.SetMaxHealth(10);
                    text = "고픈 배가 조금은 찼다.  이성 +10 체력 +10";
                }
                else
                {
                    text = "남의 것을 빼앗고 싶진 않다.  감성 +20";
                }
                break;
            case 1:
                if (num == 1)
                {
                    text = "당신은 꽤나 충동적인 사람이다.  감성 +20";
                }
                else
                {
                    text = "수상한 것은 건드리지 않는 게 상책.  이성+20";
                }
                break;
            case 2:
                if (num == 1)
                {
                    GameManager.Instance.SetPlayerAttackSpeed(10);
                    GameManager.Instance.SetPlayerMoveSpeed(10);
                    text = "책에는 유용한 지식이 많았다.  이성 +10 공격속도+10 이동속도+10";
                }
                else
                {
                    GameManager.Instance.SetHealth(10);
                    text = "이야기를 듣는 동안 기운을 조금 회복했다.  감성 +20 체력+10";
                }
                break;
            case 3:
                if (num == 1)
                {
                    GameManager.Instance.SetHealth(-10);
                    text = "상자를 여는 데 힘이 부쳤다.  이성 +10 체력 -10";
                }
                else
                {
                    text = "남의 것은 손대선 안 된다.  감성 +10";
                }
                break;
            case 4:
                if (num == 1)
                {
                    GameManager.Instance.SetHealth(10);
                    text = "기운이 조금 회복되었다. 체력 +10 감성+10";
                }
                else
                {
                    text = "잘 모르는 것은 마시지 말자. 이성 +20";
                }
                break;
            case 5:
                if (num == 1)
                {
                    text = "함께 애도하자.  감성 +20";
                }
                else
                {
                    text = "당신은 쓰레기다.  이성 +20";
                }
                break;
            case 6:
                if (num == 1)
                {
                    GameManager.Instance.SetHealth(10);
                    text = "약을 마시고 기운을 회복했다.  체력 +10 감성 +10";
                }
                else
                {
                    text = "수상한 사람이 주는 것은 거절하겠어.  이성 +20";
                }
                break;
            case 7:
                if (num == 1)
                {
                    GameManager.Instance.SetPlayerArmor(10);
                    text = "단단한 기운이 몸을 감싼다.  감성 +10 방어력 +10";
                }
                else
                {
                    text = "수상한 것은 건드리지 않는 게 상책.  이성 +20";
                }
                break;
            case 8:
                if (num == 1)
                {
                    GameManager.Instance.SetPlayerMoveSpeed(10);
                    text = "시계를 뒤집음과 동시에 몸이 가벼워짐을 느낀다.  감성 +10 이동속도+10";
                }
                else
                {
                    text = "굳이 나서서 뒤집지는 말자.  이성 +20";
                }
                break;
            case 9:
                if (num == 1)
                {
                    GameManager.Instance.SetPlayerCritical(10);
                    GameManager.Instance.SetPlayerCriticalDamage(10);
                    text = "있을 리 없는 눈동자와 마주친 것 같은 기분이다.  감성 +10 치명타피해 +10 치명타확률 +10";
                }
                else
                {
                    GameManager.Instance.SetHealth(-10);
                    GameManager.Instance.SetPlayerAttack(-10);
                    GameManager.Instance.SetPlayerArmor(-10);
                    text = "어딘가 불길한 해골이다.  이성 +10 체력 -10 공격력 -10 방어력 -10";
                }
                break;
        }

        return text;
    }
}
