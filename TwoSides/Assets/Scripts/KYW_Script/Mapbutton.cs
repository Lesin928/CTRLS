using UnityEngine;
using System.Collections;

public class Mapbutton : MonoBehaviour
{
    public GameObject map; // �� ��ü UI ������Ʈ
    public float animationDuration = 0.3f;

    private bool isVisible = false;
    private Coroutine animationCoroutine;

    private void Start()
    {
        isVisible = map.activeSelf; // ���� ���� �ݿ�
        if (map != null)
        {
            RectTransform rect = map.GetComponent<RectTransform>();
            rect.localScale = isVisible ? Vector3.one : new Vector3(0f, 1f, 1f);
        }
    }

    public void mapsetting()
    {
        if (animationCoroutine != null)
            StopCoroutine(animationCoroutine);

        if (isVisible)
        {
            animationCoroutine = StartCoroutine(HideMap());
        }
        else
        {
            map.SetActive(true);
            animationCoroutine = StartCoroutine(ShowMap());
        }

        isVisible = !isVisible;
    }
    public void button()
    {
        int i = 1;
        i++;
    }
    private IEnumerator ShowMap()
    {
        RectTransform rect = map.GetComponent<RectTransform>();
        float time = 0f;
        Vector3 startScale = new Vector3(0f, 1f, 1f);
        Vector3 endScale = Vector3.one;

        rect.localScale = startScale;

        yield return null; // ���� �����ӿ� MapController �ʱ�ȭ�� �� �ְ� ���� �ֱ�

        while (time < animationDuration)
        {
            float t = time / animationDuration;
            rect.localScale = Vector3.Lerp(startScale, endScale, t);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        rect.localScale = endScale;
    }

    private IEnumerator HideMap()
    {
        RectTransform rect = map.GetComponent<RectTransform>();
        float time = 0f;
        Vector3 startScale = Vector3.one;
        Vector3 endScale = new Vector3(0f, 1f, 1f);

        while (time < animationDuration)
        {
            float t = time / animationDuration;
            rect.localScale = Vector3.Lerp(startScale, endScale, t);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        rect.localScale = endScale;
        map.SetActive(false);
    }
}
