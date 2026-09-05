using System;
using UnityEngine;

public static class BattleEvents
{
    public static event Action<OpponentProfileInstance, bool> BattleRequested;
    public static Action BattleStarted;
    public static Action BattleEnded;

    public static void RequestBattle(OpponentProfileInstance _instance, bool _isHero)
    {
        BattleRequested?.Invoke(_instance, _isHero);
    }

    public static void ClearAllEventSubscribers()
    {
        BattleRequested = null;
        BattleStarted = null;
        BattleEnded = null;
    }
}
