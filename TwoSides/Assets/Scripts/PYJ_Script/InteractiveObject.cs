using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    // �÷��̾ ���� ������Ʈ ������ ���Դ��� ����
    private bool isPlayerInRange = false;

    private void Start()
    {
        // ���� ���� �� ���� �г��� ��Ȱ��ȭ
        if (PuzzleManager1.Instance != null && PuzzleManager1.Instance.puzzlePanel != null)
        {
            PuzzleManager1.Instance.puzzlePanel.SetActive(false);
        }
    }

    private void Update()
    {
        // �÷��̾ ���� ���� �ְ� F Ű�� ���� ��쿡�� ������ ���
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            TogglePuzzle();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �÷��̾ "Puzzle" �±׸� ���� ������Ʈ�� �ε��� ���
        if (other.CompareTag("Player") && this.CompareTag("Puzzle"))
        {
            isPlayerInRange = true;
            Debug.Log("������ �����Ϸ��� FŰ�� ��������.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // �÷��̾ "Puzzle" �±׸� ���� ������Ʈ ������ ��� ���
        if (other.CompareTag("Player") && this.CompareTag("Puzzle"))
        {
            isPlayerInRange = false;
        }
    }


    /// <summary>
    /// ���� �г��� ���ų� ���� (��� ���)
    /// </summary>
    private void TogglePuzzle()
    {
        // ���� �Ŵ����� ���� �г��� �������� ������ ����
        if (PuzzleManager1.Instance == null || PuzzleManager1.Instance.puzzlePanel == null)
            return;

        // ������ �̹� Ŭ������ ��� �޽����� ����ϰ� ����
        if (PuzzleManager1.Instance.IsPuzzleCleared())
        {
            Debug.Log("�̹� ������ Ŭ�����߽��ϴ�!");
            return;
        }

        // ���� �г� ���¿� ���� ���ų� �ݱ�
        bool isActive = PuzzleManager1.Instance.puzzlePanel.activeSelf;

        if (isActive)
        {
            HidePuzzle();  // ���� �г��� ���� ������ �ݱ�
        }
        else
        {
            ShowPuzzle();  // ���� �г��� ���� ������ ����
        }
    }

    /// <summary>
    /// ���� �г� ���̱�
    /// </summary>
    public void ShowPuzzle()
    {
        PuzzleManager1.Instance.puzzlePanel.SetActive(true);
    }

    /// <summary>
    /// ���� �г� �����
    /// </summary>
    public void HidePuzzle()
    {
        PuzzleManager1.Instance.puzzlePanel.SetActive(false);
        Debug.Log("���� â�� �������ϴ�.");
    }
}
