using UnityEngine;

// TODO: (�߰����� ���ºκ�)
// FIXME: (��ĥ�� ���ºκ�)
// NOTE : (��Ÿ �ۼ�)

/// <summary>
/// �÷��̾��� �Ӽ��� �����ϴ� Ŭ����
/// </summary>
public class TestObject : CharacterObject
{ 
    private void Awake()
    {   
        MaxHp = 10000f; // �ִ� ü��
    }  

    public override void TakeDamage(float damage)     
    {
        base.TakeDamage(damage);
        Debug.Log($"{damage}��ŭ ����");
    }

    protected override void Die()
    { 
        Debug.Log("����");
    }

}
