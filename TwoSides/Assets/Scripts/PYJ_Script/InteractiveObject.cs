using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    // 플레이어가 퍼즐 오브젝트 범위에 들어왔는지 여부
    private bool isPlayerInRange = false;

    private void Start()
    {
        // 게임 시작 시 퍼즐 패널을 비활성화
        if (PuzzleManager1.Instance != null && PuzzleManager1.Instance.puzzlePanel != null)
        {
            PuzzleManager1.Instance.puzzlePanel.SetActive(false);
        }
    }

    private void Update()
    {
        // 플레이어가 범위 내에 있고 F 키를 누른 경우에만 퍼즐을 토글
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            TogglePuzzle();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어가 "Puzzle" 태그를 가진 오브젝트와 부딪힌 경우
        if (other.CompareTag("Player") && this.CompareTag("Puzzle"))
        {
            isPlayerInRange = true;
            Debug.Log("퍼즐을 시작하려면 F키를 누르세요.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 플레이어가 "Puzzle" 태그를 가진 오브젝트 범위를 벗어난 경우
        if (other.CompareTag("Player") && this.CompareTag("Puzzle"))
        {
            isPlayerInRange = false;
        }
    }


    /// <summary>
    /// 퍼즐 패널을 열거나 닫음 (토글 기능)
    /// </summary>
    private void TogglePuzzle()
    {
        // 퍼즐 매니저나 퍼즐 패널이 존재하지 않으면 리턴
        if (PuzzleManager1.Instance == null || PuzzleManager1.Instance.puzzlePanel == null)
            return;

        // 퍼즐을 이미 클리어한 경우 메시지만 출력하고 리턴
        if (PuzzleManager1.Instance.IsPuzzleCleared())
        {
            Debug.Log("이미 퍼즐을 클리어했습니다!");
            return;
        }

        // 퍼즐 패널 상태에 따라 열거나 닫기
        bool isActive = PuzzleManager1.Instance.puzzlePanel.activeSelf;

        if (isActive)
        {
            HidePuzzle();  // 퍼즐 패널이 켜져 있으면 닫기
        }
        else
        {
            ShowPuzzle();  // 퍼즐 패널이 꺼져 있으면 열기
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
