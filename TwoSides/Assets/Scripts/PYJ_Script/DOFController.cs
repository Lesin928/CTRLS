using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteControllerURP : MonoBehaviour
{
    public Volume volume;
    private Vignette vignette;

    void Start()
    {
        if (volume.profile.TryGet(out vignette))
        {
            vignette.active = true;
            vignette.intensity.value = 0.3f; // 기본 강도
        }
    }

    // 외부에서 호출할 때 강도를 0.6으로 고정
    public void TriggerVignette()
    {
        if (vignette != null)
        {
            vignette.intensity.value = 0.6f;
        }
    }

    // 외부에서 호출할 때 강도를 0.3으로 고정
    public void ResetVignette()
    {
        if (vignette != null)
        {
            vignette.intensity.value = 0.3f;
        }
    }
}
