using System.Collections.Generic;
using UnityEngine;

// ����� Ÿ�� ����
public enum NodeType { Battle, Treasure, Rest, Mystery, Boss }

public class MapNode : MonoBehaviour
{
    public int floor; // ��尡 ��ġ�� �� ����
    public int column; // ��尡 ��ġ�� �� ����
    public NodeType type; // ����� Ÿ��
    public List<MapNode> connectedNodes = new List<MapNode>(); // ����� ���� ��� ���

    // ��带 �ʱ�ȭ�ϴ� �Լ�
    public void Init(int floor, int column, NodeType type)
    {
        this.floor = floor; // �� ����
        this.column = column; // �� ����
        this.type = type; // Ÿ�� ����
    }
}
