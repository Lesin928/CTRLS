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
    private int maxScriptCount = 2;


    private void Start()
    {
        isEventFinished = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !isEventFinished)
            Event();
        else if (Input.GetKeyDown(KeyCode.Return) && isEventFinished)
            EventPanel.SetActive(false);
    }

    public void Event()
    {
        //랜덤으로 이벤트스크립트 가져오고 사용한 id 제거
        id = Random.Range(0, maxScriptCount);
        Talk(id);

        //int id;
        //do
        //{
        //    id = Random.Range(0, maxScriptCount);
        //} while (idCheck[id]);

        //idCheck[id] = true;
        //Talk(id);

        EventPanel.SetActive(isEvent);
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
