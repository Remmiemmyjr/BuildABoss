using UnityEngine;

public static class StatusResolver
{
    public static bool TryApplyStatus(BattleEntity _target, StatusConditionDefinition _condition)
    {
        // TODO: calculate probability? do i even still wanna have that

        // if the target doesnt already have a status condition
        if (_target.statusCondition == null)
        {
            _target.ApplyStatusCondition(_condition);
            return true;
        }
        return false;
    }

    public static bool IsStatusStillActive(BattleEntity _target)
    {
        if (_target.statusCondition == null)
            return false;

        if (_target.remainingAfflictedTurns > 0)
        {
            --_target.remainingAfflictedTurns;
            _target.statusCondition.OnTurnEnd(_target);
            Debug.Log($"{_target.Name} will have {_target.statusCondition.effectName} for {_target.remainingAfflictedTurns} turns");
            return true;
        }

        else
        {
            Debug.Log($"{_target.Name}{_target.statusCondition.clearedMessage}!");
            return false;
        }
    }
}
