using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 퍼즐 조각에 붙는 스크립트. 클릭 시 동작 처리와 이미지 설정을 담당.
/// </summary>
public class PuzzlePiece : MonoBehaviour, IPointerClickHandler
{
    public int CorrectIndex { get; set; }     // 퍼즐 조각이 원래 있어야 할 인덱스
    public int CurrentIndex { get; set; }     // 퍼즐 조각이 현재 있는 인덱스

    public Image image;      // 이 조각에 표시될 이미지 컴포넌트





    /// <summary>
    /// 퍼즐 조각에 스프라이트를 설정하고, 원래 위치 인덱스를 저장
    /// </summary>
    /// <param name="sprite">설정할 스프라이트</param>
    /// <param name="correctIndex">원래 위치 인덱스</param>
    public void SetSprite(Sprite sprite, int correctIndex)
    {
        image.sprite = sprite;
        CorrectIndex = correctIndex;
    }





    /// <summary>
    /// 퍼즐 조각이 클릭되었을 때 실행되는 함수
    /// </summary>
    /// <param name="eventData">클릭 이벤트 데이터</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        PuzzleManager1.Instance.SelectPiece(this); // 퍼즐 매니저에 자신을 선택하도록 전달
    }
}
