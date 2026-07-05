using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

// A lightweight packet of information about the current battle state that systems/effects can reference without needing an instance to the operational BM.
public class BattleContext
{
    // Can make lists later (allies, opponents if there are multiple participants in a battle
    public BattleEntity Player { get; private set; }
    public BattleEntity Opponent { get; private set; }

    public BattleAction actionExecuted;
    public SpecialMove specialMoveExecuted;
    public Ingratiate ingratiateExecuted;

    public int CurrentTurnNum => BattleManager.Instance.turnNumber;
    public int Approval => BattleManager.Instance.approval;

    public BattleContext(BattleEntity _player, BattleEntity _opponent)
    {
        Player = _player;
        Opponent = _opponent;
    }

    public void SetActionExecuted(BattleAction _action)
    {
        actionExecuted = _action;
    }

    public void SetSpecialMoveExecuted(SpecialMove _move)
    {
        specialMoveExecuted = _move;
    }

    public void SetIngratiateExecuted(Ingratiate _move)
    {
        ingratiateExecuted = _move;
    }
}
