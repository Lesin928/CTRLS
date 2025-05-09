using UnityEngine;

public class StoreInteract : Interactive
{
    public GameObject go;
    public StoreManager sm;
    private Collider2D storeCollider;

    private void Start()
    {
        sm=go.GetComponent<StoreManager>();
        storeCollider = GetComponent<Collider2D>();
        if (storeCollider == null)
        {
            Debug.LogError("Collider2D 컴포넌트가 없습니다!");
        }

        storeCollider.enabled = false; // 시작 시 콜라이더 비활성화
    }

    private void Update()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager 인스턴스가 null입니다!");
            return;
        }

        if (storeCollider == null)
        {
            Debug.LogError("doorCollider가 null입니다!");
            return;
        }

        if (GameManager.Instance.isClear && !storeCollider.enabled)
        {
            storeCollider.enabled = true;
            Debug.Log("스토어 콜라이더 활성화");
        }
    }


    public override void PressF()
    {
        sm.OpenStore();
        //// GameManager.Instance.isClear 값 확인
        //Debug.Log("PressF 호출 시 isClear 값: " + GameManager.Instance.isClear);

        //if (GameManager.Instance.isClear)
        //{
        //    Debug.Log("문 개방! 다음 맵으로 이동합니다.");//지도 열리게 
        //    Mapbutton.Instance.activeButton = true;
        //    Mapbutton.Instance.GameClearAutoButton();
        //}
        //else
        //{
        //    Debug.Log("몬스터가 아직 남았다...");//아직 못함
        //}
    }
}