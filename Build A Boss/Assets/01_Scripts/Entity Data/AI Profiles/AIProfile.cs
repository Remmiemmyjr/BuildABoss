using UnityEngine;

[CreateAssetMenu(menuName = "Build A Boss/ AI / Behavior AI Profile")]
public abstract class AIProfile : ScriptableObject
{
    //public abstract 
    PersonalityTypeClass personalityType;

    public abstract void OverworldBehavior();
}
