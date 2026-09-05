using UnityEngine;

public abstract class OpponentClass : EntityClass
{
    public int xpReward { get; private set; }
    public PersonalityTypeClass personality;
    public OpponentAITypes aiType;
}
