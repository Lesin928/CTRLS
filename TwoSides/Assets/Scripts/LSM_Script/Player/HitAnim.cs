using UnityEngine;

public class HitAnim : MonoBehaviour
{ 
    [SerializeField] private SpriteRenderer spriteRenderer; 
    [SerializeField] private int flashCount = 4; 
    [SerializeField] private float maxDuration = 0.2f;
    [SerializeField] private float minDuration = 0.05f;

    public void Flash()
    {
        StartCoroutine(FlashCoroutine());
    } 
    private System.Collections.IEnumerator FlashCoroutine()
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
