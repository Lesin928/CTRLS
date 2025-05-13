using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// 페이드인 페이드아웃 스크립트트
public class FadeController : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.5f;

    public IEnumerator FadeOut()
    {
        Debug.Log("FadeOut Start");

        float time = 0f;
        Color color = fadeImage.color;

        if (color.a == 1f)
        {   //페이드아웃
            while (time < fadeDuration)
            {
                time += Time.deltaTime;
                color.a = Mathf.Lerp(1f, 0f, time / fadeDuration);
                fadeImage.color = color;
                yield return null;
            }
        }
        else
        {   //페이드인인
            while (time < fadeDuration)
            {
                time += Time.deltaTime;
                color.a = Mathf.Lerp(0f, 1f, time / fadeDuration);
                fadeImage.color = color;
                yield return null;
            }
        }

        Debug.Log("FadeOut Complete");

        gameObject.SetActive(false);
    }
}