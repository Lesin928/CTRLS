using UnityEngine;

public class DoorInteract : Interactive
{
    private Collider2D doorCollider;

    private void Start()
    {
        doorCollider = GetComponent<Collider2D>();
        if (doorCollider == null)
        {
            Debug.LogError("Collider2D 컴포넌트가 없습니다!");
        }

        doorCollider.enabled = false; // 시작 시 콜라이더 비활성화
    }

    private void Update()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager 인스턴스가 null입니다!");
            return;
        }

        if (doorCollider == null)
        {
            Debug.LogError("doorCollider가 null입니다!");
            return;
        }

        if (GameManager.Instance.isClear && !doorCollider.enabled)
        {
            doorCollider.enabled = true;
            Debug.Log("클리어 상태 감지됨 → 문 콜라이더 활성화");
        }
    }


    public override void PressF()
    {
        // GameManager.Instance.isClear 값 확인
        Debug.Log("PressF 호출 시 isClear 값: " + GameManager.Instance.isClear);

        if (GameManager.Instance.isClear)
        {
            Debug.Log("문 개방! 다음 맵으로 이동합니다.");//지도 열리게 
            Mapbutton.Instance.activeButton = true;
            Map.Instance.doorConnected = true;

            if (Map.Instance.LEVEL == 16)
                GameManager.Instance.OnMonsterDead();
            else
                Mapbutton.Instance.GameClearAutoButton();
        }
        else
        {
            Debug.Log("몬스터가 아직 남았다...");//아직 못함
        }
    }
}