using UnityEngine;

public class DoorInteract : Interactive
{
    private Collider2D doorCollider;

    private void Start()
    {
        doorCollider = GetComponent<Collider2D>();
        if (doorCollider == null)
        {
            Debug.LogError("Collider2D ������Ʈ�� �����ϴ�!");
        }

        doorCollider.enabled = false; // ���� �� �ݶ��̴� ��Ȱ��ȭ
    }

    private void Update()
    {
        if (GameManager.Instance != null && doorCollider != null)
        {
            // GameManager.Instance.isClear �� Ȯ��
            Debug.Log("isClear ��: " + GameManager.Instance.isClear);

            if (GameManager.Instance.isClear && !doorCollider.enabled)
            {
                doorCollider.enabled = true;
                Debug.Log("Ŭ���� ���� ������ �� �� �ݶ��̴� Ȱ��ȭ");
            }
        }
        else
        {
            Debug.LogError("GameManager �ν��Ͻ��� null�Դϴ�!");
        }
    }

    public override void PressF()
    {
        // GameManager.Instance.isClear �� Ȯ��
        Debug.Log("PressF ȣ�� �� isClear ��: " + GameManager.Instance.isClear);

        if (GameManager.Instance.isClear)
        {
            Debug.Log("�� ����! ���� ������ �̵��մϴ�.");
        }
        else
        {
            Debug.Log("���Ͱ� ���� ���Ҵ�...");
        }
    }
}