using UnityEngine;

// TODO:
// FIXME:
// NOTE:

/// <summary>
/// 캐릭터 능력치 추상 클래스,
/// 모든 캐릭터는 이 클래스를 상속받아야 함 
/// </summary>
public abstract class CharacterObject : MonoBehaviour
{
    #region Character Info    
    [SerializeField] private float maxHp; //최대체력
    [SerializeField] private float currentHp; //현재 체력
    [SerializeField] private float armor; //방어력
    [SerializeField] private float attack; //공격력
    [SerializeField] private float attackSpeed; //공격속도 
    [SerializeField] private float moveSpeed; //이동속도
    [SerializeField] private float critical; //치명타 확률 
    [SerializeField] private float criticalDamage; //치명타 피해
    #endregion

    #region Setters and Getters
    ///<summary>
    ///최대체력 설정 및 반환 함수
    ///</summary>
    public virtual float MaxHp
    {
        get => maxHp;
        set
        {
            maxHp = value;
            currentHp = value; // 최대체력 설정 시 현재체력도 초기화
        }
    }

    ///<summary>
    ///현재체력 반환 함수
    ///</summary>
    public virtual float CurrentHp
    {
        get => currentHp;
        set => currentHp = value;
    }

    ///<summary>
    ///방어력 설정 및 반환 함수
    ///</summary>
    public virtual float Armor
    {
        get => armor;
        set => armor = value;
    }

    ///<summary>
    ///공격력 설정 및 반환 함수
    ///</summary>
    public virtual float Attack
    {
        get => attack;
        set => attack = value;
    }

    ///<summary>
    ///공격속도 설정 및 반환 함수
    ///</summary>
    public virtual float AttackSpeed
    {
        get => attackSpeed;
        set => attackSpeed = value;
    }

    ///<summary>
    ///이동속도 설정 및 반환 함수
    ///</summary>
    public virtual float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

    ///<summary>
    ///치명타 확률 설정 및 반환 함수
    ///</summary>
    public virtual float Critical
    {
        get => critical;
        set => critical = value;
    }

    ///<summary>
    ///치명타 피해 설정 및 반환 함수
    ///</summary>
    public virtual float CriticalDamage
    {
        get => criticalDamage;
        set => criticalDamage = value;
    }
    #endregion 

    /// <summary>    
    /// 피격 함수
    /// </summary>
    /// <param name="_damage"> 공격 주체가 주는 최종 데미지 </param> 
    public virtual void TakeDamage(float _damage)
    {
        //legacy code (현재는 사용하지 않음) 크리티컬 확률을 계산하여 데미지에 반영
        //if (UnityEngine.Random.Range(0f, 1f) < _critical)
        //{
        //_damage *= criticalDamage;
        //}
        //데미지 * 방어력 반감 계수 ( 데미지 / 데미지 + 아머) 
        CurrentHp -= (float)((Mathf.Pow(_damage, 2f) / ((double)Armor + (double)_damage)));

        //체력이 0 이하면 Die() 호출
        if (CurrentHp <= 0)
        {
            CurrentHp = 0;
            Die();
        }
    }

    /// <summary>    
    /// 해당 캐릭터가 죽었을 때 호출되는 함수
    /// </summary>
    protected abstract void Die();
}
