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
    [SerializeField] private float maxHp; //�ִ�ü��
    [SerializeField] private float currentHp; //���� ü��
    [SerializeField] private float armor; //����
    [SerializeField] private float attack; //���ݷ�
    [SerializeField] private float attackSpeed; //���ݼӵ� 
    [SerializeField] private float moveSpeed; //�̵��ӵ�
    [SerializeField] private float critical; //ġ��Ÿ Ȯ�� 
    [SerializeField] private float criticalDamage; //ġ��Ÿ ����
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
        //������ * ���� �ݰ� ��� ( ������ / ������ + �Ƹ�) 
        CurrentHp -= (float)((Mathf.Pow(_damage, 2f) / ((double)Armor + (double)_damage)));

        //ü���� 0 ���ϸ� Die() ȣ��
        if (CurrentHp <= 0)
        {
            CurrentHp = 0;
            Die();
        }
    }

    /// <summary>    
    /// �ش� ĳ���Ͱ� �׾��� �� ȣ��Ǵ� �Լ�
    /// </summary>
    protected abstract void Die();
}
