using UnityEngine;

public static class MoveResolver
{
    public static bool UseMove(BattleEntity _user, BattleEntity _target, SpecialMove _move)
    {
        if (_user.currMana < _move.manaCost)
            return false;

        // visual effect

        _user.UseMana(_move.manaCost);
        if (_move.power > 0)
        {
            _target.TakeDamage(_move.power / 3);
        }

        //foreach (MoveBehavior effect in move.effects)
        //{
        //    if (effect == null)
        //        continue;

        //    effect.Perform(user, target, move);
        //}

        if (_move.statusCondition != StatusConditionTypes.None)
            StatusResolver.TryApplyStatus(_target, _move.statusCondition);

        return true;
    }
    // false = move could not be used
    // true = move performed successfully and concluded
}
