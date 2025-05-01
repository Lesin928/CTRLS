using UnityEngine;

// TODO:
// FIXME:
// NOTE:

/// <summary>
/// ĳ���� �ɷ�ġ �߻� Ŭ����,
/// ��� ĳ���ʹ� �� Ŭ������ ��ӹ޾ƾ� �� 
/// </summary>
public abstract class CharacterObject : MonoBehaviour
{
    #region Character Info
    [SerializeField]
    protected float maxHp; //�ִ�ü��
    protected float currentHp; //���� ü��
    protected float armor; //����
    protected float attack; //���ݷ�
    protected float attackSpeed; //���ݼӵ� 
    protected float moveSpeed; //�̵��ӵ�
    protected float critical; //ġ��Ÿ Ȯ�� 
    protected float criticalDamage; //ġ��Ÿ ����
    #endregion

    #region Setters and Getters
    ///<summary>
    ///�ִ�ü�� ���� �� ��ȯ �Լ�
    ///</summary>
    public virtual float MaxHp
    {
        get => maxHp;
        set
        {
            maxHp = value;
            currentHp = value; // �ִ�ü�� ���� �� ����ü�µ� �ʱ�ȭ
        }
    }

    ///<summary>
    ///����ü�� ��ȯ �Լ�
    ///</summary>
    public virtual float CurrentHp
    {
        get => currentHp;
        set => currentHp = value;
    }

    ///<summary>
    ///���� ���� �� ��ȯ �Լ�
    ///</summary>
    public virtual float Armor
    {
        get => armor;
        set => armor = value;
    }

    ///<summary>
    ///���ݷ� ���� �� ��ȯ �Լ�
    ///</summary>
    public virtual float Attack
    {
        get => attack;
        set => attack = value;
    }

    ///<summary>
    ///���ݼӵ� ���� �� ��ȯ �Լ�
    ///</summary>
    public virtual float AttackSpeed
    {
        get => attackSpeed;
        set => attackSpeed = value;
    }

    ///<summary>
    ///�̵��ӵ� ���� �� ��ȯ �Լ�
    ///</summary>
    public virtual float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

    ///<summary>
    ///ġ��Ÿ Ȯ�� ���� �� ��ȯ �Լ�
    ///</summary>
    public virtual float Critical
    {
        get => critical;
        set => critical = value;
    }

    ///<summary>
    ///ġ��Ÿ ���� ���� �� ��ȯ �Լ�
    ///</summary>
    public virtual float CriticalDamage
    {
        get => criticalDamage;
        set => criticalDamage = value;
    }
    #endregion 

    /// <summary>    
    /// �ǰ� �Լ�
    /// </summary>
    /// <param name="_damage"> ���� ��ü�� �ִ� ���� ������ </param> 
    public virtual void TakeDamage(float _damage) 
    {
        //legacy code (����� ������� ����) ũ��Ƽ�� Ȯ���� ����Ͽ� �������� �ݿ�
        //if (UnityEngine.Random.Range(0f, 1f) < _critical)
        //{
            //_damage *= criticalDamage;
        //}
        
        currentHp -= (float)((double)((Mathf.Pow(_damage, 2f)) / ((double)armor) + (double)_damage));

        //ü���� 0 ���ϸ� Die() ȣ��
        if (currentHp <= 0)
        {
            currentHp = 0;
            Die();
        }
    }

    /// <summary>    
    /// �ش� ĳ���Ͱ� �׾��� �� ȣ��Ǵ� �Լ�
    /// </summary>
    protected abstract void Die();

}
