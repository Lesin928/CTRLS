using UnityEngine;

// TODO:
// FIXME:
// NOTE:

/// <summary>
/// ĳ���� �ɷ�ġ �߻� Ŭ����,
/// ��� ĳ���ʹ� �� Ŭ������ ��ӹ޾ƾ� �� 
/// </summary>
public abstract class Character_Stat : MonoBehaviour
{
    public float maxHp; //�ִ�ü��
    public float currentHp; //���� ü��
    public float armor; //����
    public float attack; //���ݷ�
    public float attackSpeed; //���ݼӵ� 
    public float moveSpeed; //�̵��ӵ�
    public float critical; //ġ��Ÿ Ȯ�� 
    public float criticalDamage; //ġ��Ÿ ����

    ///<summary>
    ///�ִ� ü���� ���� �Լ�
    ///</summary>
    public virtual void SetHp(float hp)
    {
        this.maxHp = hp;
        this.currentHp = hp;
    }
    ///<summary>
    ///���� ü���� �����ϴ� �Լ�
    ///</summary>
    public virtual float GetHp()
    {
        return currentHp;
    } 

    /// <summary>    
    /// ġ��Ÿ ���� ���� ���¸�ŭ ���ظ� �氨�Ͽ� ������ ������, ���ʹ� ġ��Ÿ�� �߻����� ����.
    /// </summary>
    /// <param name="damage"> ���� ��ü�� �ִ� ���������� </param>
    public abstract void TakeDamage(float damage);

}
