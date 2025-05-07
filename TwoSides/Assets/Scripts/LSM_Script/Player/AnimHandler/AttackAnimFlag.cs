using UnityEngine;

// TODO: (추가할일 적는부분)
// FIXME: (고칠거 적는부분)
// NOTE : (기타 작성)

/// <summary>
/// 공격 이펙트 애니메이션의 이벤트 헨들러
/// </summary>
public class AttackAnimFlag : MonoBehaviour
{
    public PlayerObject playerObject { get; private set; }
    private void Awake()
    {
        playerObject = GetComponentInParent<PlayerObject>();
    }

    /// <summary>
    /// 첫 번째 공격이 끝나면 메시지 전달하는 메서드
    /// 무조건 애니메이션 이벤트로 사용하여야 함.
    /// </summary>
    public void FirstAttackFinished()
    {
        if (playerObject.attackCollider1.activeSelf)
        {
            playerObject.attackCollider1.SetActive(false);
        }
        playerObject.EndAttack = true;
    }

    /// <summary>
    /// 두 번째 공격이 끝나면 메시지 전달하는 메서드
    /// 무조건 애니메이션 이벤트로 사용하여야 함.
    /// </summary>
    public void SecondAttackFinished()
    {
        if (playerObject.attackCollider2.activeSelf)
        { 
            playerObject.attackCollider2.SetActive(false); 
        }
          
    }
}
