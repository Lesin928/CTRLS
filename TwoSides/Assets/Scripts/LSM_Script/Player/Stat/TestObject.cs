using UnityEngine;

// TODO: (추가할일 적는부분)
// FIXME: (고칠거 적는부분)
// NOTE : (기타 작성)

/// <summary>
/// 테스트 에너미를 정의하는 클래스
/// </summary>
public class TestObject : CharacterObject
{ 
    private void Awake()
    {   
        MaxHp = 10000f; // 최대 체력
    }


    /// <summary>
    /// 테스트 에너미의 피격 처리
    /// </summary>
    public override void TakeDamage(float damage)     
    {
        base.TakeDamage(damage);
        Debug.Log($"{damage}만큼 아픔");
    }

    /// <summary>
    /// 테스트 에너미의 사망 처리
    /// </summary>
    protected override void Die()
    { 
        Debug.Log("죽음");
    }

}
