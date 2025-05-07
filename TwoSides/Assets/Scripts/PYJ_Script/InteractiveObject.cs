using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    private bool isPlayerInRange = false;

    private void Start()
    {
        // ���� �г� ��Ȱ��ȭ (�ʱ� ����)
        if (PuzzleManager1.Instance != null && PuzzleManager1.Instance.puzzlePanel != null)
        {
            PuzzleManager1.Instance.puzzlePanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            TogglePuzzle();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("������ �����Ϸ��� FŰ�� ��������.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    /// <summary>
    /// ���� �г� ���
    /// </summary>
    private void TogglePuzzle()
    {
        if (PuzzleManager1.Instance == null || PuzzleManager1.Instance.puzzlePanel == null)
            return;

        if (PuzzleManager1.Instance.IsPuzzleCleared())
        {
            Debug.Log("�̹� ������ Ŭ�����߽��ϴ�!");
            return;
        }

        bool isActive = PuzzleManager1.Instance.puzzlePanel.activeSelf;

        if (isActive)
        {
            HidePuzzle();
        }
        else
        {
            ShowPuzzle();
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
