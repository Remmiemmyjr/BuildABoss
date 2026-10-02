using UnityEngine;

public abstract class OpponentData : EntityData
{
    public int xpReward { get; private set; }
    public PersonalityType personality;
    public OpponentAITypes combatAIType;
}
