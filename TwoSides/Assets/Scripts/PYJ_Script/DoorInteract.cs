using UnityEngine;

public class DoorInteract : Interactive
{
    private bool isOpen = false;

    private void Start()
    {
        if (!DoorManager.Instance.IsGameCleared)
        {
            GetComponent<Collider>().enabled = false; // 상호작용 막기
        }
    }

    public void EnableInteraction()
    {
        GetComponent<Collider>().enabled = true;
    }

    public override void PressF()
    {
        if (!DoorManager.Instance.IsGameCleared)
        {
            Debug.Log("게임을 클리어해야 문을 열 수 있습니다.");
            return;
        }

        if (isOpen)
        {
            Debug.Log("문은 이미 열려 있습니다.");
            return;
        }

        Debug.Log("문이 열립니다.");
        isOpen = true;
    }
}
