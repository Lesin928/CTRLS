using System.Collections.Generic;
using UnityEngine;

// 노드의 타입 정의
public enum NodeType { Battle, Puzzle, Store, Mystery, Boss, Tutorial }

public class MapNode : MonoBehaviour
{
    public int floor; // 노드가 위치한 층 정보
    public int column; // 노드가 위치한 열 정보
    public NodeType type; // 노드의 타입
    public List<MapNode> connectedNodes = new List<MapNode>(); // 연결된 다음 노드 목록

    // 노드를 초기화하는 함수
    public void Init(int floor, int column, NodeType type)
    {
        this.floor = floor; // 층 설정
        this.column = column; // 열 설정
        this.type = type; // 타입 설정
    }
}
