using UnityEngine;
using System.Collections;

// TODO: (�߰����� ���ºκ�)
// FIXME: (��ĥ�� ���ºκ�)
// NOTE : (��Ÿ �ۼ�)

/// <summary>
/// �÷��̾��� ���� ��ų �������� �����ϴ� Ŭ����
/// </summary>
public class DaggerSkill : MonoBehaviour
{    
    [SerializeField]
    private GameObject effect;
    private PlayerObject playerObject;

    private void Awake()
    {
        playerObject = GetComponentInParent<PlayerObject>();     
    }

    //���� ���ݽ� �浹�� ���Ϳ� ���� �浹ó�� 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.gameObject.GetComponent<EnemyObject>() != null)
            {
                collision.gameObject.GetComponent<EnemyObject>().TakeDamage(IsCritical());
            }
            else if (collision.gameObject.GetComponent<TestObject>() != null)
            {   //�׽�Ʈ�� �ڵ�
                collision.gameObject.GetComponent<TestObject>().TakeDamage(IsCritical());
            }
            //�б�
            PushAttack(collision);

            // �ǰ� ����Ʈ  
            HitPoint(collision);
        }
    }

    /// <summary>
    /// ġ��Ÿ ������ �����ϴ� �޼���
    /// </summary>
    private float IsCritical()
    {
        float finalDamage = playerObject.Attack * 1.4f; // �⺻ ������ * ��ų ���
        //ġ��Ÿ ����
        if (Random.Range(0f, 1f) < playerObject.Critical)
        {
            finalDamage *= playerObject.CriticalDamage;
        }
        return finalDamage;
    }

    /// <summary>
    /// �б� ó�� �޼���
    /// </summary>
    private void PushAttack(Collider2D collision)
    {
        //�б�
        if (collision.GetComponent<PushableObject>() != null)
        {
            Vector2 collisionDirection = (collision.transform.position - transform.position).normalized; // �浹 ���⺤��
            collision.gameObject.GetComponent<PushableObject>().Push(collisionDirection * playerObject.Attack * 0.2f); // ������ ��� �˹�
        }
    }

    /// <summary>
    /// �ǰ� ����Ʈ ��ġ�� ó���ϴ� �޼���
    /// </summary>
    private void HitPoint(Collider2D collision)
    {
        Vector3 effectPosition = collision.ClosestPoint(transform.position);
        //effectPosition.y = transform.position.y; // ���̸� ���� ��ü�� ���̷� ����  
        GameObject hit = Instantiate(effect, effectPosition, Quaternion.identity);
        //GameObject hit = Instantiate(effect, collision.gameObject.transform.position, gameObject.transform.rotation);
    }

}