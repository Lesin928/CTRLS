using UnityEngine;

public class InteractiveObject : MonoBehaviour
{

    // ���� �г� UI
    public GameObject puzzlePanel;

    // �÷��̾ ��ȣ�ۿ� ���� ���� �ִ��� üũ
    private bool isPlayerInRange = false;




    /// <summary>
    /// ���� �г��� ��Ȱ��ȭ (�ʱ� ����)
    /// </summary>
    private void Start()
    {
        
        if (puzzlePanel != null)
        {
            puzzlePanel.SetActive(false);
        }
    }




    /// <summary>
    /// �÷��̾ ���� ���� �ְ� ���� Ű�� ������ ���� �г��� Ȱ��ȭ
    /// </summary>
    private void Update()
    {
        
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Return))
        {
            ShowPuzzle();
        }
    }



    /// <summary>
    /// �÷��̾ ���� ������Ʈ�� �浹�Ͽ��� �� ���� �޽��� ���
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // �÷��̾ ���� ���� ������ ��
            Debug.Log("������ �����Ϸ��� ���� Ű�� ��������.");
        }
    }




    /// <summary>
    /// �÷��̾ ���� ���� �ְ� ���� Ű�� ������ ���� �г��� Ȱ��ȭ
    /// </summary>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // �÷��̾ �������� ������ ��
        }
    }


    /// <summary>
    /// ���� �г��� �l��ȭ
    /// </summary>
    void ShowPuzzle()
    {
        if (puzzlePanel != null)
        {
            puzzlePanel.SetActive(true);
            Debug.Log("������ ���۵Ǿ����ϴ�!");
        }
    }




    /// <summary>
    /// ������ �Ϸ�Ǹ� ���� â�� �ݴ� �޽��� ���
    /// </summary>
    public void HidePuzzle()
    {
        if (puzzlePanel != null)
        {
            puzzlePanel.SetActive(false);
            Debug.Log("������ �Ϸ�Ǿ� â�� �������ϴ�.");
        }
    }
}
