using System;
using Unity.Cinemachine;
using UnityEngine;
using System.Collections;

public class ParryZoom : MonoBehaviour
{
    [Header("Orthographic Zoom ����")]
    [Tooltip("�и� �� ���� Ȯ���� Orthographic Size")]
    public float zoomInSize = 3.5f;
    [Tooltip("Ȯ���ϴ� �� �ɸ��� �ð�")]
    public float zoomInDuration = 0.000001f;
    [Tooltip("������� �����ϴ� �� �ɸ��� �ð�")]
    public float zoomOutDuration = 0.1f;

    private CinemachineCamera cineCam;
    private float originalSize;
    private CinemachineConfiner2D confiner;



    void Awake()
    {
        // CinemachineCamera ������Ʈ ��������
        cineCam = GetComponent<CinemachineCamera>();
        if(cineCam != null)
        {
            confiner = cineCam.GetComponent<CinemachineConfiner2D>();
        }
        // �ʱ� OrthographicSize ����
        originalSize = cineCam.Lens.OrthographicSize;
    }
    
    void Start()
    {
        // 2) �� �ε� ����, "Confiner" �±��� background ������Ʈ ã��
        GameObject bg = GameObject.FindWithTag("Confiner");
        if (bg == null)
        {
            Debug.LogError("Confiner �±׸� ���� ������Ʈ�� �����ϴ�!");
            return;
        }

        // 3) BoxCollider2D ������Ʈ ��������
        BoxCollider2D bgCollider = bg.GetComponent<BoxCollider2D>();
        if (bgCollider == null)
        {
            Debug.LogError("��� ������Ʈ�� BoxCollider2D�� �����ϴ�!");
            return;
        }

        // 4) Confiner�� ��� �Ҵ�
        confiner.BoundingShape2D = bgCollider;

        // 5) ���� ������ ĳ�� ��ȿȭ
        if (confiner != null)
            confiner.InvalidateLensCache();
    }

    /// <summary>
    /// �и� �̺�Ʈ���� ȣ���� �޼���
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
        // 1) ������ Ȯ��
        float t = 0f;/*
        while (t < zoomInDuration)
        {
            t += Time.deltaTime;
            float size = Mathf.Lerp(originalSize, zoomInSize, t / zoomInDuration);
            SetOrthoSize(size);
            yield return null;
        }*/
        SetOrthoSize(zoomInSize);

        // 2) õõ�� ����
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

    // Lens�� struct�̹Ƿ�, ���� �� �ٽ� �Ҵ��ؾ� �մϴ�
    private void SetOrthoSize(float size)
    {
        var lens = cineCam.Lens;
        lens.OrthographicSize = size;
        cineCam.Lens = lens;
    }
}