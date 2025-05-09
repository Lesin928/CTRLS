using System.Collections;
using UnityEngine;

public class CreditText : MonoBehaviour
{
    public RectTransform uiImage;
    public Vector2 targetPosition;
    public float speed = 200f;

    public GameObject fadeCanvasPrefab;

    private bool isFaded = false;

    void Update()
    {
        uiImage.anchoredPosition = Vector2.MoveTowards(
            uiImage.anchoredPosition,
            targetPosition,
            speed * Time.deltaTime
        );

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
    }
}