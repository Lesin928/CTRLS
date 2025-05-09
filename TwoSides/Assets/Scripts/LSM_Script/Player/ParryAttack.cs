using UnityEngine;
using System.Collections;

// TODO: (�߰����� ���ºκ�)
// FIXME: (��ĥ�� ���ºκ�)
// NOTE : (��Ÿ �ۼ�)

/// <summary>
/// �÷��̾��� ���� ���� �������� �����ϴ� Ŭ����
/// </summary>
public class ParryAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject hiteffect;
    [SerializeField]
    private GameObject parryeffect;
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
            GameObject parry = Instantiate(parryeffect, transform.position, Quaternion.identity);

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

        if (collision.CompareTag("Effect"))
        { 
            Destroy(collision.gameObject); // �浹�� ����Ʈ ����
            // �ǰ� ����Ʈ  
            HitPoint(collision);
        }
    }

    /// <summary>
    /// ġ��Ÿ ������ �����ϴ� �޼���
    /// </summary>
    private float IsCritical()
    {
        float finalDamage = playerObject.Attack; // �⺻ ������
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
        // �ǰ� ����Ʈ ��ġ collision ��ġ�� ����
        GameObject hit = Instantiate(hiteffect, collision.gameObject.transform.position, Quaternion.identity);
        //���� �±װ� effect�� ũ�� 1/2
        if (collision.CompareTag("Effect"))
        {
            hit.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        } 
    }

}