
public static class MoveResolver
{
    public static bool UseMove(BattleEntity _user, BattleEntity _target, SpecialMove _move)
    {
        if (_user.currMana < _move.manaCost)
            return false;

        // visual effect
        if (_move.statusCondition != null)
        {
            if (!StatusResolver.TryApplyStatus(_target, _move.statusCondition))
                return false;
        }

        _user.UseMana(_move.manaCost);
        if (_move.power > 0)
        {
            _target.QueueDamage(_move.power / 3);
            _target.ApplyDamage();
        }

        // if the move has a status condition to apply...


        //foreach (MoveBehavior effect in move.effects)
        //{
        //    if (effect == null)
        //        continue;

        //    effect.Perform(user, target, move);
        //}

        return true;
    }
    // false = move could not be used
    // true = move performed successfully and concluded
}
