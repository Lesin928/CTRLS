using System.Collections.Generic;
using UnityEngine;

public class EventScriptManager : MonoBehaviour
{
    public static EventScriptManager Instance;

    Dictionary<int, string[]> EventScript;
    private bool[] idCheck;
    private int maxScriptCount = 2;

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

    private void Start()
    {
        idCheck = new bool[maxScriptCount];
    }

    private void GenerateScript()
    {
        EventScript.Add(0, new string[] { "임시 이벤트1",
            "1. maxHp +10, hp -10\n2. maxHp -10, hp +10"});
        EventScript.Add(1, new string[] { "임시 이벤트2",
            "1. maxHp +30, hp -30\n2. maxHp -30, hp +30"});
    }

    public string GetEventScript(int id, int scriptIndex)
    {
        if (scriptIndex == EventScript[id].Length)
        {
            idCheck[id] = true;
            return null;
        }

        else
            return EventScript[id][scriptIndex];
    }

    public bool isUsedId(int id)
    {
        if (idCheck[id])
            return true;
        return false;
    }

    public void EventResultUpdate(int id, int num)
    {
        switch (id)
        {
            case 0:
                if (num == 1)
                {
                    GameManager.Instance.AddMaxHealth(10);
                    GameManager.Instance.AddHealth(-10);
                }
                else
                {
                    GameManager.Instance.MinusMaxHealth(10);
                    GameManager.Instance.AddHealth(10);
                }
                break;
            case 1:
                if (num == 1)
                {
                    GameManager.Instance.AddMaxHealth(30);
                    GameManager.Instance.AddHealth(-30);
                }
                else
                {
                    GameManager.Instance.MinusMaxHealth(30);
                    GameManager.Instance.AddHealth(30);
                }
                break;
        }
    }
}
