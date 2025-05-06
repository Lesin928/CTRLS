using UnityEngine;

public class Apple : Interactive
{
    public GameObject panelToShow;  // �ν����Ϳ��� ������ �г�

    public override void PressF()
    {
        Debug.Log("����� ������.");

        if (panelToShow != null)
        {
            if (EventManager.Instance.isEventFinished == false)
                EventManager.Instance.StartEvent();
            else
                EventManager.Instance.ExitEvent();
            //panelToShow.SetActive(true); // �г� ǥ��
        }
        else
        {
            Debug.LogWarning("panelToShow�� ������� �ʾҽ��ϴ�!");
        }
    }
}
