using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public GameObject EventPanel;
    public Text EventText;
    public Button EventButton1;
    public Button EventButton2;

    //이벤트 아닌 일반대화 전용
    public static int TUTORIAL = 100;

    private int id;
    private bool isEventFinished;
    private int scriptIndex;

    private void Start()
    {
        // 이벤트맵이 아닌 다른 곳에서 사용시 elseif를 걸어서 사용해 주세요
        // ex) 보스스테이지 GameManager.Instance.currentStage == 15
        if (GameManager.Instance.currentStage == 1)
            id = TUTORIAL;
        else
            id = EventScriptManager.Instance.GetScriptId();
        isEventFinished = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !isEventFinished)
        {
            Event(id);
            EventPanel.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Return) && isEventFinished)
        {
            ExitEvent();
        }
    }

    private void ExitEvent()
    {
        EventPanel.SetActive(false);
        HUDManager.Instance.ResumGame();
    }

    void Event(int id)
    {
        EventTalk(id);

        HUDManager.Instance.PauseGame();
    }

    void EventTalk(int id)
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

            //일반 대화 탈출 조건
            if (id == TUTORIAL && scriptIndex == 3)
                isEventFinished = true;
        }
    }

    void OnClickButton1()
    {
        isEventFinished = true;

        EventButton1.gameObject.SetActive(false);
        EventButton2.gameObject.SetActive(false);

        EventScriptManager.Instance.EventResultUpdate(id, 1);

        EventText.text = "1번을 선택하셨습니다";
    }

    void OnClickButton2()
    {
        isEventFinished = true;

        EventButton1.gameObject.SetActive(false);
        EventButton2.gameObject.SetActive(false);

        EventScriptManager.Instance.EventResultUpdate(id, 2);

        EventText.text = "2번을 선택하셨습니다";
    }
}
