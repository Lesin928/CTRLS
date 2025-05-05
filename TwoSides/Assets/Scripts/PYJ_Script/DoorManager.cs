using UnityEngine;

public class DoorManager : MonoBehaviour
{



    /// <summary>
    ///�� ������Ƽ�� �־�� �ٸ� ��ũ��Ʈ���� ������ �� �ֽ��ϴ�
    /// </summary>

    public static DoorManager Instance { get; private set; }



    public bool IsGameCleared { get; private set; } = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }





    public void SetGameCleared(bool cleared)
    {
        IsGameCleared = cleared;

        if (cleared)
        {
            // �� ������Ʈ ã�Ƽ� ���ͷ��� ���ֱ�
            GameObject door = GameObject.FindWithTag("Door");
            if (door != null && door.TryGetComponent<DoorInteract>(out var doorInteract))
            {
                doorInteract.EnableInteraction();
            }
        }
    }
}

