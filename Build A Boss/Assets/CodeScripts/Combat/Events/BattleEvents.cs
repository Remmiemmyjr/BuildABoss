using System;
using UnityEngine;

public static class BattleEvents
{
    public static event Action<OpponentProfileInstance> BattleRequested;
    public static Action BattleStarted;
    public static Action BattleEnded;

    public static void RequestBattle(OpponentProfileInstance _instance)
    {
        BattleRequested?.Invoke(_instance);
    }

    public static void ClearAllEventSubscribers()
    {
        BattleRequested = null;
        BattleStarted = null;
        BattleEnded = null;
    }
}
