using UnityEngine;

[CreateAssetMenu(menuName = "Build A Boss/Identity/Personality")]
public class PersonalityTypeClass : ScriptableObject
{
    [Header("Personality Type")]
    public string personalityName;

    [Header("Impact")]
    public float recruitmentDifficultyModifier;
    public StatType buffStat;
    public float positiveModifier;
    public StatType debuffStat;
    public float negativeModifier;

    // AI Behavior
    // Dialogue Table
}

