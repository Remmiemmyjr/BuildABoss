using UnityEngine;

public abstract class OpponentClass : EntityClass
{
    public int xpReward { get; private set; }
    public PersonalityType personality;
    public OpponentAITypes combatAIType;
}
