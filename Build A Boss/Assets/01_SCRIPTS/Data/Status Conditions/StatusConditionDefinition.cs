using UnityEngine;

[CreateAssetMenu(fileName = "StatusCondition", menuName = "Build A Boss/Status Condition")]
public class StatusConditionDefinition : ScriptableObject
{
    public string effectName;

    public int duration;
    [Range(0,1)] public float probability = 1f;
    [Range(0,1)] public float damageFractionPerTurn = 0.1f;

    public string appliedMessage, recurringMessage, clearedMessage;

    public virtual int OnTurnEnd(IBattleUnit _unit)
    {
        return _unit.QueueDamage(Mathf.RoundToInt(_unit.MaxHP * damageFractionPerTurn));
    }
}
