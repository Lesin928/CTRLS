using UnityEngine;

public class DoorInteract : Interactive
{
    private bool isOpen = false;

    private void Start()
    {
        if (!DoorManager.Instance.IsGameCleared)
        {
            GetComponent<Collider>().enabled = false; // ��ȣ�ۿ� ����
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
            Debug.Log("������ Ŭ�����ؾ� ���� �� �� �ֽ��ϴ�.");
            return;
        }

        if (isOpen)
        {
            Debug.Log("���� �̹� ���� �ֽ��ϴ�.");
            return;
        }

        Debug.Log("���� �����ϴ�.");
        isOpen = true;
    }
}
