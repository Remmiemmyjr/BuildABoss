using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleSequencer : MonoBehaviour
{
    [SerializeField] private BattleLogWriter battleLogWriter;

    public async Awaitable RunTurn(Queue<BattleAction> _actions)
    {
        while (_actions.Count > 0)
        {
            var action = _actions.Dequeue();
            if (!action.user.IsAlive) continue; // just break here too not continue i think?

            await ExecuteAction(action);
            if (!action.target.IsAlive) break;
        }

        await HandleAfterEffects();
    }

    public async Awaitable StartBattle(BattleEntity _opponent)
    {
        await battleLogWriter.PrintTurnMessage($"{_opponent.Name} challenges you to a fight!");
    }

    public async Awaitable PromptSelection()
    {
        await battleLogWriter.PrintTurnMessage($"What will you do?");
    }

    public async Awaitable EndBattle(WhyBattleEnded _why, BattleEntity _opponent)
    {
        switch(_why)
        {
            case WhyBattleEnded.Defeat:
                await battleLogWriter.PrintTurnMessage($"You were defeated by {_opponent.Name}");
                break;

            case WhyBattleEnded.Victory:
                await battleLogWriter.PrintTurnMessage($"You won!");
                break;

            case WhyBattleEnded.Recruit:
                await battleLogWriter.PrintTurnMessage($"You recruited {_opponent.Name}");
                break;
        }
    }

    private Awaitable ExecuteAction(BattleAction _action) => _action.type switch
    {
        BattleActionType.Attack => ExecuteAttack(_action),
        BattleActionType.Defend => ExecuteDefend(_action),
        BattleActionType.SpecialMove => ExecuteSpecialMove(_action),
        BattleActionType.Ingratiate => ExecuteIngratiate(_action),
        _ => default
    };

    private async Awaitable ExecuteAttack(BattleAction _action)
    {
        // 1. Describe
        await battleLogWriter.PrintTurnMessage($"{_action.user.Name} attacked {_action.target.Name}");

        // 2. Compute
        _action.target.ComputeIncomingDamage(_action.user.AttackDamage);
        // 3. Apply & Update
        _action.target.ApplyDamage();
    }

    private async Awaitable ExecuteDefend(BattleAction _action)
    {
        _action.user.IsDefending = true;
        // 1. Describe
        await battleLogWriter.PrintTurnMessage($"{_action.user.Name} defended against {_action.target.Name}");
        // 2. Apply
    }

    private async Awaitable ExecuteSpecialMove(BattleAction _action)
    {
        // 1. Describe
        await battleLogWriter.PrintTurnMessage($"{_action.user.Name} used {_action.move.moveName}");
        // 2. Apply
        bool moveSuccess = MoveResolver.UseMove(_action.user, _action.target, _action.move);
    }

    private async Awaitable ExecuteIngratiate(BattleAction _action)
    {
        // 1. Describe
        await battleLogWriter.PrintTurnMessage($"{_action.user.Name} used {_action.ingratiate.ingratiateName} to try and boost their approval!");
        // 2. Apply
        MinionData minion = (MinionData)_action.target.EntityProfile.Entity;
        if (minion)
        {
            IngratiateResolver.UseIngratiate(_action.ingratiate, minion);
        }
    }

    private async Awaitable HandleAfterEffects()
    {
        foreach(var entity in new[] {BattleManager.Instance.GetPlayerUnit(), BattleManager.Instance.GetOpponentUnit()})
        {
            if (entity.statusCondition == null) continue;
            
            await battleLogWriter.PrintTurnMessage($"{entity.Name} was affected by {entity.statusCondition}");
            StatusResolver.OnAfterTurn(entity);
            entity.ApplyDamage();
        }
    }
}
