using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public static Map Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    public int floors = 15;
    public int columns = 7;
    public GameObject nodePrefab;
    public RectTransform nodeParent;
    public GameObject linePrefab;
    public RectTransform lineParent;
    private MapNode[,] grid;
    public GameObject battlePrefab;
    public GameObject mysteryPrefab;
    public GameObject StorePrefab;
    public GameObject PuzzlePrefab;
    public GameObject bossPrefab;
    public GameObject tutorialPrefab;
    public RectTransform backgroundBoxPrefab;
    public static bool mapGenerated = false;
    public int LEVEL;
    private MapNode currentNode;
    public static RectTransform latestBackgroundBox;
    public GameObject clearMarkPrefab;
    public bool previousInteractionState = true;
    public bool doorConnected = false; // ✅ 추가

    public GameObject redDotPrefab; // ✅ 추가
    private GameObject redDotInstance; // ✅ 추가

    void Start()
    {
        if (mapGenerated) return;
        nodeParent.SetAsLastSibling();
        lineParent.SetAsFirstSibling();
        GenerateGrid();
        GeneratePaths();
        AssignNodeTypes();
        AddStartNode();
        AddBossNode();
        ConnectNodesWithEdges();
        CreateBackgroundBox();

        mapGenerated = true;
    }

    private int previousLevel = -1;

    void Update()
    {
        if (LEVEL != previousLevel || doorConnected != previousInteractionState)
        {
            RefreshButtonStates();
            previousLevel = LEVEL;
            previousInteractionState = doorConnected;
        }

        IsClearCheck();
        UpdateRedDot(); // ✅ 추가
    }

    void UpdateRedDot() // ✅ 빨간 점 갱신 함수
    {
        if (redDotInstance != null)
        {
            Destroy(redDotInstance);
            redDotInstance = null;
        }

        if (!doorConnected && currentNode != null && redDotPrefab != null)
        {
            redDotInstance = Instantiate(redDotPrefab, currentNode.transform);
            RectTransform rt = redDotInstance.GetComponent<RectTransform>();
            rt.anchoredPosition = Vector2.zero;
        }
    }

    private void IsClearCheck()
    {
        if (doorConnected && currentNode != null)
        {
            Button btn = currentNode.GetComponentInChildren<Button>();
            if (btn != null)
                btn.gameObject.SetActive(false);

            foreach (Transform child in currentNode.transform)
            {
                if (child.name.Contains("ClearMark") || child.name.Contains("X"))
                    child.gameObject.SetActive(true);
            }

            currentNode = null;
        }
    }

    public void ResetMap()
    {
        Debug.Log("ResetMap called!");

        foreach (Transform child in nodeParent)
            Destroy(child.gameObject);

        foreach (Transform child in lineParent)
            Destroy(child.gameObject);

        currentNode = null;
        previousLevel = -1;
        LEVEL = 1;
        grid = null;
        mapGenerated = false;

        Start();
    }

    void RefreshButtonStates()
    {
        if (!doorConnected)
        {
            foreach (MapNode node in grid)
            {
                if (node == null) continue;
                Button button = node.GetComponentInChildren<Button>();
                if (button != null) button.interactable = false;
            }
            return;
        }

        foreach (MapNode node in grid)
        {
            if (node == null) continue;
            Button button = node.GetComponentInChildren<Button>();
            if (button != null) button.interactable = false;
        }

        if (currentNode == null)
        {
            for (int col = 0; col < columns; col++)
            {
                MapNode node = grid[col, LEVEL];
                if (node == null) continue;

                Button button = node.GetComponentInChildren<Button>();
                if (button != null)
                {
                    button.interactable = true;
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(() => OnNodeButtonClicked(node));
                }
            }
        }
        else
        {
            foreach (var nextNode in currentNode.connectedNodes)
            {
                if (nextNode == null && nextNode.floor != LEVEL) continue;

                Button button = nextNode.GetComponentInChildren<Button>();
                if (button != null)
                {
                    button.interactable = true;
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(() => OnNodeButtonClicked(nextNode));
                }
            }
        }
    }

    void OnNodeButtonClicked(MapNode node)
    {
        currentNode = node;
        Debug.Log(currentNode);
        LEVEL++;
    }

    void GenerateGrid()
    {
        grid = new MapNode[columns, floors + 1];
    }

    void AddBossNode()
    {
        int bossCol = columns / 2;
        int bossFloor = floors;

        MapNode bossNode = CreateNode(bossFloor, bossCol);
        bossNode.type = NodeType.Boss;
        UpdateNodeVisual(bossNode);

        for (int col = 0; col < columns; col++)
        {
            MapNode fromNode = grid[col, floors - 1];
            if (fromNode != null)
            {
                fromNode.connectedNodes.Add(bossNode);
            }
        }

        grid[bossCol, bossFloor] = bossNode;
    }

    void GeneratePaths()
    {
        for (int col = 0; col < columns; col++)
        {
            int currentCol = col;

            if (grid[currentCol, 1] == null)
                grid[currentCol, 1] = CreateNode(1, currentCol);

            for (int floor = 1; floor < floors; floor++)
            {
                if (grid[currentCol, floor] == null)
                    grid[currentCol, floor] = CreateNode(floor, currentCol);

                if (floor < floors - 1)
                {
                    List<int> nextCols = new List<int>();
                    if (currentCol > 0) nextCols.Add(currentCol - 1);
                    nextCols.Add(currentCol);
                    if (currentCol < columns - 1) nextCols.Add(currentCol + 1);

                    int nextCol = nextCols[Random.Range(0, nextCols.Count)];

                    if (grid[nextCol, floor + 1] == null)
                        grid[nextCol, floor + 1] = CreateNode(floor + 1, nextCol);

                    grid[currentCol, floor].connectedNodes.Add(grid[nextCol, floor + 1]);
                    currentCol = nextCol;
                }
            }
        }
    }

    MapNode CreateNode(int floor, int col)
    {
        float mapWidth = columns * 150f;
        float offsetX = -mapWidth / 2f + 150f / 2f;

        float y = floor * 150f;
        Vector2 pos = new Vector2(col * 150f + offsetX, y);

        GameObject obj = Instantiate(nodePrefab, nodeParent);
        obj.name = $"NODE{floor}";
        RectTransform rt = obj.GetComponent<RectTransform>();
        rt.anchoredPosition = pos;

        Vector3 localPos = rt.localPosition;
        localPos.z = 0f;
        rt.localPosition = localPos;

        MapNode node = obj.GetComponent<MapNode>();
        node.Init(floor, col, NodeType.Mystery);

        if (clearMarkPrefab != null)
        {
            GameObject clearMark = Instantiate(clearMarkPrefab, node.transform);
            RectTransform rtf = clearMark.GetComponent<RectTransform>();
            rtf.anchoredPosition = Vector2.zero;
            clearMark.SetActive(false);
        }

        return node;
    }

    void AssignNodeTypes()
    {
        for (int floor = 0; floor < floors; floor++)
        {
            for (int col = 0; col < columns; col++)
            {
                MapNode node = grid[col, floor];
                if (node == null) continue;

                if (floor == 1) node.type = NodeType.Battle;
                //else if (floor == 8) node.type = NodeType.Puzzle;
                else if (floor == 14) node.type = NodeType.Store;
                else
                {
                    float rand = Random.value;
                    if (rand < 0.5f) node.type = NodeType.Battle;
                    else if (rand < 0.7f) node.type = NodeType.Mystery;
                    else if (rand < 0.85f) node.type = NodeType.Store;
                    else node.type = NodeType.Puzzle;
                }

                UpdateNodeVisual(node);
            }
        }
    }

    void ConnectNodesWithEdges()
    {
        foreach (MapNode node in grid)
        {
            if (node == null) continue;

            RectTransform nodeRT = node.GetComponent<RectTransform>();
            Vector2 from = nodeRT.anchoredPosition;

            foreach (MapNode next in node.connectedNodes)
            {
                RectTransform nextRT = next.GetComponent<RectTransform>();
                Vector2 to = nextRT.anchoredPosition;

                CreateLine(from, to);
            }
        }
    }

    void CreateLine(Vector2 from, Vector2 to)
    {
        GameObject lineObj = Instantiate(linePrefab, lineParent);
        RectTransform rt = lineObj.GetComponent<RectTransform>();

        Vector2 direction = to - from;
        float length = direction.magnitude;

        rt.sizeDelta = new Vector2(length, 5f);
        rt.pivot = new Vector2(0, 0.5f);
        rt.anchoredPosition = from;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rt.localRotation = Quaternion.Euler(0, 0, angle);
    }

    void AddStartNode()
    {
        int startCol = columns / 2;
        int startFloor = 0;

        MapNode tutorialNode = CreateNode(startFloor, startCol);
        tutorialNode.type = NodeType.Tutorial;
        UpdateNodeVisual(tutorialNode);

        grid[startCol, startFloor] = tutorialNode;

        for (int col = 0; col < columns; col++)
        {
            MapNode upperNode = grid[col, 1];
            if (upperNode != null)
            {
                tutorialNode.connectedNodes.Add(upperNode);
            }
        }
    }

    void UpdateNodeVisual(MapNode node)
    {
        GameObject prefabToUse = null;

        switch (node.type)
        {
            case NodeType.Battle: prefabToUse = battlePrefab; break;
            case NodeType.Mystery: prefabToUse = mysteryPrefab; break;
            case NodeType.Store: prefabToUse = StorePrefab; break;
            case NodeType.Puzzle: prefabToUse = PuzzlePrefab; break;
            case NodeType.Boss: prefabToUse = bossPrefab; break;
            case NodeType.Tutorial: prefabToUse = tutorialPrefab; break;
        }

        if (prefabToUse != null)
        {
            GameObject visual = Instantiate(prefabToUse, node.transform);
            RectTransform rt = visual.GetComponent<RectTransform>();
            rt.anchoredPosition = Vector2.zero;

            Button button = visual.GetComponent<Button>();
            if (button != null)
            {
                button.interactable = (node.floor == LEVEL);
            }
        }
    }

    void CreateBackgroundBox()
    {
        float nodeWidth = 150f;
        float nodeHeight = 150f;

        float mapWidth = columns * nodeWidth;
        float mapHeight = (floors + 1) * nodeHeight;

        Vector2 position = new Vector2(0f, 10f);

        RectTransform mapPanel = nodeParent.parent.GetComponent<RectTransform>();
        RectTransform box = Instantiate(backgroundBoxPrefab, mapPanel);
        latestBackgroundBox = box;
        MapController dragHandler = FindAnyObjectByType<MapController>();
        if (dragHandler != null)
        {
            dragHandler.boundaryRect = box;
        }
        box.anchoredPosition = position;

        float padding = 50f;
        box.sizeDelta = new Vector2(mapWidth + padding, mapHeight + padding);

        int linePanelIndex = lineParent.GetSiblingIndex();
        box.SetSiblingIndex(linePanelIndex);
    }
}