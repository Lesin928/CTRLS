using UnityEngine;
using System.Collections;
// TODO: (추가할일 적는부분)
// FIXME: (고칠거 적는부분)
// NOTE : (기타 작성)

/// <summary>
/// 플레이어 피격 애니메이션을 관리하는 클래스
/// </summary>
public class HitAnim : MonoBehaviour
{ 
    [SerializeField] private SpriteRenderer spriteRenderer; 
    [SerializeField] private int flashCount = 4; 
    [SerializeField] private float maxDuration = 0.2f;
    [SerializeField] private float minDuration = 0.05f;

    /// <summary>
    /// 피격 애니메이션을 시작하는 메서드
    /// </summary>
    public void Flash()
    {
        StartCoroutine(FlashCoroutine());
    }

    /// <summary>
    /// 플래시 효과를 구현하는 코루틴
    /// 선형 감소하는 간격으로 스프라이트 렌더러의 활성화 상태를 변경
    /// 깜빡인다는 뜻
    /// </summary>
    private IEnumerator FlashCoroutine()
    {
        for (int i = 0; i < flashCount; i++)
        {
            // 점점 줄어드는 간격 계산 (선형 감소)
            float t = (float)i / (flashCount - 1);  
            float duration = Mathf.Lerp(maxDuration, minDuration, t);

            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(duration);

            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(duration);
        }
    }
}
