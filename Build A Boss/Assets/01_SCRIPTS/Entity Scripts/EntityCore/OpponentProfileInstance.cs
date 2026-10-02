using UnityEngine;

public class OpponentProfileInstance : EntityProfileInstance
{
    public OpponentData Opponent;
    public OpponentAITypes AIType => Opponent.combatAIType;

    public OpponentProfileInstance(OpponentData _opponentClass) : base(_opponentClass)
    {
        Opponent = _opponentClass;
    }
}
