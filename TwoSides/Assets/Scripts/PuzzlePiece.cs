using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IPointerClickHandler
{
    public int CorrectIndex { get; set; } // 원래 위치
    public int CurrentIndex { get; set; } // 현재 위치

    public Image image; // 퍼즐 이미지

    public void SetSprite(Sprite sprite, int correctIndex)
    {
        image.sprite = sprite;
        CorrectIndex = correctIndex;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PuzzleManager.Instance.SelectPiece(this);
    }
}
