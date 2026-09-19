using System.Collections.Generic;
using UnityEngine;

public class DB_CombatAI
{
    public static Dictionary<OpponentAITypes, CombatAIProfile> GetProfile = new Dictionary<OpponentAITypes, CombatAIProfile>()
    {
        [OpponentAITypes.BasicMinion] = new MinionCombatAI()
    };
}
