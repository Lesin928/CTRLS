using UnityEngine;

// TODO: (�߰����� ���ºκ�)
// FIXME: (��ĥ�� ���ºκ�)
// NOTE : (��Ÿ �ۼ�)

/// <summary>
/// �׽�Ʈ ���ʹ̸� �����ϴ� Ŭ����
/// </summary>
public class TestObject : CharacterObject
{ 
    private void Awake()
    {   
        MaxHp = 10000f; // �ִ� ü��
    }


    /// <summary>
    /// �׽�Ʈ ���ʹ��� �ǰ� ó��
    /// </summary>
    public override void TakeDamage(float damage)     
    {
        base.TakeDamage(damage);
        Debug.Log($"{damage}��ŭ ����");
    }

    /// <summary>
    /// �׽�Ʈ ���ʹ��� ��� ó��
    /// </summary>
    protected override void Die()
    { 
        Debug.Log("����");
    }

}
