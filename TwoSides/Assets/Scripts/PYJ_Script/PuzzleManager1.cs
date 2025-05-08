using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager1 : MonoBehaviour
{


    public GameObject puzzlePanel;


    /// 퍼즐 매니저 싱글톤 인스턴스
    public static PuzzleManager1 Instance;

    /// 퍼즐 조각 프리팹
    public GameObject piecePrefab;

    /// 퍼즐 조각이 배치될 부모(그리드 패널)
    public Transform puzzleParent;

    /// 퍼즐 조각용으로 잘라 놓은 스프라이트 배열
    public Sprite[] puzzleSprites;

    /// 현재 생성된 퍼즐 조각 리스트
    private List<PuzzlePiece> pieces = new();

    /// 현재 선택된 퍼즐 조각
    private PuzzlePiece selectedPiece;

    /// 싱글톤 인스턴스 할당
    private void Awake()
    {
        Instance = this;
    }

    private bool puzzleCleared = false; // 퍼즐 클리어 여부



    /// <summary>
    /// 게임 시작 시 퍼즐 생성
    /// /// </summary>
    void Start()
    {
        CreatePuzzle();
    }



    public bool IsPuzzleCleared()
    {
        return puzzleCleared;
    }



    //void Update()
    //{
    //    if (puzzleCleared) return;

    //    if (Input.GetKeyDown(KeyCode.F))
    //    {
    //        if (puzzlePanel.activeSelf)
    //        {
    //            puzzlePanel.SetActive(false); // �г� �ݱ�
    //            Debug.Log("���� â �ݱ�");
    //        }
    //        else
    //        {
    //            puzzlePanel.SetActive(true); // �г� ����
    //            Debug.Log("���� â ����");
    //        }
    //    }
    //}




    /// <summary>
    /// 퍼즐 조각을 생성하고 무작위로 배치
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
    /// 퍼즐 조각 선택 처리
    /// </summary>
    /// <param name="piece">선택된 퍼즐 조각</param>
    /// 

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
    /// 두 퍼즐 조각의 이미지와 정답 인덱스를 바꾸기
    /// 
    /// 두 조각의 그림이 바뀌어야 하고,
    ///각 조각이 원래 어디 위치에 있어야 했는지도 바뀌어야 퍼즐 완성 여부를 정확히 판단할 수 있음

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
    /// 퍼즐이 완성되었는지 검사
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

            GameManager.Instance.OnStageClear();

            Debug.Log("퍼즐 완성!");
            puzzleCleared = true; // 클리어 상태로 기록

            InteractiveObject interactiveObject = Object.FindFirstObjectByType<InteractiveObject>();
            if (interactiveObject != null)
            {
                interactiveObject.HidePuzzle(); // 퍼즐 패널 숨기기
            }
        }
    }




    /// <summary>
    /// 리스트 요소를 무작위로 섞는 함수
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
