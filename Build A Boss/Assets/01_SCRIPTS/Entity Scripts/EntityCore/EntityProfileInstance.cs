using System.Collections.Generic;
using UnityEngine;

public abstract class EntityProfileInstance
{
    public EntityClass BaseClass;
    public int CurrHP;
    public int CurrMana;
    public int Level;
    public GameObject GameObjectInstance;
    public GameObject OverworldController; // change to class Controller
    public StatBlockDefinition Stats;
    public List<SpecialMove> KnownMoves;
    public StatusConditionInstance CurrStatusCondition;

    // Constructor
    public EntityProfileInstance(EntityClass _entityClass)
    {
        BaseClass = _entityClass;
        Stats = _entityClass.baseStats;
        CurrHP = _entityClass.baseStats.maxHP;
        CurrMana = _entityClass.baseStats.maxMana;
        Level = _entityClass.level;

        KnownMoves = _entityClass.knownMoves;
        CurrStatusCondition = null;
    }
}