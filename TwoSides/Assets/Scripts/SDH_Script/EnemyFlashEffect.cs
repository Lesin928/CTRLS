using System.Collections;
using UnityEngine;

public class EnemyFlashEffect : MonoBehaviour
{
    [SerializeField] private Material flashMaterial; // 깜빡임 효과에 사용할 머티리얼
    [SerializeField] private float duration; // 깜빡임 지속 시간

    private SpriteRenderer spriteRenderer; // 깜빡임 효과를 적용할 스프라이트 렌더러
    private Material originalMaterial; // 원래 사용 중인 머티리얼 저장
    private Coroutine flashRoutine; // 현재 실행 중인 코루틴

    void Start()
    {
        // 현재 오브젝트의 SpriteRenderer 컴포넌트를 가져옴
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 원래 사용하던 머티리얼 저장 (깜빡임 효과 후 복원하기 위함)
        originalMaterial = spriteRenderer.material;
    }

    public void Flash()
    {
        // 만약 이미 깜빡임 코루틴이 실행 중이라면 중지
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        // 새로운 깜빡임 코루틴 실행 및 저장
        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        // 깜빡임 머티리얼로 변경
        spriteRenderer.material = flashMaterial;

        // 설정된 지속 시간만큼 대기
        yield return new WaitForSeconds(duration);

        // 원래 머티리얼로 복원
        spriteRenderer.material = originalMaterial;

        // 코루틴 실행 완료 표시
        flashRoutine = null;
    }
}