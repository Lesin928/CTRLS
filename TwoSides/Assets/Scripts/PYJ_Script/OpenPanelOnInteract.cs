using UnityEngine;

public class OpenPanelOnInteract : MonoBehaviour
{
    // �÷��̾ ��ȣ�ۿ��� �� �ִ� �Ÿ�
    public float interactionDistance = 3f;

    // UI �г� ������Ʈ (����Ƽ �����Ϳ��� �Ҵ�)
    public GameObject panelToOpen;

    // �÷��̾��� Transform (�ַ� MainCamera�� �÷��̾� ��ü)
    public Transform player;

    // ���� ����: ���� �г��� ���� �ִ��� ����
    private bool isPanelOpen = false;

    void Start()
    {
        // ������ �� �г��� ���� ����
        if (panelToOpen != null)
        {
            panelToOpen.SetActive(false);
        }
    }

    void Update()
    {
        // �÷��̾�� �� ������Ʈ ���� �Ÿ� ���
        float distance = Vector3.Distance(player.position, transform.position);

        // �Ÿ��� ��ȣ�ۿ� �Ÿ� �̳��� ��
        if (distance <= interactionDistance)
        {
            // F Ű�� �������� Ȯ��
            if (Input.GetKeyDown(KeyCode.F))
            {
                TogglePanel();
            }
        }
    }

    // �г� ����/�ݱ� ��� �Լ�
    void TogglePanel()
    {
        if (panelToOpen != null)
        {
            isPanelOpen = !isPanelOpen;
            panelToOpen.SetActive(isPanelOpen);
        }
    }
}
