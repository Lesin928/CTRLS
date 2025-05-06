using UnityEngine;

public class InteractObject : Interactive
{
    public GameObject panelToShow;  // �ν����Ϳ��� ������ �г�

    public override void PressF()
    {
        Debug.Log("����� ������.");

        if (panelToShow != null)
        {
            //panelToShow.SetActive(true); // �г� ǥ��
            if (EventManager.Instance.isEventFinished == false)
            {
                EventManager.Instance.StartEvent();
            }
            else
            {
                EventManager.Instance.ExitEvent();
            }
        }
        else
        {
            Debug.LogWarning("panelToShow�� ������� �ʾҽ��ϴ�!");
        }
    }
}