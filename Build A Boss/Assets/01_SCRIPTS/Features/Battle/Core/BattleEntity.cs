using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleEntity : IBattleUnit
{
    #region Variables
    public EntityProfileInstance EntityProfile { get; set; }
    public string Name { get; set; }
    public CombatAIProfile CombatAI { get; set; }
    public bool IsAlive => currHP > 0;
    public bool IsDefending = false;
    public int remainingAfflictedTurns;

    [Header("Battle Info")]
    public int currHP;
    public int currMana;
    public List<SpecialMove> KnownMoves { get; private set; }
    public StatusConditionDefinition statusCondition;
    public StatBlock RuntimeStats; // protected or private?

    [Header("Computations")]
    public int pendingDamage { get; private set; }
    public int QueueDamage(int _amount) => pendingDamage += _amount;


    [Header("Stats")]
    // Lambda Getters of Base Stats, for ease of access (could just use RuntimeStats)
    public int AttackDamage => GetStatWithModifier(StatType.AttackDamage);
    public int Defense => GetStatWithModifier(StatType.Defense);
    public int Speed => GetStatWithModifier(StatType.Speed);
    public int SpecialPower => GetStatWithModifier(StatType.SpecialPower);

    public int MaxHP => EntityProfile.Stats.maxHP;
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
        pendingDamage = 0;
    }
    #endregion


    #region Battle Operations
    public void ApplyDamage()
    {
        // All damage should be queued and evaluated before applied?
        // TODO: Damage should be influenced by entity stats
        Debug.Log($"pending damage is {pendingDamage}");
        int finalDmg = IsDefending ? pendingDamage / 2 : pendingDamage;
        finalDmg = Mathf.Min(finalDmg, currHP);
        currHP -= finalDmg;
        pendingDamage = 0;
        Debug.Log($"final damage is {finalDmg}");

        OnHealthChanged?.Invoke(this);
    }

    public void Heal(int amount)
    {
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

    public void ApplyStatusCondition(StatusConditionDefinition _statusCondition)
    {
        remainingAfflictedTurns = _statusCondition.duration;
        statusCondition = _statusCondition;
        Debug.Log($"{Name} will have {_statusCondition.effectName} for {remainingAfflictedTurns} turns");
    }

    public void RemoveStatusEffect()
    {
        remainingAfflictedTurns = 0;
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
        pendingDamage = 0;
        remainingAfflictedTurns = 0;
        RemoveStatusEffect();
    }


    #endregion

}
