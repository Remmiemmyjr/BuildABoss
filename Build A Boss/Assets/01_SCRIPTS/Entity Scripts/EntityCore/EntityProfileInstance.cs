using System.Collections.Generic;
using UnityEngine;

public class EntityProfileInstance
{
    public EntityClass EntityDefinition;
    public int CurrHP;
    public int CurrMana;
    public int Level;
    public GameObject GameObjectInstance;
    public GameObject OverworldController; // change to class Controller

    public List<SpecialMove> KnownMoves;
    public StatusConditionInstance CurrStatusCondition;

    // Constructor
    public EntityProfileInstance(EntityClass _entityDefinition)
    {
        EntityDefinition = _entityDefinition;

        CurrHP = _entityDefinition.baseStats.maxHP;
        CurrMana = _entityDefinition.baseStats.maxMana;
        Level = _entityDefinition.level;

        KnownMoves = _entityDefinition.knownMoves;
        CurrStatusCondition = null;
    }
}