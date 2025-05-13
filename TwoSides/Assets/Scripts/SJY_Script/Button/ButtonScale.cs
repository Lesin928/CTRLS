using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// 버튼 클릭시 텍스트 크기를 애니메이션으로 변경하는 스크립트
public class ButtonScale : MonoBehaviour
{
    public Button button;
    public Text buttonText;
    public float scaleDuration = 0.1f;
    public float minScale = 0.8f;

    private Vector3 originalScale;

    public void OnClickButton()
    {
        originalScale = buttonText.transform.localScale;
        StartCoroutine(AnimateTextScale());
    }

    IEnumerator AnimateTextScale()
    {
        // �۰�
        yield return ScaleText(minScale, scaleDuration);

        // �������
        yield return ScaleText(1f, scaleDuration);
    }

    IEnumerator ScaleText(float targetScale, float duration)
    {
        Vector3 startScale = buttonText.transform.localScale;
        Vector3 endScale = originalScale * targetScale;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            buttonText.transform.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }

        buttonText.transform.localScale = endScale;
    }
}
