using UnityEngine;

/// <summary>
/// 적의 Slash 공격 이펙트를 처리하는 클래스입니다.
/// 애니메이션 이벤트를 통해 활성화되며, 플레이어와 충돌 시 처리 로직을 수행합니다.
/// </summary>
public class Slash : MonoBehaviour
{
    /// <summary>
    /// 애니메이션 이벤트에서 호출되어, 적의 바라보는 방향에 맞춰 이펙트 방향을 설정합니다.
    /// </summary>
    /// <param name="facingDir">적이 바라보는 방향 (1 또는 -1)</param>
    public void Active(int facingDir)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * facingDir;
        transform.localScale = scale;
    }

    // 플레이어와 충돌 시 호출
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerObject player = collision.GetComponent<PlayerObject>();
            if (player != null)
            {
                Debug.Log("공격 성공");
            }
        }
    }

    // 애니메이션 이벤트에서 호출되어 이펙트 오브젝트를 파괴
    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
