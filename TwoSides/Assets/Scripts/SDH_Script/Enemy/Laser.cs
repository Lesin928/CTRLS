using UnityEngine;

public class Laser : MonoBehaviour
{
    public void Shoot(int facingDir)
    {
        Vector3 scale = transform.localScale;
        scale.y = Mathf.Abs(scale.y) * -facingDir;
        transform.localScale = scale;

        // Z 축 회전값을 90도로 설정
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("공격 성공");
            }
        }
    }

    private void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
