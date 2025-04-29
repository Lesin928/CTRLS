using UnityEngine;

public class Rock : MonoBehaviour
{
    public float fallSpeed = 5f;

    /// <summary>
    /// ������ ������ �Ʒ��� ����߸���. Ư�� ��ġ�� �Ѿ�� destroy�Ѵ�.
    /// </summary>
    void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }


    /// <summary>
    /// (Player�±� ����) �÷��̾�� �浹�ϸ� TakeDamage()�Լ�ȣ��,����destroy.
    /// </summary>

    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager1.Instance.TakeDamage();
            Destroy(gameObject);
        }
    }
}
