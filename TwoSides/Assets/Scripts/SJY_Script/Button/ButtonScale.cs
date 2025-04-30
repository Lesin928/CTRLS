using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
        // 작게
        yield return ScaleText(minScale, scaleDuration);

        // 원래대로
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
