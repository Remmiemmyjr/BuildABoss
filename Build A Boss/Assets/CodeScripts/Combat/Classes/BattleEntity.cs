using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEntity
{
    // TODO: Set visual data at some point (sprite n whatnot)

    #region Variables
    public EntityClass Entity { get; private set; }
    public EntityProfileInstance Profile { get; set; }
    public CombatAIProfile CombatAI;
    public bool IsAlive => currHP > 0;
    public bool IsDefending = false; // TODO: hmmmm.... gotta be a better way to manage lmao

    [Header("Battle Info")]
    public int level => Entity.level; // saves memory, cant accidentally edit level
    public int currHP;
    public int currMana;
    public List<SpecialMove> KnownMoves { get; private set; }
    public StatusConditionInstance statusCondition;
    public StatBlock RuntimeStats; // protected or private?

    // Lambda Getters of Base Stats, for ease of access (could just use RuntimeStats)
    public int AttackDamage => GetStatWithModifier(StatType.AttackDamage);
    public int Defense => GetStatWithModifier(StatType.Defense);
    public int Speed => GetStatWithModifier(StatType.Speed);
    public int SpecialPower => GetStatWithModifier(StatType.SpecialPower);
    #endregion

    #region Delegates & Events
    public event Action<BattleEntity> OnHealthChanged;
    public event Action<BattleEntity> OnManaChanged;
    #endregion


    #region Constructor
    public BattleEntity(EntityProfileInstance _profile) 
    {
        Profile = _profile;
        Entity = _profile.entityDefinition;
        currHP = _profile.currHP;
        currMana = _profile.currMana;
        statusCondition = _profile.statusCondition;
        KnownMoves = new List<SpecialMove>(_profile.KnownMoves);
        RuntimeStats = new StatBlock(Entity.baseStats);
    }
    #endregion


    #region Battle Operations
    public void TakeDamage(int damage)
    {
        // TODO: Damage should be influenced by entity stats
        currHP -= (damage >= currHP)? currHP : damage;

        damage = Mathf.Clamp(damage, 0, currHP);

        OnHealthChanged?.Invoke(this);
    }

    public void Heal(int amount)
    {
        if (currHP >= Entity.baseStats.maxHP)
            return;
        amount = Mathf.Clamp(amount, 0, Entity.baseStats.maxHP - currHP);

        currHP += amount;

        OnHealthChanged?.Invoke(this);
    }

    public bool UseMana(int cost)
    {
        // TODO: pathetic
        if (currMana <= cost) // TODO: broke ass, not enough to spend. dont forget to tell player this
            return false;

        currMana -= cost;

        OnManaChanged?.Invoke(this);
        return true;
    }

    public void RestoreMana(int amount)
    {
        // TODO: pathetic
        if (currMana >= Entity.baseStats.maxMana)
            return;

        currMana += amount;
        OnManaChanged?.Invoke(this);
    }

    public void ApplyStatusEffect(StatusConditionTypes statusEffect)
    {
        //statusCondition = DB_StatusCondition.Conditions[statusEffect];
        statusCondition = new StatusConditionInstance(statusEffect, DB_StatusCondition.Conditions[statusEffect].duration);
    }

    public void RemoveStatusEffect()
    {
        statusCondition = null;
    }

    public int GetStatWithModifier(StatType stat)
    {
        return RuntimeStats.GetStat(stat);
    }

    public void ResetAll()
    {
        currHP = Entity.baseStats.maxHP;
        currMana = Entity.baseStats.maxMana;
        RemoveStatusEffect();
    }
    #endregion

}
