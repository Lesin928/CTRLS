using UnityEngine;

public class DoorManager : MonoBehaviour
{



    /// <summary>
    ///이 프로퍼티가 있어야 다른 스크립트에서 접근할 수 있습니다
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
            // 문 오브젝트 찾아서 인터랙션 켜주기
            GameObject door = GameObject.FindWithTag("Door");
            if (door != null && door.TryGetComponent<DoorInteract>(out var doorInteract))
            {
                doorInteract.EnableInteraction();
            }
        }
    }
}

