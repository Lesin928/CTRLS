using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    private bool hasActivated = false;
    private bool isPanelOpen = false;

    public GameObject interactionPanel; // 인스펙터에서 연결

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasActivated && other.CompareTag("Player"))
        {
            hasActivated = true;
            Debug.Log("플레이어가 트리거에 들어왔습니다. F 키를 누르세요.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hasActivated = false;
            isPanelOpen = false;
            if (interactionPanel != null)
                interactionPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (!hasActivated || !Input.GetKeyDown(KeyCode.F)) return;

        if (!isPanelOpen)
        {
            // 첫 번째 F: 패널 열기
            if (interactionPanel != null)
                interactionPanel.SetActive(true);
            isPanelOpen = true;
            Debug.Log("패널 열림. 다시 F를 누르면 게임이 시작됩니다.");
        }
        else
        {
            // 두 번째 F: 게임 시작
            if (!RockManager.Instance) return;

            if (!RockManager.Instance.IsGameStarted())
            {
                RockManager.Instance.StartGame();
                Debug.Log("게임 시작!");
            }

            RockManager.Instance.StartSpawning();

            isPanelOpen = false;
            if (interactionPanel != null)
                interactionPanel.SetActive(false);

            Debug.Log("F키 두 번째 눌림! 바위 스폰 시작!");
        }
    }
}


