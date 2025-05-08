using UnityEngine;

public class Moving_Thin_Ground : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;     // ��鸲 �ӵ� (��: 1 ~ 3)
    [SerializeField] private float moveAmount = 0.5f;  // ��鸲 �Ÿ� (��: 0.2 ~ 1)

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
        // �ε巴�� �¿� �̵�
        float offsetX = Mathf.Sin(Time.time * moveSpeed) * moveAmount;
        Vector3 newPosition = new Vector3(startPosition.x + offsetX, startPosition.y, startPosition.z);

        // �̵��� ���
        Vector3 delta = newPosition - previousPosition;

        // �̵� ����
        transform.position = newPosition;

        // �÷��̾ ���� ������ ���� �Ÿ���ŭ �̵�
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

