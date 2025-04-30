using UnityEngine;

public class OpenPanelOnInteract : MonoBehaviour
{
    // 플레이어가 상호작용할 수 있는 거리
    public float interactionDistance = 3f;

    // UI 패널 오브젝트 (유니티 에디터에서 할당)
    public GameObject panelToOpen;

    // 플레이어의 Transform (주로 MainCamera나 플레이어 본체)
    public Transform player;

    // 내부 상태: 현재 패널이 열려 있는지 여부
    private bool isPanelOpen = false;

    void Start()
    {
        // 시작할 때 패널은 꺼진 상태
        if (panelToOpen != null)
        {
            panelToOpen.SetActive(false);
        }
    }

    void Update()
    {
        // 플레이어와 이 오브젝트 사이 거리 계산
        float distance = Vector3.Distance(player.position, transform.position);

        // 거리가 상호작용 거리 이내일 때
        if (distance <= interactionDistance)
        {
            // F 키를 눌렀는지 확인
            if (Input.GetKeyDown(KeyCode.F))
            {
                TogglePanel();
            }
        }
    }

    // 패널 열기/닫기 토글 함수
    void TogglePanel()
    {
        if (panelToOpen != null)
        {
            isPanelOpen = !isPanelOpen;
            panelToOpen.SetActive(isPanelOpen);
        }
    }
}
