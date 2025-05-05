using UnityEngine;

public class InteractiveAble : MonoBehaviour
{
    public GameObject F;

    //�ش� ������Ʈ�� Ȱ��ȭ �Ǿ��� ��
    private void OnEnable()
    {
        // FŰ �̹��� ����
        F.SetActive(false);
        // ���� �÷��̾� ������Ʈ�� �浹���� ��
        if (Physics2D.OverlapCircle(transform.position, 0.1f, LayerMask.GetMask("Player")) != null)
        {
            // FŰ �̹��� ���
            F.SetActive(true);
        }
    }
    private void OnDisable()
    {
        // FŰ �̹��� ����
        F.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾�� �浹���� ��
        if (collision.CompareTag("Player"))
        {
            // FŰ �̹��� ���
            F.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // �÷��̾�� �浹�� ������ ��
        if (collision.CompareTag("Player"))
        {
            // FŰ �̹��� ����
            F.SetActive(false);
        }
    }   
}
