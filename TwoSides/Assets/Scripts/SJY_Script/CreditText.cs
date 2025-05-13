using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//앤딩크래딧 텍스트 스크립트
public class CreditText : MonoBehaviour
{
    public RectTransform uiImage;
    public Vector2 targetPosition;
    public float speed = 200f;
    public GameObject fadeCanvasPrefab;

    private bool isFaded = false;

    void Update()
    {
        //targetPosition으로 이동
        uiImage.anchoredPosition = Vector2.MoveTowards(
            uiImage.anchoredPosition,
            targetPosition,
            speed * Time.deltaTime
        );

        //targetPosition에 도달하면 FadeOut 호출
        if (!isFaded && uiImage.anchoredPosition == targetPosition)
        {
            isFaded = true;
            StartCoroutine(CallFadeOut());
        }
    }

    IEnumerator CallFadeOut()
    {
        FadeController fade = fadeCanvasPrefab.GetComponent<FadeController>();

        yield return StartCoroutine(fade.FadeOut());
        //페이드아웃동안 다른 스크립트가 실행되지 안게 대기기
        yield return new WaitForSeconds(1.5f);
    }
}