using UnityEngine;

public class OpponentProfileInstance : EntityProfileInstance
{
    public OpponentClass opponentClass;
    public OpponentAITypes AIType => opponentClass.aiType;

    public OpponentProfileInstance(OpponentClass _opponentClass) : base(_opponentClass)
    {
        opponentClass = _opponentClass;
    }
}
