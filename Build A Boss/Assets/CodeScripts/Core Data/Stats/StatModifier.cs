using UnityEngine;

public class StatModifier
{
    public StatType statType;
    public int flatBonus;

    public StatModifier(StatType _statType, int _flatBonus)
    {
        statType = _statType;
        flatBonus = _flatBonus;
    }
}
