using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public GameObject EventPanel;
    public Text EventText;
    public Button EventButton1;
    public Button EventButton2;

    private int id;
    private bool isEvent;
    private bool isEventFinished;
    private int scriptIndex;

    private void Start()
    {
        EventScriptManager.Instance.GetScriptId();
        isEventFinished = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !isEventFinished)
        {
            Event(id);
        }
        else if (Input.GetKeyDown(KeyCode.Return) && isEventFinished)
        {
            EventPanel.SetActive(false);
            HUDManager.Instance.ShowHUD();
        }
    }

    public void Event(int id)
    {
        Talk(id);

        EventPanel.SetActive(isEvent);
        HUDManager.Instance.HideHUD();
    }

    void Talk(int id)
    {
        string text = EventScriptManager.Instance.GetEventScript(id, scriptIndex);

        if (text == null)
        {
            scriptIndex = 0;

            EventButton1.gameObject.SetActive(true);
            EventButton2.gameObject.SetActive(true);

            EventButton1.onClick.AddListener(OnClickButton1);
            EventButton2.onClick.AddListener(OnClickButton2);
        }
        else
            EventText.text = text;

        isEvent = true;
        scriptIndex++;
    }

    public void OnClickButton1()
    {
        isEvent = false;
        isEventFinished = true;

        EventButton1.gameObject.SetActive(false);
        EventButton2.gameObject.SetActive(false);

        EventScriptManager.Instance.EventResultUpdate(id, 1);

        EventText.text = "1번을 선택하셨습니다";
    }

    public void OnClickButton2()
    {
        isEvent = false;
        isEventFinished = true;

        EventButton1.gameObject.SetActive(false);
        EventButton2.gameObject.SetActive(false);

        EventScriptManager.Instance.EventResultUpdate(id, 2);

        EventText.text = "2번을 선택하셨습니다";
    }
}
