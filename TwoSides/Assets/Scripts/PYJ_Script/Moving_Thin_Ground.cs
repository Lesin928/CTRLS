using UnityEngine;

public class Moving_Thin_Ground : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;     // 흔들림 속도 (예: 1 ~ 3)
    [SerializeField] private float moveAmount = 0.5f;  // 흔들림 거리 (예: 0.2 ~ 1)

    private Vector3 startPosition;
    private Vector3 previousPosition;
    private Transform playerOnPlatform;

    void Start()
    {
        startPosition = transform.position;
        previousPosition = startPosition;
    }

    void Update()
    {
        // 부드럽게 좌우 이동
        float offsetX = Mathf.Sin(Time.time * moveSpeed) * moveAmount;
        Vector3 newPosition = new Vector3(startPosition.x + offsetX, startPosition.y, startPosition.z);

        // 이동량 계산
        Vector3 delta = newPosition - previousPosition;

        // 이동 적용
        transform.position = newPosition;

        // 플레이어가 위에 있으면 같은 거리만큼 이동
        if (playerOnPlatform != null)
        {
            playerOnPlatform.position += delta;
        }

        previousPosition = newPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerOnPlatform = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (collision.transform == playerOnPlatform)
                playerOnPlatform = null;
        }
    }
}

