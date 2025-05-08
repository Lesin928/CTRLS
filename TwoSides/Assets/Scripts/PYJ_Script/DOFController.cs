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
            vignette.active = true; // 비네트 효과 켜기
            vignette.intensity.value = 0.3f; // 기본 강도
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (vignette != null)
            {
                vignette.intensity.value = 0.6f; // Enter 키로 강도 증가
            }
        }
    }
}

