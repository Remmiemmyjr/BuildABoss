using UnityEngine;

public class StatusConditionInstance
{
    public StatusConditionTypes definition;
    public int turnsRemaining;

    public StatusConditionInstance(StatusConditionTypes _definition, int _turnsRemaining)
    {
        definition = _definition;
        turnsRemaining = _turnsRemaining;
    }
}
