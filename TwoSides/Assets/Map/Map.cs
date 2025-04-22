using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int floors = 15; // 총 층 수
    public int columns = 7; // 총 열 수
    public GameObject nodePrefab; // 기본 노드 프리팹
    public RectTransform nodeParent; // 노드를 생성할 부모 오브젝트 (UI용)
    public GameObject linePrefab; // 선(LineRenderer 또는 UI 라인) 프리팹
    public RectTransform lineParent; // 선을 생성할 부모 오브젝트
    private MapNode[,] grid; // 노드를 저장하는 2차원 배열
    public GameObject battlePrefab; // 전투 노드 시각화 프리팹
    public GameObject mysteryPrefab; // 미스터리 노드 시각화 프리팹
    public GameObject restPrefab; // 휴식 노드 시각화 프리팹
    public GameObject treasurePrefab; // 보물 노드 시각화 프리팹
    public GameObject bossPrefab; // 보스 노드 시각화 프리팹
    public RectTransform backgroundBoxPrefab; // 배경 박스 프리팹 (빈 Image UI 등)

    void Start()
    {
        GenerateGrid(); // 노드 배열 초기화
        GeneratePaths(); // 경로 생성
        AssignNodeTypes(); // 노드 타입 지정
        AddStartNode(); // 시작 노드 추가
        AddBossNode(); // 보스 노드 추가
        ConnectNodesWithEdges(); // 노드 간 연결선 생성
        CreateBackgroundBox(); // 전체 맵을 덮는 박스 생성
    }

    void GenerateGrid()
    {
        grid = new MapNode[columns, floors + 1]; // 보스층까지 포함한 그리드 배열 초기화
    }

    void AddBossNode()
    {
        int bossCol = columns / 2; // 중앙 열에 배치
        int bossFloor = floors;    // 마지막 층(16층)

        MapNode bossNode = CreateNode(bossFloor, bossCol); // 보스 노드 생성
        bossNode.type = NodeType.Boss; // 타입 설정
        UpdateNodeVisual(bossNode); // 시각 업데이트

        // 마지막 층 바로 아래 층의 모든 노드와 연결
        for (int col = 0; col < columns; col++)
        {
            MapNode fromNode = grid[col, floors - 1]; // 15층 노드
            if (fromNode != null)
            {
                fromNode.connectedNodes.Add(bossNode); // 보스 노드로 연결
            }
        }

        grid[bossCol, bossFloor] = bossNode; // 그리드에 보스 노드 저장
    }

    void GeneratePaths()
    {
        HashSet<int> usedStartCols = new HashSet<int>(); // 시작 열 중복 방지용

        for (int path = 0; path < 6; path++) // 6개의 경로 생성
        {
            int col;
            do
            {
                col = Random.Range(0, columns); // 랜덤한 시작 열 선택
            } while (path < 2 && usedStartCols.Contains(col)); // 앞 2개는 중복 방지

            usedStartCols.Add(col); // 사용한 열 저장
            int currentCol = col;

            for (int floor = 1; floor < floors; floor++) // 각 층마다 노드 생성
            {
                if (grid[currentCol, floor] == null)
                    grid[currentCol, floor] = CreateNode(floor, currentCol); // 노드가 없으면 생성

                if (floor < floors - 1) // 마지막 층이 아니면 다음 층 연결
                {
                    List<int> nextCols = new List<int>(); // 이동 가능한 열
                    if (currentCol > 0) nextCols.Add(currentCol - 1); // 왼쪽
                    nextCols.Add(currentCol); // 가운데
                    if (currentCol < columns - 1) nextCols.Add(currentCol + 1); // 오른쪽

                    int nextCol = nextCols[Random.Range(0, nextCols.Count)]; // 다음 열 선택

                    if (grid[nextCol, floor + 1] == null)
                        grid[nextCol, floor + 1] = CreateNode(floor + 1, nextCol); // 다음 층 노드 생성

                    grid[currentCol, floor].connectedNodes.Add(grid[nextCol, floor + 1]); // 노드 연결
                    currentCol = nextCol; // 현재 열 갱신
                }
            }
        }
    }

    MapNode CreateNode(int floor, int col)
    {
        float mapWidth = columns * 150f; // 전체 가로 크기
        float offsetX = -mapWidth / 2f + 150f / 2f; // 가로 중앙 정렬

        float y = floor * 150f; // 아래(0층) 기준으로 위로 올라감
        Vector2 pos = new Vector2(col * 150f + offsetX, y); // Y는 음수 없음

        GameObject obj = Instantiate(nodePrefab, nodeParent);
        obj.GetComponent<RectTransform>().anchoredPosition = pos;

        MapNode node = obj.GetComponent<MapNode>();
        node.Init(floor, col, NodeType.Mystery);
        return node;
    }



    void AssignNodeTypes()
    {
        for (int floor = 0; floor < floors; floor++) // 모든 층 반복
        {
            for (int col = 0; col < columns; col++) // 모든 열 반복
            {
                MapNode node = grid[col, floor]; // 현재 노드
                if (node == null) continue; // 없으면 건너뜀

                if (floor == 0) node.type = NodeType.Battle; // 시작 층은 전투
                else if (floor == 8) node.type = NodeType.Treasure; // 9층은 보물
                else if (floor == 14) node.type = NodeType.Rest; // 15층은 휴식
                else
                {
                    float rand = Random.value; // 랜덤 타입 지정
                    if (rand < 0.5f) node.type = NodeType.Battle;
                    else if (rand < 0.7f) node.type = NodeType.Mystery;
                    else if (rand < 0.85f) node.type = NodeType.Rest;
                    else node.type = NodeType.Treasure;
                }
                UpdateNodeVisual(node); // 시각 업데이트
            }
        }
    }

    void ConnectNodesWithEdges()
    {
        foreach (MapNode node in grid) // 모든 노드 순회
        {
            if (node == null) continue; // 없으면 건너뜀

            RectTransform nodeRT = node.GetComponent<RectTransform>();
            Vector2 from = nodeRT.anchoredPosition; // 시작 위치

            foreach (MapNode next in node.connectedNodes) // 연결된 노드 순회
            {
                RectTransform nextRT = next.GetComponent<RectTransform>();
                Vector2 to = nextRT.anchoredPosition; // 끝 위치

                CreateLine(from, to); // 라인 생성
            }
        }
    }

    void CreateLine(Vector2 from, Vector2 to)
    {
        GameObject lineObj = Instantiate(linePrefab, lineParent); // 라인 오브젝트 생성
        RectTransform rt = lineObj.GetComponent<RectTransform>();

        Vector2 direction = to - from; // 방향 벡터
        float length = direction.magnitude; // 선 길이

        rt.sizeDelta = new Vector2(length, 5f); // 선 길이 및 두께 설정
        rt.pivot = new Vector2(0, 0.5f); // 회전 기준점
        rt.anchoredPosition = from; // 시작 위치 설정
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 각도 계산
        rt.localRotation = Quaternion.Euler(0, 0, angle); // 회전 적용
    }
    void AddStartNode()
    {
        int startCol = columns / 2; // 중앙 열
        int startFloor = 0;

        MapNode startNode = CreateNode(startFloor, startCol);
        startNode.type = NodeType.Battle;
        UpdateNodeVisual(startNode);

        grid[startCol, startFloor] = startNode;

        // 1층에 연결 가능한 노드가 있으면 연결
        for (int col = 0; col < columns; col++)
        {
            MapNode upperNode = grid[col, 1]; // 1층 노드
            if (upperNode != null)
            {
                startNode.connectedNodes.Add(upperNode);
            }
        }
    }


    void UpdateNodeVisual(MapNode node)
    {
        GameObject prefabToUse = null; // 사용할 프리팹

        switch (node.type) // 노드 타입에 따라 프리팹 선택
        {
            case NodeType.Battle: prefabToUse = battlePrefab; break;
            case NodeType.Mystery: prefabToUse = mysteryPrefab; break;
            case NodeType.Rest: prefabToUse = restPrefab; break;
            case NodeType.Treasure: prefabToUse = treasurePrefab; break;
            case NodeType.Boss: prefabToUse = bossPrefab; break;
        }

        if (prefabToUse != null)
        {
            GameObject visual = Instantiate(prefabToUse, node.transform); // 자식으로 생성
            RectTransform rt = visual.GetComponent<RectTransform>();
            rt.anchoredPosition = Vector2.zero; // 중앙 정렬
        }
    }
    void CreateBackgroundBox()
    {
        float nodeWidth = 150f;
        float nodeHeight = 150f;

        float mapWidth = columns * nodeWidth;
        float mapHeight = (floors + 1) * nodeHeight;

        // 박스의 중심을 정확히 맞추기
        // X: 가운데 정렬
        float offsetX = -mapWidth / 2f + nodeWidth / 2f;

        // Y: 0층이 Y=0이므로, 맨 위까지 가는 중앙 위치는 /2
        float offsetY = mapHeight / 2f - nodeHeight / 2f;

        Vector2 position = new Vector2(mapWidth / 2f + offsetX, offsetY);

        RectTransform box = Instantiate(backgroundBoxPrefab, nodeParent);
        box.anchoredPosition = position;

        // 🔥 크기를 정확히 키우기: 노드 전체 영역보다 더 크게 감싸고 싶으면 여유를 추가해도 됨
        float padding = 50f;
        box.sizeDelta = new Vector2(mapWidth + padding, mapHeight + padding);

        // 맨 뒤로 보내기
        box.SetAsFirstSibling();
    }


}
