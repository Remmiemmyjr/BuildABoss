using System.Collections.Generic;
using UnityEngine;


public class StatBlock
{
    private Dictionary<StatType, int> baseStats;
    private List<StatModifier> modifiers = new List<StatModifier>();

    public StatBlock(StatBlockDefinition definition)
    {
        baseStats = new Dictionary<StatType, int>();

        baseStats[StatType.HP] = definition.maxHP;
        baseStats[StatType.Mana] = definition.maxMana;
        baseStats[StatType.AttackDamage] = definition.attackDamage;
        baseStats[StatType.Defense] = definition.defense;
        baseStats[StatType.Speed] = definition.speed;
        baseStats[StatType.SpecialPower] = definition.specialPower;
    }

    public int GetStat(StatType statType)
    {
        int value = baseStats.TryGetValue(statType, out int baseValue) ? baseValue : 0;

        if(modifiers != null)
        {
            foreach (var modifier in modifiers)
            {
                if (modifier.statType == statType)
                    value += modifier.flatBonus;
            }
        }

        return value;
    }
    
}


