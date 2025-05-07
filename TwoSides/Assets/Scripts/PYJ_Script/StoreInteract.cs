using UnityEngine;

public class StoreInteract : Interactive
{
    public GameObject go;
    public StoreManager sm;
    private Collider2D storeCollider;

    private void Start()
    {
        sm=go.GetComponent<StoreManager>();
        storeCollider = GetComponent<Collider2D>();
        if (storeCollider == null)
        {
            Debug.LogError("Collider2D ������Ʈ�� �����ϴ�!");
        }

        storeCollider.enabled = false; // ���� �� �ݶ��̴� ��Ȱ��ȭ
    }

    private void Update()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager �ν��Ͻ��� null�Դϴ�!");
            return;
        }

        if (storeCollider == null)
        {
            Debug.LogError("doorCollider�� null�Դϴ�!");
            return;
        }

        if (GameManager.Instance.isClear && !storeCollider.enabled)
        {
            storeCollider.enabled = true;
            Debug.Log("����� �ݶ��̴� Ȱ��ȭ");
        }
    }


    public override void PressF()
    {
        sm.OpenStore();
        //// GameManager.Instance.isClear �� Ȯ��
        //Debug.Log("PressF ȣ�� �� isClear ��: " + GameManager.Instance.isClear);

        //if (GameManager.Instance.isClear)
        //{
        //    Debug.Log("�� ����! ���� ������ �̵��մϴ�.");//���� ������ 
        //    Mapbutton.Instance.clearOn = true;
        //    Mapbutton.Instance.GameClearAutoButton();
        //}
        //else
        //{
        //    Debug.Log("���Ͱ� ���� ���Ҵ�...");//���� ����
        //}
    }
}