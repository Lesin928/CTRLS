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
                    text = "���� �谡 ������ á��.  �̼� +10 ü�� +10";
                }
                else
                {
                    text = "���� ���� ���Ѱ� ���� �ʴ�.  ���� +20";
                }
                break;
            case 1:
                if (num == 1)
                {
                    text = "����� �ϳ� �浿���� ����̴�.  ���� +20";
                }
                else
                {
                    text = "������ ���� �ǵ帮�� �ʴ� �� ��å.  �̼�+20";
                }
                break;
            case 2:
                if (num == 1)
                {
                    GameManager.Instance.SetPlayerAttackSpeed(10);
                    GameManager.Instance.SetPlayerMoveSpeed(10);
                    text = "å���� ������ ������ ���Ҵ�.  �̼� +10 ���ݼӵ�+10 �̵��ӵ�+10";
                }
                else
                {
                    GameManager.Instance.SetHealth(10);
                    text = "�̾߱⸦ ��� ���� ����� ���� ȸ���ߴ�.  ���� +20 ü��+10";
                }
                break;
            case 3:
                if (num == 1)
                {
                    GameManager.Instance.SetHealth(-10);
                    text = "���ڸ� ���� �� ���� ���ƴ�.  �̼� +10 ü�� -10";
                }
                else
                {
                    text = "���� ���� �մ뼱 �� �ȴ�.  ���� +10";
                }
                break;
            case 4:
                if (num == 1)
                {
                    GameManager.Instance.SetHealth(10);
                    text = "����� ���� ȸ���Ǿ���. ü�� +10 ����+10";
                }
                else
                {
                    text = "�� �𸣴� ���� ������ ����. �̼� +20";
                }
                break;
            case 5:
                if (num == 1)
                {
                    text = "�Բ� �ֵ�����.  ���� +20";
                }
                else
                {
                    text = "����� �������.  �̼� +20";
                }
                break;
            case 6:
                if (num == 1)
                {
                    GameManager.Instance.SetHealth(10);
                    text = "���� ���ð� ����� ȸ���ߴ�.  ü�� +10 ���� +10";
                }
                else
                {
                    text = "������ ����� �ִ� ���� �����ϰھ�.  �̼� +20";
                }
                break;
            case 7:
                if (num == 1)
                {
                    GameManager.Instance.SetPlayerArmor(10);
                    text = "�ܴ��� ����� ���� ���Ѵ�.  ���� +10 ���� +10";
                }
                else
                {
                    text = "������ ���� �ǵ帮�� �ʴ� �� ��å.  �̼� +20";
                }
                break;
            case 8:
                if (num == 1)
                {
                    GameManager.Instance.SetPlayerMoveSpeed(10);
                    text = "�ð踦 �������� ���ÿ� ���� ���������� ������.  ���� +10 �̵��ӵ�+10";
                }
                else
                {
                    text = "���� ������ �������� ����.  �̼� +20";
                }
                break;
            case 9:
                if (num == 1)
                {
                    GameManager.Instance.SetPlayerCritical(10);
                    GameManager.Instance.SetPlayerCriticalDamage(10);
                    text = "���� �� ���� �����ڿ� ����ģ �� ���� ����̴�.  ���� +10 ġ��Ÿ���� +10 ġ��ŸȮ�� +10";
                }
                else
                {
                    GameManager.Instance.SetHealth(-10);
                    GameManager.Instance.SetPlayerAttack(-10);
                    GameManager.Instance.SetPlayerArmor(-10);
                    text = "��� �ұ��� �ذ��̴�.  �̼� +10 ü�� -10 ���ݷ� -10 ���� -10";
                }
                break;
        }

        return text;
    }
}
