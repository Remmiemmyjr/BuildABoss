using UnityEngine;

public static class StatusResolver
{
    public static void TryApplyStatus(BattleEntity _target, StatusConditionTypes _statusType)
    {
        if (_statusType == StatusConditionTypes.None)
            return;
        if (_target.statusCondition != null)
            return;

        StatusCondition condition = DB_StatusCondition.Conditions[_statusType];

        if (Random.value > condition.probability)
            return;

        _target.ApplyStatusEffect(_statusType);
    }

    public static void OnAfterTurn(BattleEntity unit, BattleContext context)
    {
        if (unit.statusCondition == null)
            return;

        StatusConditionInstance condition = unit.statusCondition;
        
        if (condition.turnsRemaining <= 0)
        {
            unit.RemoveStatusEffect();
            Debug.Log("Opponent is no longer burned!");
            return;
        }

        condition.turnsRemaining--;

        switch (condition.definition)
        {
            // Physical Afflictions
            case StatusConditionTypes.Burned:
                unit.TakeDamage(unit.Entity.baseStats.maxHP / 8);
                break;

            case StatusConditionTypes.Freezing:
                unit.TakeDamage(unit.Entity.baseStats.maxHP / 8);
                break;

            case StatusConditionTypes.Poisoned:
                unit.TakeDamage(unit.Entity.baseStats.maxHP / 8);
                break;

            case StatusConditionTypes.Bleeding:
                unit.TakeDamage(unit.Entity.baseStats.maxHP / 8);
                break;

            case StatusConditionTypes.Paralyzed:
                unit.TakeDamage(unit.Entity.baseStats.maxHP / 8);
                break;

            case StatusConditionTypes.Blinded:
                unit.TakeDamage(unit.Entity.baseStats.maxHP / 8);
                break;


            // Mental Afflictions
            case StatusConditionTypes.Dread:
                unit.TakeDamage(unit.Entity.baseStats.maxHP / 8);
                break;

            case StatusConditionTypes.Baffled:
                unit.TakeDamage(unit.Entity.baseStats.maxHP / 8);
                break;

            case StatusConditionTypes.Depressed:
                unit.TakeDamage(unit.Entity.baseStats.maxHP / 8);
                break;

            case StatusConditionTypes.Pissed:
                unit.TakeDamage(unit.Entity.baseStats.maxHP / 8);
                break;

            case StatusConditionTypes.Cringe:
                unit.TakeDamage(unit.Entity.baseStats.maxHP / 8);
                break;
        }
    }
}
