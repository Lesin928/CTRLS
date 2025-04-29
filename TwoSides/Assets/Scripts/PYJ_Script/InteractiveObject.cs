using UnityEngine;

public class InteractiveObject : MonoBehaviour
{

    // 퍼즐 패널 UI
    public GameObject puzzlePanel;

    // 플레이어가 상호작용 범위 내에 있는지 체크
    private bool isPlayerInRange = false;




    /// <summary>
    /// 퍼즐 패널을 비활성화 (초기 상태)
    /// </summary>
    private void Start()
    {
        
        if (puzzlePanel != null)
        {
            puzzlePanel.SetActive(false);
        }
    }




    /// <summary>
    /// 플레이어가 범위 내에 있고 엔터 키를 누르면 퍼즐 패널을 활성화
    /// </summary>
    private void Update()
    {
        
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Return))
        {
            ShowPuzzle();
        }
    }



    /// <summary>
    /// 플레이어가 퍼즐 오브젝트와 충돌하였을 때 시작 메시지 출력
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // 플레이어가 범위 내에 들어왔을 때
            Debug.Log("퍼즐을 시작하려면 엔터 키를 누르세요.");
        }
    }




    /// <summary>
    /// 플레이어가 범위 내에 있고 엔터 키를 누르면 퍼즐 패널을 활성화
    /// </summary>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // 플레이어가 범위에서 나갔을 때
        }
    }


    /// <summary>
    /// 퍼즐 패널을 홯성화
    /// </summary>
    void ShowPuzzle()
    {
        if (puzzlePanel != null)
        {
            puzzlePanel.SetActive(true);
            Debug.Log("퍼즐이 시작되었습니다!");
        }
    }




    /// <summary>
    /// 퍼즐이 완료되면 퍼즐 창을 닫는 메시지 출력
    /// </summary>
    public void HidePuzzle()
    {
        if (puzzlePanel != null)
        {
            puzzlePanel.SetActive(false);
            Debug.Log("퍼즐이 완료되어 창이 닫혔습니다.");
        }
    }
}
