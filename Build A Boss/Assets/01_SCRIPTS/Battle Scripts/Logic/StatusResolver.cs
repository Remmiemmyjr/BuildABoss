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

    public static void OnAfterTurn(BattleEntity unit)
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
                unit.ComputeIncomingDamage(unit.EntityProfile.Stats.maxHP / 8);
                break;

            case StatusConditionTypes.Freezing:
                unit.ComputeIncomingDamage(unit.EntityProfile.Stats.maxHP / 8);
                break;

            case StatusConditionTypes.Poisoned:
                unit.ComputeIncomingDamage(unit.EntityProfile.Stats.maxHP / 8);
                break;

            case StatusConditionTypes.Bleeding:
                unit.ComputeIncomingDamage(unit.EntityProfile.Stats.maxHP / 8);
                break;

            case StatusConditionTypes.Paralyzed:
                unit.ComputeIncomingDamage(unit.EntityProfile.Stats.maxHP / 8);
                break;

            case StatusConditionTypes.Blinded:
                unit.ComputeIncomingDamage(unit.EntityProfile.Stats.maxHP / 8);
                break;


            // Mental Afflictions
            case StatusConditionTypes.Dread:
                unit.ComputeIncomingDamage(unit.EntityProfile.Stats.maxHP / 8);
                break;

            case StatusConditionTypes.Baffled:
                unit.ComputeIncomingDamage(unit.EntityProfile.Stats.maxHP / 8);
                break;

            case StatusConditionTypes.Depressed:
                unit.ComputeIncomingDamage(unit.EntityProfile.Stats.maxHP / 8);
                break;

            case StatusConditionTypes.Pissed:
                unit.ComputeIncomingDamage(unit.EntityProfile.Stats.maxHP / 8);
                break;

            case StatusConditionTypes.Cringe:
                unit.ComputeIncomingDamage(unit.EntityProfile.Stats.maxHP / 8);
                break;
        }
    }
}
