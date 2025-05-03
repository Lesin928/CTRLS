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

            //치명타 판정
            if (UnityEngine.Random.Range(0f, 1f) < playerObject.Critical)
            {
                finalDamage *= playerObject.CriticalDamage;
            }

            //데미지 적용            
            //collision.gameObject.GetComponent<EnemyObject>().TakeDamage(finalDamage);
            //테스트 코드
            collision.gameObject.GetComponent<TestObject>().TakeDamage(finalDamage);

            //밀기
            if (collision.GetComponent<PushableObject>() != null)
            {
                Vector2 collisionDirection = (collision.transform.position - transform.position).normalized; // 충돌 방향벡터
                collision.gameObject.GetComponent<PushableObject>().Push(collisionDirection * playerObject.Attack * 0.2f ); // 데미지 비례 넉백
            }          

            // 피격 이펙트  
            Vector3 effectPosition = collision.ClosestPoint(transform.position);
            //effectPosition.y = transform.position.y; // 높이를 공격 주체의 높이로 설정  
            GameObject hit = Instantiate(effect, effectPosition, Quaternion.identity);
            //GameObject hit = Instantiate(effect, collision.gameObject.transform.position, gameObject.transform.rotation);
        }
    } 
}