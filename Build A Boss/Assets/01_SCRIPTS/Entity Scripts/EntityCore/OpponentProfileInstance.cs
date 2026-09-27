using UnityEngine;

public class OpponentProfileInstance : EntityProfileInstance
{
    public OpponentClass MyOpponentClass;
    public OpponentAITypes AIType => MyOpponentClass.combatAIType;

    public OpponentProfileInstance(OpponentClass _opponentClass) : base(_opponentClass)
    {
        MyOpponentClass = _opponentClass;
    }
}
