using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    private bool hasActivated = false;
    private bool canStartSpawning = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasActivated && other.CompareTag("Player"))
        {
            hasActivated = true;
            canStartSpawning = true;
            Debug.Log("충돌 완료! 엔터 키를 누르면 바위가 떨어집니다.");
        }
    }

    private void Update()
    {
        if (canStartSpawning && Input.GetKeyDown(KeyCode.F))
        {
            if (!RockManager.Instance) return;

            if (!RockManager.Instance.IsGameStarted())  // 게임 시작 상태 체크
            {
                RockManager.Instance.StartGame(); // 자동으로 게임 시작
                Debug.Log("게임 시작!");
            }

            RockManager.Instance.StartSpawning();
            canStartSpawning = false;
            Debug.Log("엔터 키 눌림! 바위 떨어지기 시작!");
        }
    }
}


