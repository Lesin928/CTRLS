using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public GameObject puzzlePanel; // 퍼즐 패널 UI
    private bool isPlayerInRange = false; // 플레이어가 상호작용 범위 내에 있는지 체크

    private void Start()
    {
        // 퍼즐 패널을 비활성화 (초기 상태)
        if (puzzlePanel != null)
        {
            puzzlePanel.SetActive(false);
        }
    }

    private void Update()
    {
        // 플레이어가 범위 내에 있고 엔터 키를 누르면 퍼즐 패널을 활성화
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Return))
        {
            ShowPuzzle();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // 플레이어가 범위 내에 들어왔을 때
            Debug.Log("퍼즐을 시작하려면 엔터 키를 누르세요.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // 플레이어가 범위에서 나갔을 때
        }
    }

    void ShowPuzzle()
    {
        // 퍼즐 패널을 활성화
        if (puzzlePanel != null)
        {
            puzzlePanel.SetActive(true);
            Debug.Log("퍼즐이 시작되었습니다!");
        }
    }
}
