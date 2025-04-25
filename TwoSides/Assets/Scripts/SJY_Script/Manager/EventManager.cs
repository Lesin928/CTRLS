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

    //�̺�Ʈ �ƴ� �Ϲݴ�ȭ ����
    public static int TUTORIAL = 100;

    private int id;
    private bool isEventFinished;
    private int scriptIndex;

    private void Start()
    {
        // �̺�Ʈ���� �ƴ� �ٸ� ������ ���� elseif�� �ɾ ����� �ּ���
        // ex) ������������ GameManager.Instance.currentStage == 15
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

            //�Ϲ� ��ȭ Ż�� ����
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

        EventText.text = "1���� �����ϼ̽��ϴ�";
    }

    void OnClickButton2()
    {
        isEventFinished = true;

        EventButton1.gameObject.SetActive(false);
        EventButton2.gameObject.SetActive(false);

        EventScriptManager.Instance.EventResultUpdate(id, 2);

        EventText.text = "2���� �����ϼ̽��ϴ�";
    }
}
