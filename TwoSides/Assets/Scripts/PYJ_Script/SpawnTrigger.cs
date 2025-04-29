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
        if (canStartSpawning && Input.GetKeyDown(KeyCode.Return))
        {
            if (!GameManager1.Instance) return;

            if (!GameManager1.Instance.IsGameStarted())  // ���� ���� ���� üũ
            {
                GameManager1.Instance.StartGame(); // �ڵ����� ���� ����
                Debug.Log("���� ����!");
            }

            GameManager1.Instance.StartSpawning();
            canStartSpawning = false;
            Debug.Log("���� Ű ����! ���� �������� ����!");
        }
    }
}


