using UnityEngine;

// TODO:
// FIXME:
// NOTE:

/// <summary>
/// 캐릭터 능력치 추상 클래스,
/// 모든 캐릭터는 이 클래스를 상속받아야 함 
/// </summary>
public abstract class Character_Stat : MonoBehaviour
{
    public float maxHp; //최대체력
    public float currentHp; //현재 체력
    public float armor; //방어력
    public float attack; //공격력
    public float attackSpeed; //공격속도 
    public float moveSpeed; //이동속도
    public float critical; //치명타 확률 
    public float criticalDamage; //치명타 피해

    ///<summary>
    ///최대 체력을 설정 함수
    ///</summary>
    public virtual void SetHp(float hp)
    {
        this.maxHp = hp;
        this.currentHp = hp;
    }
    ///<summary>
    ///현재 체력을 리턴하는 함수
    ///</summary>
    public virtual float GetHp()
    {
        return currentHp;
    } 

    /// <summary>    
    /// 치명타 판정 이후 방어력만큼 피해를 경감하여 데미지 받으며, 몬스터는 치명타가 발생하지 않음.
    /// </summary>
    /// <param name="damage"> 공격 주체가 주는 최종데미지 </param>
    public abstract void TakeDamage(float damage);

}
