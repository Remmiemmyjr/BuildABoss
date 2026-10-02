using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEntity
{
    // TODO: Set visual data at some point (sprite n whatnot)

    #region Variables
    //public EntityData Entity { get; private set; }
    public EntityProfileInstance EntityProfile { get; set; }
    public string Name { get; set; }
    public CombatAIProfile CombatAI { get; set; }
    public bool IsAlive => currHP > 0;
    public bool IsDefending = false; // TODO: hmmmm.... gotta be a better way to manage lmao

    [Header("Battle Info")]
    public int level => EntityProfile.Level;
    public int currHP;
    public int currMana;
    public List<SpecialMove> KnownMoves { get; private set; }
    public StatusConditionInstance statusCondition;
    public StatBlock RuntimeStats; // protected or private?

    [Header("Computations")]
    private int pendingDamage;
    public void ComputeIncomingDamage(int _amount) => pendingDamage += _amount;

    [Header("Stats")]
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
        EntityProfile = _profile;
        Name = _profile.Entity.displayName;
        currHP = _profile.CurrHP;
        currMana = _profile.CurrMana;
        statusCondition = _profile.CurrStatusCondition;
        KnownMoves = new List<SpecialMove>(_profile.KnownMoves);
        RuntimeStats = new StatBlock(_profile.Entity.baseStats);
    }
    #endregion


    #region Battle Operations

    public void ApplyDamage()
    {
        // All damage should be queued and evaluated before applied?
        // TODO: Damage should be influenced by entity stats
        int finalDmg = IsDefending ? pendingDamage / 2 : pendingDamage;
        finalDmg = Mathf.Min(finalDmg, currHP);
        currHP -= finalDmg;
        pendingDamage = 0;

        OnHealthChanged?.Invoke(this);
    }

    public void Heal(int amount)
    {
        // TODO: Change all Enitty.baseStats instances to grab from Profile
        if (currHP >= EntityProfile.Stats.maxHP)
            return;
        amount = Mathf.Clamp(amount, 0, EntityProfile.Stats.maxHP - currHP);

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
        if (currMana >= EntityProfile.Stats.maxMana)
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
        currHP = EntityProfile.Stats.maxHP;
        currMana = EntityProfile.Stats.maxMana;
        RemoveStatusEffect();
    }
    #endregion

}
