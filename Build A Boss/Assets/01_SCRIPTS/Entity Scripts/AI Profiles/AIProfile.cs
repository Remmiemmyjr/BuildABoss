using UnityEngine;

[CreateAssetMenu(menuName = "Build A Boss/ AI / Behavior AI Profile")]
public abstract class AIProfile : ScriptableObject
{
    //public abstract 
    PersonalityType personalityType;

    public abstract void OverworldBehavior();
}
