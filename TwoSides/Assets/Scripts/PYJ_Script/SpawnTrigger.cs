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
            Debug.Log("�浹 �Ϸ�! ���� Ű�� ������ ������ �������ϴ�.");
        }
    }

    private void Update()
    {
        if (canStartSpawning && Input.GetKeyDown(KeyCode.F))
        {
            if (!RockManager.Instance) return;

            if (!RockManager.Instance.IsGameStarted())  // ���� ���� ���� üũ
            {
                RockManager.Instance.StartGame(); // �ڵ����� ���� ����
                Debug.Log("���� ����!");
            }

            RockManager.Instance.StartSpawning();
            canStartSpawning = false;
            Debug.Log("���� Ű ����! ���� �������� ����!");
        }
    }
}


