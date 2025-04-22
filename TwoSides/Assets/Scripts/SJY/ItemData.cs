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
public class ItemData
{
    public StatType statType;
    public float value;

    public ItemData(StatType statType, float value)
    {
        this.statType = statType;
        this.value = value;
    }
}
