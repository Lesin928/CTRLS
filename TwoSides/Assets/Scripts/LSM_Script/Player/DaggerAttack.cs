using UnityEngine;

public class DaggerAttack : MonoBehaviour
{
    
    [SerializeField]
    private GameObject effect;
    private PlayerObject playerObject;

    private void Awake()
    {
        playerObject = GetComponentInParent<PlayerObject>();     
    } 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            float finalDamage = playerObject.Attack;

            //ġ��Ÿ ����
            if (UnityEngine.Random.Range(0f, 1f) < playerObject.Critical)
            {
                finalDamage *= playerObject.CriticalDamage;
            }

            //������ ����            
            //collision.gameObject.GetComponent<EnemyObject>().TakeDamage(finalDamage);
            //�׽�Ʈ �ڵ�
            collision.gameObject.GetComponent<TestObject>().TakeDamage(finalDamage);

            //�б�
            if (collision.GetComponent<PushableObject>() != null)
            {
                Vector2 collisionDirection = (collision.transform.position - transform.position).normalized; // �浹 ���⺤��
                collision.gameObject.GetComponent<PushableObject>().Push(collisionDirection * playerObject.Attack * 0.2f ); // ������ ��� �˹�
            }          

            // �ǰ� ����Ʈ  
            Vector3 effectPosition = collision.ClosestPoint(transform.position);
            //effectPosition.y = transform.position.y; // ���̸� ���� ��ü�� ���̷� ����  
            GameObject hit = Instantiate(effect, effectPosition, Quaternion.identity);
            //GameObject hit = Instantiate(effect, collision.gameObject.transform.position, gameObject.transform.rotation);
        }
    } 
}