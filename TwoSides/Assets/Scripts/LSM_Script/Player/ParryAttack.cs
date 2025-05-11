using UnityEngine;
using System.Collections;

// TODO: (추가할일 적는부분)
// FIXME: (고칠거 적는부분)
// NOTE : (기타 작성)

/// <summary>
/// 플레이어의 근접 공격 데미지를 관리하는 클래스
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

    //근접 공격시 충돌한 몬스터에 대한 충돌처리 
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
            {   //테스트용 코드
                collision.gameObject.GetComponent<TestObject>().TakeDamage(IsCritical());
            }
            //밀기
            PushAttack(collision);

            // 피격 이펙트  
            HitPoint(collision);
        }

        if (collision.CompareTag("Effect"))
        { 
            Destroy(collision.gameObject); // 충돌한 이펙트 삭제
            // 피격 이펙트  
            HitPoint(collision);
        }
    }

    /// <summary>
    /// 치명타 판정을 수행하는 메서드
    /// </summary>
    private float IsCritical()
    {
        float finalDamage = playerObject.Attack * 2f; // 기본 데미지 * 패링 계수
        //치명타 판정
        if (Random.Range(0f, 1f) < playerObject.Critical)
        {
            finalDamage *= playerObject.CriticalDamage;
        }
        return finalDamage;
    }

    /// <summary>
    /// 밀기 처리 메서드
    /// </summary>
    private void PushAttack(Collider2D collision)
    {
        //밀기
        if (collision.GetComponent<PushableObject>() != null)
        {
            Vector2 collisionDirection = (collision.transform.position - transform.position).normalized; // 충돌 방향벡터
            collision.gameObject.GetComponent<PushableObject>().Push(collisionDirection * playerObject.Attack * 0.2f); // 데미지 비례 넉백
        }
    } 
    /// <summary>
    /// 피격 이펙트 위치를 처리하는 메서드
    /// </summary>
    private void HitPoint(Collider2D collision)
    {
        // 피격 이펙트 위치 collision 위치로 설정
        GameObject hit = Instantiate(hiteffect, collision.gameObject.transform.position, Quaternion.identity);
        //만약 태그가 effect면 크기 1/2
        if (collision.CompareTag("Effect"))
        {
            hit.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        } 
    }

}