using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// ���� ������ �ٴ� ��ũ��Ʈ. Ŭ�� �� ���� ó���� �̹��� ������ ���.
/// </summary>
public class PuzzlePiece : MonoBehaviour, IPointerClickHandler
{
    public int CorrectIndex { get; set; }     // ���� ������ ���� �־�� �� �ε���
    public int CurrentIndex { get; set; }     // ���� ������ ���� �ִ� �ε���

    public Image image;      // �� ������ ǥ�õ� �̹��� ������Ʈ





    /// <summary>
    /// ���� ������ ��������Ʈ�� �����ϰ�, ���� ��ġ �ε����� ����
    /// </summary>
    /// <param name="sprite">������ ��������Ʈ</param>
    /// <param name="correctIndex">���� ��ġ �ε���</param>
    public void SetSprite(Sprite sprite, int correctIndex)
    {
        image.sprite = sprite;
        CorrectIndex = correctIndex;
    }





    /// <summary>
    /// ���� ������ Ŭ���Ǿ��� �� ����Ǵ� �Լ�
    /// </summary>
    /// <param name="eventData">Ŭ�� �̺�Ʈ ������</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        PuzzleManager1.Instance.SelectPiece(this); // ���� �Ŵ����� �ڽ��� �����ϵ��� ����
    }
}
