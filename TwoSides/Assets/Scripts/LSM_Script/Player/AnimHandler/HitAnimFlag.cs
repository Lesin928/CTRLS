using UnityEngine;

// TODO: (추가할일 적는부분)
// FIXME: (고칠거 적는부분)
// NOTE : (기타 작성)

/// <summary>
/// 피격 이펙트 애니메이션의 이벤트 헨들러
/// </summary>
public class HitAnimFlag : MonoBehaviour
{
    /// <summary>
    /// 이펙트가 끝나면 지우는 메서드
    /// 무조건 애니메이션 이벤트로 사용하여야 함.
    /// </summary>
    public void AnimFinished()
    {
        Destroy(gameObject);
    }
}

