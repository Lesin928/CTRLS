using UnityEngine;

public class DaggerAttack : MonoBehaviour
{
    public PlayerObject playerObject { get; private set; }
    private void Awake()
    {
        playerObject = GetComponent<PlayerObject>();
    } 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            float finalDamage = playerObject.Attack;
            if (UnityEngine.Random.Range(0f, 1f) < playerObject.Critical)
            {
                finalDamage *= playerObject.CriticalDamage;
            }
            collision.gameObject.GetComponent<EnemyObject>().TakeDamage(finalDamage);
            if(collision.GetComponent<PushableObject>() != null)
            {
                Vector2 collisionDirection = (collision.transform.position - transform.position).normalized; // √Êµπ πÊ«‚∫§≈Õ
                collision.gameObject.GetComponent<PushableObject>().Push(collisionDirection * playerObject.Attack * 0.2f ); // µ•πÃ¡ˆ ∫Ò∑  ≥ÀπÈ
            }
            //««∞› ¿Ã∆Â∆Æ
            //GameObject go = Instantiate(effect, collision.gameObject.transform.position, gameObject.transform.rotation);
        }
    } 
}