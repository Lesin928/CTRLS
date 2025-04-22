
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    public GameObject piecePrefab;
    public Transform puzzleParent; // GridLayoutGroup 있는 Panel
    public Sprite[] puzzleSprites; // 잘라놓은 16개 스프라이트

    private List<PuzzlePiece> pieces = new();
    private PuzzlePiece selectedPiece;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        CreatePuzzle();
    }

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

    void SwapPieces(PuzzlePiece a, PuzzlePiece b)
    {
        // Sprite 스왑
        Sprite tempSprite = a.image.sprite;
        int tempCorrectIndex = a.CorrectIndex;

        a.image.sprite = b.image.sprite;
        a.CorrectIndex = b.CorrectIndex;

        b.image.sprite = tempSprite;
        b.CorrectIndex = tempCorrectIndex;
    }

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
            Debug.Log("퍼즐 완성!");
        }
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
