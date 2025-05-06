using UnityEngine;

// TODO: (추가할일 적는부분)
// FIXME: (고칠거 적는부분)
// NOTE : (기타 작성)

/// <summary>
/// 스킬 애니메이션의 이벤트 헨들러
/// </summary>
public class SkillAnimFlag : MonoBehaviour
{ 
    public PlayerObject playerObject { get; private set; }
    private void Awake()
    {
        playerObject = GetComponentInParent<PlayerObject>();
    }
    /// <summary>
    /// 스킬 사용이 끝나면 메시지를 전달하는 메서드
    /// 무조건 애니메이션 이벤트로 사용하여야 함.
    /// </summary>
    public void SkillkFinished()
    { 
        playerObject.IsSkill = false;
    } 
}
