using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IPointerClickHandler
{
    public int CorrectIndex { get; set; } // ���� ��ġ
    public int CurrentIndex { get; set; } // ���� ��ġ

    public Image image; // ���� �̹���

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
