using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    private bool isPlayerInRange = false;

    private void Start()
    {
        // 퍼즐 패널 비활성화 (초기 상태)
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
            Debug.Log("퍼즐을 시작하려면 F키를 누르세요.");
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
    /// 퍼즐 패널 토글
    /// </summary>
    private void TogglePuzzle()
    {
        if (PuzzleManager1.Instance == null || PuzzleManager1.Instance.puzzlePanel == null)
            return;

        if (PuzzleManager1.Instance.IsPuzzleCleared())
        {
            Debug.Log("이미 퍼즐을 클리어했습니다!");
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
    /// 퍼즐 패널 보이기
    /// </summary>
    public void ShowPuzzle()
    {
        PuzzleManager1.Instance.puzzlePanel.SetActive(true);
    }

    /// <summary>
    /// 퍼즐 패널 숨기기
    /// </summary>
    public void HidePuzzle()
    {
        PuzzleManager1.Instance.puzzlePanel.SetActive(false);
        Debug.Log("퍼즐 창이 닫혔습니다.");
    }
}
