using System.Collections.Generic;
using UnityEngine;

public class EntityProfileInstance
{
    public EntityClass entityDefinition;
    public int currHP;
    public int currMana;
    public int level;

    public List<SpecialMove> KnownMoves;
    public StatusConditionInstance statusCondition;

    // Constructor
    public EntityProfileInstance(EntityClass _entityDefinition)
    {
        entityDefinition = _entityDefinition;

        currHP = _entityDefinition.baseStats.maxHP;
        currMana = _entityDefinition.baseStats.maxMana;
        level = _entityDefinition.level;

        KnownMoves = _entityDefinition.knownMoves;
        statusCondition = null;
    }
}