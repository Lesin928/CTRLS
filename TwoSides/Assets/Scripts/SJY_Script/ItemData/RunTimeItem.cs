using UnityEngine;

public class RuntimeItem
{
    public StatType statType;
    public float value;
    public Sprite icon;
    public int price;

    public RuntimeItem(ItemData data)
    {
        statType = data.statType;
        value = data.value;
        icon = data.icon;
        price = data.price;
    }
}
