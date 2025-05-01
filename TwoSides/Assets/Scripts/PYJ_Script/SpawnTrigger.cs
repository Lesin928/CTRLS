using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    private bool hasActivated = false;
    private bool isPanelOpen = false;

    public GameObject interactionPanel; // �ν����Ϳ��� ����

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasActivated && other.CompareTag("Player"))
        {
            hasActivated = true;
            Debug.Log("�÷��̾ Ʈ���ſ� ���Խ��ϴ�. F Ű�� ��������.");
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
            // ù ��° F: �г� ����
            if (interactionPanel != null)
                interactionPanel.SetActive(true);
            isPanelOpen = true;
            Debug.Log("�г� ����. �ٽ� F�� ������ ������ ���۵˴ϴ�.");
        }
        else
        {
            // �� ��° F: ���� ����
            if (!RockManager.Instance) return;

            if (!RockManager.Instance.IsGameStarted())
            {
                RockManager.Instance.StartGame();
                Debug.Log("���� ����!");
            }

            RockManager.Instance.StartSpawning();

            isPanelOpen = false;
            if (interactionPanel != null)
                interactionPanel.SetActive(false);

            Debug.Log("FŰ �� ��° ����! ���� ���� ����!");
        }
    }
}


