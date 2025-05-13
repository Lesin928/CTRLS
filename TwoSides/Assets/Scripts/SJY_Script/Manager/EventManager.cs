using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ?방 이벤트 관리하는 스크립트트
public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public GameObject EventPanel;
    public Text EventText;
    public Button EventButton1;
    public Button EventButton2;

    //이벤트 아닌 일반대화 전용
    public static int TUTORIAL = 101;

    //이벤트방 번호마다 fixedEventId를 지정해줌줌
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
        //튜토리얼 전용 이벤트, 타임라인으로 대체함함
        if (GameManager.Instance.currentStage == 1)
        {
            id = TUTORIAL;
            isEventFinished = false;
        }
        else
        {
            //씬이름이 'Mystery0'일때 숫자부분만 가져옴옴
            string sceneName = SceneManager.GetActiveScene().name;
            sceneName.Substring(8);

            string number = new string(sceneName.Where(char.IsDigit).ToArray());
            Debug.Log("씬번호 : " + number);
            id = int.Parse(number);
            Debug.Log("이벤트 아이디 : " + id);
            isEventTalk = true;
        }
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

    // 전체적인 흐름
    // f키를 눌러 상호작용 할때마다 StartEvent() 호출
    // StartEvent()에서 일반 대화/이지선다 이벤트 구분
    // 1. 일반대화 이면 다음 대화 스크립트가 남아있는지 확인후
    // 남아있으면 isEventFinished = false; 
    // 남아있지 않으면 isEventFinished = true;로로 스테이지클리어
    // 2. 이지선다 이벤트면 버튼을 활성화
    // 버튼 클릭시 isEventFinished = true;로 스테이지클리어
    public void StartEvent()
    {
        EventPanel.SetActive(true);
        Event(id);
    }

    public void ExitEvent()
    {
        EventPanel.SetActive(false);

        if (isEventTalk)
        {
            Debug.Log("이벤트 종료");
            GameManager.Instance.OnStageClear();
        }
        else
        {
            id++;
            scriptIndex = 0;
            isEventFinished = false;

            // 다음 스크립트가 없으면 대화가 종료됨됨
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

    // f 한번 누를때마다 스크립트 하나씩 출력력
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
            // 스크립트가 끝나면 선택지 버튼 활성화화
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
