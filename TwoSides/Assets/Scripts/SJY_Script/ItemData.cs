using System.Data;
using UnityEngine;

public enum StatType
{
    Health,
    MaxHealth,
    Armor,
    Attack,
    AttackSpeed,
    MoveSpeed,
    Critical,
    CriticalDamage
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/ItemData")]
public class ItemData : ScriptableObject
{
    public StatType statType;
    public float value;
    public Sprite icon;
}