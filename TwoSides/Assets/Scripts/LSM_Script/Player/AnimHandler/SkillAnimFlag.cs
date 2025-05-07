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
    /// 스킬이 내려찍을 때 수행되는 매서드
    /// </summary>
    public void Strike()
    { 
        if (!playerObject.skillCollider.activeSelf)
        {
            playerObject.skillCollider.SetActive(true);
        }
    } 

    /// <summary>
    /// 내려찍기가 끝날 때 수행되는 메서드
    /// </summary>
    public void StrikeFinished()
    {
        if (playerObject.skillCollider.activeSelf)
        {
            playerObject.skillCollider.SetActive(false);
        }
        playerObject.IsSkill = false;

    }
}
