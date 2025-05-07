using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    private bool hasActivated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasActivated && other.CompareTag("Player"))
        {
            hasActivated = true;
            Debug.Log("플레이어가 트리거에 들어왔습니다. F 키를 누르면 게임이 시작됩니다.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hasActivated = false;
        }
    }

    private void Update()
    {
        if (!hasActivated || !Input.GetKeyDown(KeyCode.F)) return;

        if (!RockManager.Instance) return;

        if (!RockManager.Instance.IsGameStarted())
        {
            RockManager.Instance.StartGame();
            Debug.Log("게임 시작!");
        }

        RockManager.Instance.StartSpawning();
        Debug.Log("바위 스폰 시작!");

        // 트리거 재사용을 원하지 않으면 비활성화
        hasActivated = false;
    }
}
