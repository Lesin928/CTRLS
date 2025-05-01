using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MapController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IScrollHandler
{
    public RectTransform contentRect; // 맵 패널 (nodeParent)
    public RectTransform boundaryRect; // 배경 박스 (경계)

    public float dragSpeed = 1f;
    public float zoomSpeed = 0.1f;
    public float minZoom = 0.5f;
    public float maxZoom = 2f;

    private Vector2 lastMousePosition;
    private Vector3 initialScale;

    private RectTransform viewportRect; // 드래그 영역 (RectMask2D 붙어있는 GameObject)

    private void OnEnable()
    {
        StartCoroutine(DelayedInit());
    }

    private IEnumerator DelayedInit()
    {
        yield return null; // 한 프레임 대기

        if (contentRect == null || boundaryRect == null)
        {
            Debug.LogError("ContentRect나 BoundaryRect가 설정되지 않았습니다!");
            yield break;
        }

        viewportRect = GetComponent<RectTransform>(); // 뷰포트 RectTransform 설정
        initialScale = contentRect.localScale;

        ClampPosition(); // 초기 위치 클램프
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastMousePosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.position - lastMousePosition;

        // Y축 이동만 허용
        contentRect.anchoredPosition += new Vector2(0f, delta.y * dragSpeed);
        lastMousePosition = eventData.position;

        ClampPosition();
    }

    public void OnEndDrag(PointerEventData eventData) { }

    public void OnScroll(PointerEventData eventData)
    {
        float scrollDelta = eventData.scrollDelta.y;

        Vector3 newScale = contentRect.localScale + Vector3.one * (scrollDelta * zoomSpeed);
        newScale = Vector3.Max(initialScale * minZoom, Vector3.Min(initialScale * maxZoom, newScale));
        contentRect.localScale = newScale;

        ClampPosition();
    }

    private void ClampPosition()
    {
        if (boundaryRect == null || contentRect == null || viewportRect == null)
            return;

        Vector2 boundarySize = boundaryRect.rect.size * boundaryRect.lossyScale;
        Vector2 viewportSize = viewportRect.rect.size * viewportRect.lossyScale;
        Vector2 contentSize = contentRect.rect.size * contentRect.lossyScale * contentRect.localScale;

        float fixedX = 0f;

        float minY = viewportSize.y / 2f - boundarySize.y;
        float maxY = viewportSize.y / 2f - 700f;

        float clampY = Mathf.Clamp(contentRect.anchoredPosition.y, minY, maxY);
        contentRect.anchoredPosition = new Vector2(fixedX, clampY);
    }
}
