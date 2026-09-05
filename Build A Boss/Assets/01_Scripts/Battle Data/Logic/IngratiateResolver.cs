using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;


public static class IngratiateResolver
{
    public static void UseIngratiate(Ingratiate _ingratiate, MinionClass _opponent)
    {
        if (_ingratiate == _opponent.ingratiatesThatWork.Find(item => item.ingratiateName == _ingratiate.ingratiateName))
        {
            BattleManager.Instance.AddApproval(_ingratiate.approvalBoost);
            Debug.Log($"{_opponent.displayName} loved it!!! :D");
        }
        else if (_ingratiate == _opponent.hatedIngratiates.Find(item => item.ingratiateName == _ingratiate.ingratiateName))
        {
            BattleManager.Instance.RemoveApproval(_ingratiate.approvalBoost);
            Debug.Log($"{_opponent.displayName} hated that...... >:(");
        }
        else
        {
            Debug.Log("It didnt do anything...");
        }
    }
}
