using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

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
        {
            while (time < fadeDuration)
            {
                time += Time.deltaTime;
                color.a = Mathf.Lerp(1f, 0f, time / fadeDuration);
                fadeImage.color = color;
                yield return null;
            }
        }
        else
        {
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