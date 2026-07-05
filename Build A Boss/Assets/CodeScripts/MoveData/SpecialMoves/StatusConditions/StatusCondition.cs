using System;
using UnityEngine;
using System.Collections;
using System.Linq;

//[CreateAssetMenu(menuName = "Build A Boss/Moves/Status Effects")]
public class StatusCondition //: ScriptableObject
{
    public float probability = 1f;
    public string effectName;

    public StatusConditionTypes statusEffect;
    public int duration;

    public string statusAppliedMessage;
    public string reocurringEffectMessage;
    public string effectClearedMessage;
}
