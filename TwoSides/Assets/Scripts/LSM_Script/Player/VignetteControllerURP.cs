using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class VignetteControllerURP : MonoBehaviour
{
    public Volume volume;
    private Vignette vignette;

    void Start()
    {
        if (volume.profile.TryGet(out vignette))
        {
            vignette.active = true;
            vignette.intensity.value = 0.2f; // �⺻ ����
        }
    }
    void Awake()
    { 
        if (volume == null)
        {
            volume = FindAnyObjectByType<Volume>();   
            if (volume == null)
                Debug.LogError("���� Global Volume�� �����ϴ�!");
        }
    } 

    public void TriggerVignette()
    {
        if (vignette != null)
        {
            StartCoroutine(SmoothTrigger());
            StartCoroutine(SmoothReset());
        }
    }  

    IEnumerator SmoothTrigger()
    {
        float t = 0;
        float start = 0.3f, end = 0.6f, dur = 0.2f;
        while (t < dur)
        {
            vignette.intensity.value = Mathf.Lerp(start, end, t / dur);
            t += Time.deltaTime;
            yield return null;
        }
        vignette.intensity.value = end;
    }

    IEnumerator SmoothReset()
    {
        float t = 0;
        float start = 0.6f, end = 0.3f, dur = 0.2f;
        while (t < dur)
        {
            vignette.intensity.value = Mathf.Lerp(start, end, t / dur);
            t += Time.deltaTime;
            yield return null;
        }
        vignette.intensity.value = end;
    }

}