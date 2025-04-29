using UnityEngine;

public class Rock : MonoBehaviour
{
    public float fallSpeed = 5f;

    /// <summary>
    /// 바위를 위에서 아래로 떨어뜨린다. 특정 위치가 넘어가면 destroy한다.
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
    /// (Player태그 붙은) 플레이어와 충돌하면 TakeDamage()함수호출,바위destroy.
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
