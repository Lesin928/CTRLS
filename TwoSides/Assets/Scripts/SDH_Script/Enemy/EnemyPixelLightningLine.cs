using UnityEngine;

/// <summary>
/// 시작 지점과 끝 지점 사이에 픽셀 스타일의 전기 라인을 그리는 클래스입니다.
/// 흔들림과 픽셀 스냅 효과를 통해 전기 같은 시각 효과를 구현합니다.
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class EnemyPixelLightningLine : MonoBehaviour
{
    [Header("Point References")]
    public Transform startPoint; // 시작 지점 (라인의 시작 위치)
    public Transform endPoint;   // 끝 지점 (라인의 끝 위치)

    [Header("Line Settings")]
    public int segmentCount = 30;       // 라인을 구성할 세그먼트 개수 (숫자가 많을수록 더 부드러운 라인)
    public float waveAmplitude = 0.15f; // 파형의 진폭 (세로 방향 흔들림의 강도)
    public float waveFrequency = 8f;    // 파형의 주파수 (세로 방향 흔들림의 빈도)
    public float pixelSize = 0.02f;     // 픽셀 스냅을 위한 단위 크기 (픽셀 기반 정렬에 사용됨)

    [Header("Sorting Layer")]
    public string sortingLayerName = "Effect"; // Sorting Layer 이름
    public int sortingOrder = 0;               // Sorting Layer 내에서의 순서

    private LineRenderer lr; // 라인을 그리기 위한 LineRenderer 컴포넌트

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.textureMode = LineTextureMode.Tile;
        lr.alignment = LineAlignment.TransformZ;
        lr.startWidth = pixelSize * 0.3f;
        lr.endWidth = pixelSize * 0.3f;
        lr.material = new Material(Shader.Find("Sprites/Default"));

        // Sorting Layer와 Order 설정
        lr.sortingLayerName = sortingLayerName;
        lr.sortingOrder = sortingOrder;
    }

    private void Update()
    {
        if (startPoint == null || endPoint == null) 
            Destroy(gameObject);

        Vector3[] linePositions = new Vector3[segmentCount];

        for (int i = 0; i < segmentCount; i++)
        {
            float t = i / (float)(segmentCount - 1);
            Vector3 pos = Vector3.Lerp(startPoint.position, endPoint.position, t);

            // 흔들림 추가
            float noise1 = Mathf.PerlinNoise(t * waveFrequency, Time.time * 0.5f) * waveAmplitude;
            float noise2 = Mathf.PerlinNoise(t * waveFrequency * 2f, Time.time * 0.6f) * waveAmplitude * 0.5f;
            float randomNoise = Random.Range(-0.05f, 0.05f);

            pos.y += noise1 + noise2 + randomNoise;

            // 픽셀 스냅
            pos.x = Mathf.Round(pos.x / pixelSize) * pixelSize;
            pos.y = Mathf.Round(pos.y / pixelSize) * pixelSize;
            pos.z = Mathf.Round(pos.z / pixelSize) * pixelSize;

            linePositions[i] = pos;
        }

        lr.positionCount = linePositions.Length;
        lr.SetPositions(linePositions);
    }
}
