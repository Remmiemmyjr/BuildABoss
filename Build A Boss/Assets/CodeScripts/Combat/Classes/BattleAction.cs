using UnityEngine;

public enum BattleActionType
{
    Attack,
    Defend,
    SpecialMove,
    Ingratiate,
    Flee
}

public class BattleAction
{
    public BattleActionType type;
    public BattleEntity user;
    public BattleEntity target;
    public SpecialMove move;
    public Ingratiate ingratiate;

    public BattleAction(BattleActionType _type, BattleEntity _user, BattleEntity _target = null, SpecialMove _move = null, Ingratiate _ingratiate = null)
    {
        type = _type;
        user = _user;
        target = _target;
        move = _move;
        ingratiate = _ingratiate;
    }
}
