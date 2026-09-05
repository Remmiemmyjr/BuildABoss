using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Build A Boss/Moves/Special Move")]
public class SpecialMove : ScriptableObject
{
    public string moveName;
    public DamageType damageType;

    [Header("Info")]
    [TextArea(3, 3)] public string description;
    [TextArea(3, 3)] public string describeResult;

    [Header("Stats")]
    public int power;
    public int accuracy;
    public int manaCost;
    public int duration;

    [Header("Status Conditions and Move Behaviors")]
    public bool usedOnSelf;
    public MoveBehavior behaviorEffect;
    public StatusConditionTypes statusCondition;

    // visual effect in combat
}
