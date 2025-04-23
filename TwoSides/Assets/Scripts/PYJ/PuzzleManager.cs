using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{

    /// ���� �Ŵ��� �̱��� �ν��Ͻ�
    public static PuzzleManager Instance;

    /// ���� ���� ������
    public GameObject piecePrefab;

    /// ���� ������ ��ġ�� �θ�(�׸��� �г�)
    public Transform puzzleParent;

    /// ���� ���������� �߶� ���� ��������Ʈ �迭
    public Sprite[] puzzleSprites;

    /// ���� ������ ���� ���� ����Ʈ
    private List<PuzzlePiece> pieces = new();

    /// ���� ���õ� ���� ����
    private PuzzlePiece selectedPiece;

    /// �̱��� �ν��Ͻ� �Ҵ�
    private void Awake()
    {
        Instance = this;
    }





    /// ���� ���� �� ���� ����
    void Start()
    {
        CreatePuzzle();
    }





    /// <summary>
    /// ���� ������ �����ϰ� �������� ��ġ
    /// </summary>
    void CreatePuzzle()
    {
        List<int> indices = Enumerable.Range(0, puzzleSprites.Length).ToList();
        Shuffle(indices);

        for (int i = 0; i < 16; i++)
        {
            GameObject obj = Instantiate(piecePrefab, puzzleParent);
            PuzzlePiece piece = obj.GetComponent<PuzzlePiece>();

            piece.image = obj.GetComponent<Image>();
            piece.SetSprite(puzzleSprites[indices[i]], indices[i]);
            piece.CurrentIndex = i;

            pieces.Add(piece);
        }
    }

    /// <summary>
    /// ���� ���� ���� ó��
    /// </summary>
    /// <param name="piece">���õ� ���� ����</param>
    public void SelectPiece(PuzzlePiece piece)
    {
        if (selectedPiece == null)
        {
            selectedPiece = piece;
        }
        else
        {
            SwapPieces(selectedPiece, piece);
            selectedPiece = null;
            CheckClear();
        }
    }

    /// <summary>
    /// �� ���� ������ �̹����� ���� �ε����� ����
    /// </summary>
    void SwapPieces(PuzzlePiece a, PuzzlePiece b)
    {
        Sprite tempSprite = a.image.sprite;
        int tempCorrectIndex = a.CorrectIndex;

        a.image.sprite = b.image.sprite;
        a.CorrectIndex = b.CorrectIndex;

        b.image.sprite = tempSprite;
        b.CorrectIndex = tempCorrectIndex;
    }

    /// <summary>
    /// ������ �ϼ��Ǿ����� �˻�
    /// </summary>
    void CheckClear()
    {
        bool isCorrect = true;
        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i].CorrectIndex != i)
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
        {
            Debug.Log("���� �ϼ�!");
            // ������ �ϼ��Ǹ� ���� �г��� �ݴ´�
            InteractiveObject interactiveObject = FindObjectOfType<InteractiveObject>();
            if (interactiveObject != null)
            {
                interactiveObject.HidePuzzle(); // ���� �г� �����
            }
        }
    }


    /// <summary>
    /// ����Ʈ ��Ҹ� �������� ���� �Լ�
    /// </summary>
    void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
