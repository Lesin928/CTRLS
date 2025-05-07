using UnityEngine;

public class InteractiveAble : MonoBehaviour
{
    public GameObject F;

    //해당 오브젝트가 활성화 되었을 때
    private void OnEnable()
    {
        // F키 이미지 숨김
        F.SetActive(false);
        // 현재 플레이어 오브젝트와 충돌중일 때
        if (Physics2D.OverlapCircle(transform.position, 0.1f, LayerMask.GetMask("Player")) != null)
        {
            // F키 이미지 띄움
            F.SetActive(true);
        }
    }
    private void OnDisable()
    {
        // F키 이미지 숨김
        F.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌중일 때
        if (collision.CompareTag("Player"))
        {
            // F키 이미지 띄움
            F.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 플레이어와 충돌이 끝났을 때
        if (collision.CompareTag("Player"))
        {
            // F키 이미지 숨김
            F.SetActive(false);
        }
    }
}
