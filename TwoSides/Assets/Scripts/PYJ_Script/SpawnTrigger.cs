using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    private bool hasActivated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasActivated && other.CompareTag("Player"))
        {
            hasActivated = true;
            Debug.Log("�÷��̾ Ʈ���ſ� ���Խ��ϴ�. F Ű�� ������ ������ ���۵˴ϴ�.");
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
            Debug.Log("���� ����!");
        }

        RockManager.Instance.StartSpawning();
        Debug.Log("���� ���� ����!");

        // Ʈ���� ������ ������ ������ ��Ȱ��ȭ
        hasActivated = false;
    }
}
