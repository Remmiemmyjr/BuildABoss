using System.Collections.Generic;
using UnityEngine;

public class DB_StatusCondition
{
    // TODO: need a damage multiplier based on entity weakness to damage types
    public static Dictionary<StatusConditionTypes, StatusCondition> Conditions = new Dictionary<StatusConditionTypes, StatusCondition>()
    {
        // Key: ConditionType           // Value: Condition
        // PHYSICAL CONDITIONS =================================================
        [StatusConditionTypes.Burned] = new StatusCondition()
        {
            effectName = "Burned",
            statusEffect = StatusConditionTypes.Burned,
            duration = 1,
            probability = 1f,
            statusAppliedMessage = " was inflicted with burning!",
            reocurringEffectMessage = " was burned!",
            effectClearedMessage = " is no longer burning."
        },

        [StatusConditionTypes.Freezing] = new StatusCondition()
        {
            effectName = "Freezing",

        },

        [StatusConditionTypes.Poisoned] = new StatusCondition()
        {
            effectName = "Poisoned",

        },

        [StatusConditionTypes.Bleeding] = new StatusCondition()
        {
            effectName = "Bleeding",

        },

        [StatusConditionTypes.Paralyzed] = new StatusCondition()
        {
            effectName = "Paralyzed",

        },

        [StatusConditionTypes.Blinded] = new StatusCondition()
        {
            effectName = "Blinded",

        },


        // Key: ConditionType           // Value: Condition
        // MENTAL CONDITIONS ===================================================
        [StatusConditionTypes.Dread] = new StatusCondition()
        {
            effectName = "Dread",

        },

        [StatusConditionTypes.Baffled] = new StatusCondition()
        {
            effectName = "Baffled",

        },

        [StatusConditionTypes.Depressed] = new StatusCondition()
        {
            effectName = "Depressed",

        },

        [StatusConditionTypes.Pissed] = new StatusCondition()
        {
            effectName = "Pissed",

        },

        [StatusConditionTypes.Cringe] = new StatusCondition()
        {
            effectName = "Cringe",

        },
    };
}

