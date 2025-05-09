using System;
using Unity.Cinemachine;
using UnityEngine;
using System.Collections;

public class ParryZoom : MonoBehaviour
{
    [Header("Orthographic Zoom 설정")]
    [Tooltip("패링 시 순간 확대할 Orthographic Size")]
    public float zoomInSize = 3.5f;
    [Tooltip("확대하는 데 걸리는 시간")]
    public float zoomInDuration = 0.000001f;
    [Tooltip("원래대로 복귀하는 데 걸리는 시간")]
    public float zoomOutDuration = 0.1f;

    private CinemachineCamera cineCam;
    private float originalSize;
    private CinemachineConfiner2D confiner;



    void Awake()
    {
        // CinemachineCamera 컴포넌트 가져오기
        cineCam = GetComponent<CinemachineCamera>();
        if(cineCam != null)
        {
            confiner = cineCam.GetComponent<CinemachineConfiner2D>();
        }
        // 초기 OrthographicSize 저장
        originalSize = cineCam.Lens.OrthographicSize;
    }
    
    void Start()
    {
        // 2) 씬 로딩 직후, "Confiner" 태그의 background 오브젝트 찾기
        GameObject bg = GameObject.FindWithTag("Confiner");
        if (bg == null)
        {
            Debug.LogError("Confiner 태그를 가진 오브젝트가 없습니다!");
            return;
        }

        // 3) BoxCollider2D 컴포넌트 가져오기
        BoxCollider2D bgCollider = bg.GetComponent<BoxCollider2D>();
        if (bgCollider == null)
        {
            Debug.LogError("배경 오브젝트에 BoxCollider2D가 없습니다!");
            return;
        }

        // 4) Confiner에 경계 할당
        confiner.BoundingShape2D = bgCollider;

        // 5) 내부 폴리곤 캐시 무효화
        if (confiner != null)
            confiner.InvalidateLensCache();
    }

    /// <summary>
    /// 패링 이벤트에서 호출할 메서드
    /// </summary>
    public void OnParry()
    {
        StopAllCoroutines();
        StartCoroutine(ZoomOrthoRoutine());
        var confiner = cineCam.GetComponent<CinemachineConfiner2D>();
        if (confiner != null)
            confiner.InvalidateLensCache();
    }

    private IEnumerator ZoomOrthoRoutine()
    {
        // 1) 빠르게 확대
        float t = 0f;/*
        while (t < zoomInDuration)
        {
            t += Time.deltaTime;
            float size = Mathf.Lerp(originalSize, zoomInSize, t / zoomInDuration);
            SetOrthoSize(size);
            yield return null;
        }*/
        SetOrthoSize(zoomInSize);

        // 2) 천천히 복귀
        t = 0f;
        while (t < zoomOutDuration)
        {
            t += Time.deltaTime;
            float size = Mathf.Lerp(zoomInSize, originalSize, t / zoomOutDuration);
            SetOrthoSize(size);
            yield return null;
        }
        SetOrthoSize(originalSize);
    }

    // Lens는 struct이므로, 변경 후 다시 할당해야 합니다
    private void SetOrthoSize(float size)
    {
        var lens = cineCam.Lens;
        lens.OrthographicSize = size;
        cineCam.Lens = lens;
    }
}