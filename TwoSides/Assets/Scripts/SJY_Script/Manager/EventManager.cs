using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public GameObject EventPanel;
    public Text EventText;
    public Button EventButton1;
    public Button EventButton2;

    //이벤트 아닌 일반대화 전용
    public static int TUTORIAL = 101;

    public int fixedEventId = -1;
    private int id;
    public bool isEventFinished;
    private int scriptIndex;
    public bool isEventTalk = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if (fixedEventId > 0)
            id = fixedEventId;
        else if (GameManager.Instance.currentStage == 1)
        {
            id = TUTORIAL;
            isEventTalk = true;
        }
        else
            id = EventScriptManager.Instance.GetScriptId();
        isEventFinished = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !isEventFinished)
        {
            StartEvent();
        }
        else if (Input.GetKeyDown(KeyCode.Return) && isEventFinished)
        {
            ExitEvent();
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void StartEvent()
    {
        Event(id);
        EventPanel.SetActive(true);
    }

    public void ExitEvent()
    {
        // 이벤트 !!
        Debug.Log("이벤트 종료");
        EventPanel.SetActive(false);

        if (isEventTalk)
        {
            GameManager.Instance.OnStageClear();
        }
        else
        {
            id++;
            scriptIndex = 0;
            isEventFinished = false;

            if (EventScriptManager.Instance.GetEventScript(id, scriptIndex) == null)
            {
                Debug.Log("저스트이벤트 종료");
                isEventFinished = true;
                GameManager.Instance.OnStageClear();
            }
        }

    }

    private void Event(int id)
    {
        if (id > 10)
            Talk(id);
        else
            EventTalk(id);
    }

    private void Talk(int id)
    {
        int maxIndex = EventScriptManager.Instance.GetMaxScriptCount(id);
        string text = EventScriptManager.Instance.GetEventScript(id, scriptIndex);

        EventText.text = text;
        scriptIndex++;
        if (scriptIndex == maxIndex)
            isEventFinished = true;
    }

    private void EventTalk(int id)
    {
        string text = EventScriptManager.Instance.GetEventScript(id, scriptIndex);

        if (text == null)
        {
            EventButton1.gameObject.SetActive(true);
            EventButton2.gameObject.SetActive(true);

            EventButton1.onClick.AddListener(OnClickButton1);
            EventButton2.onClick.AddListener(OnClickButton2);
        }
        else
        {
            EventText.text = text;
            scriptIndex++;
        }
    }

    private void OnClickButton1()
    {
        isEventFinished = true;

        EventButton1.gameObject.SetActive(false);
        EventButton2.gameObject.SetActive(false);

        string resaultText = EventScriptManager.Instance.EventResultUpdate(id, 1);

        EventText.text = resaultText;
    }

    private void OnClickButton2()
    {
        isEventFinished = true;

        EventButton1.gameObject.SetActive(false);
        EventButton2.gameObject.SetActive(false);

        string resaultText = EventScriptManager.Instance.EventResultUpdate(id, 2);

        EventText.text = resaultText;
    }
}
