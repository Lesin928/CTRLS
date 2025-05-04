using UnityEngine;

// TODO: (추가할일 적는부분)
// FIXME: (고칠거 적는부분)
// NOTE : (기타 작성)

/// <summary>
/// 플레이어의 속성을 관리하는 클래스
/// </summary>
public class TestObject : CharacterObject
{ 
    private void Awake()
    {   
        MaxHp = 10000f; // 최대 체력
    }  

    public override void TakeDamage(float damage)     
    {
        base.TakeDamage(damage);
        Debug.Log($"{damage}만큼 아픔");
    }

    protected override void Die()
    { 
        Debug.Log("죽음");
    }

}
