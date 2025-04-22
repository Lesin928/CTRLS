using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public GameObject puzzlePanel; // ���� �г� UI
    private bool isPlayerInRange = false; // �÷��̾ ��ȣ�ۿ� ���� ���� �ִ��� üũ

    private void Start()
    {
        // ���� �г��� ��Ȱ��ȭ (�ʱ� ����)
        if (puzzlePanel != null)
        {
            puzzlePanel.SetActive(false);
        }
    }

    private void Update()
    {
        // �÷��̾ ���� ���� �ְ� ���� Ű�� ������ ���� �г��� Ȱ��ȭ
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Return))
        {
            ShowPuzzle();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // �÷��̾ ���� ���� ������ ��
            Debug.Log("������ �����Ϸ��� ���� Ű�� ��������.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // �÷��̾ �������� ������ ��
        }
    }

    void ShowPuzzle()
    {
        // ���� �г��� Ȱ��ȭ
        if (puzzlePanel != null)
        {
            puzzlePanel.SetActive(true);
            Debug.Log("������ ���۵Ǿ����ϴ�!");
        }
    }
}
