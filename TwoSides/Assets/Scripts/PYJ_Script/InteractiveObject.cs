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
    /// �÷��̾ ���� ���� �ְ� F Ű�� ������ ���� �г��� Ȱ��ȭ
    /// </summary>
    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
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
            Debug.Log("������ �����Ϸ��� FŰ�� ��������.");
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
    public void ShowPuzzle()
    {
        if (PuzzleManager1.Instance == null)
            return;

        if (PuzzleManager1.Instance.IsPuzzleCleared())
        {
            Debug.Log("�̹� ������ Ŭ�����߽��ϴ�!");
            return;
        }

        PuzzleManager1.Instance.puzzlePanel.SetActive(true);
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
