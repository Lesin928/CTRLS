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
                    resultText = "당신은 꽤나 충동적인 사람이다.\t감성 +20";
                }
                else
                {
                    resultText = "수상한 것은 건드리지 않는게 상책.\t이성 +20";
                }
                break;
            case 2:
                if (num == 1)
                {
                    GameManager.Instance.SetPlayerAttackSpeed(10);
                    GameManager.Instance.SetPlayerMoveSpeed(10);
                    resultText = "책에는 유용한 지식이 많았다.\t이성 +10 /공격속도+10/이동속도+10";
                }
                else
                {
                    GameManager.Instance.SetHealth(10);
                    resultText = "이야기를 듣는 동안 기운을 조금 회복했다.\t감성 +20 체력+10";
                }
                break;
            case 3:
                if (num == 1)
                {
                    GameManager.Instance.SetHealth(-10);
                    resultText = "상자를 여는 데 힘이 부쳤다.\t이성 +10 체력 -10";
                }
                else
                {
                    resultText = "남의 것은 손대선 안 된다.\t감성 +10";
                }
                break;
            case 4:
                if (num == 1)
                {
                    GameManager.Instance.SetHealth(+10);
                    resultText = "기운이 조금 회복되었다.\t체력 +10 / 감성+10";
                }
                else
                {
                    resultText = "잘 모르는 것은 마시지 말자.\t이성 +20";
                }
                break;
            case 5:
                if (num == 1)
                {
                    resultText = "함께 애도하자.\t감성 +20";
                }
                else
                {
                    resultText = "당신은 쓰레기다.\t이성 +20";
                }
                break;
            case 6:
                if (num == 1)
                {
                    GameManager.Instance.SetHealth(10);
                    resultText = "약을 마시고 기운을 회복했다.\t체력 +10 / 감성 +10";
                }
                else
                {
                    resultText = "수상한 사람이 주는 것은 거절하겠어.\t이성 +20";
                }
                break;
            case 7:
                if (num == 1)
                {
                    GameManager.Instance.SetPlayerArmor(10);
                    resultText = "단단한 기운이 몸을 감싼다.\t감성 +10 / 방어력 +10";
                }
                else
                {
                    resultText = "수상한 것은 건드리지 않는 게 상책.\t이성 +20";
                }
                break;
            case 8:
                if (num == 1)
                {
                    GameManager.Instance.SetPlayerMoveSpeed(10);
                    resultText = "시계를 뒤집음과 동시에 몸이 가벼워짐을 느낀다.\t감성 +10/이동속도+10";
                }
                else
                {
                    resultText = "굳이 나서서 뒤집지는 말자.\t이성 +20";
                }
                break;
            case 9:
                if (num == 1)
                {
                    GameManager.Instance.SetPlayerCritical(10);
                    GameManager.Instance.SetPlayerCriticalDamage(10);
                    resultText = "있을 리 없는 눈동자와 마주친 것 같은 기분이다.\t감성 +10/치명타피해10증가/치명타확률10증가";
                }
                else
                {
                    GameManager.Instance.SetHealth(-10);
                    GameManager.Instance.SetPlayerAttack(-10);
                    GameManager.Instance.SetPlayerArmor(-10);
                    resultText = "어딘가 불길한 해골이다. \t이성 +10 체력 -10 공격력 -10 방어력 -10";
                }
                break;
        }

        return resultText;
    }
}
