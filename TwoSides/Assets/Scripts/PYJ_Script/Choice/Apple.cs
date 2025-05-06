using UnityEngine;

public class Apple : Interactive
{
    public GameObject panelToShow;  // 인스펙터에서 연결할 패널

    public override void PressF()
    {
        Debug.Log("사과를 만졌따.");

        if (panelToShow != null)
        {
            panelToShow.SetActive(true); // 패널 표시
        }
        else
        {
            Debug.LogWarning("panelToShow가 연결되지 않았습니다!");
        }
    }
}
