using UnityEngine;

[CreateAssetMenu(menuName = "Build A Boss/Moves/Special Move")]
public class SpecialMove : ScriptableObject
{
    public string moveName;
    public DamageType damageType;

    [Header("Info")]
    [TextArea(3, 3)] public string description;
    [TextArea(3, 3)] public string resultMessage;

    [Header("Stats")]
    public int power;
    public int manaCost;

    [Header("Status Conditions and Move Behaviors")]
    public bool usedOnSelf;
    public MoveBehavior behaviorEffect;
    public StatusConditionDefinition statusCondition;

    // visual effect in combat
}
