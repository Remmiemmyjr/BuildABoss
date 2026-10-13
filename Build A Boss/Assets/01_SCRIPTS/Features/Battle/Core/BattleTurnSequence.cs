using System.Collections.Generic;
using UnityEngine;

public class BattleTurnSequence : MonoBehaviour
{
    [SerializeField] private TextWriter battleLogWriter;
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
        await battleLogWriter.PrintTurnMessage($"<+move><+grow><wave><color=#C988F0>{_opponent.Name}</color></wave> challenges you to a fight!</+grow></+move>", 1.5f);
    }

    public async Awaitable PromptSelection()
    {
        await battleLogWriter.PrintTurnMessage($"<+move><+grow><wave amplitude=1.5 upperiod=0.85 downperiod=0.85 crestwait=0.25 troughwait=0.25><pivot><grow>What will you do?</grow></pivot></wave></+grow></+move>", 0.05f);
    }

    public async Awaitable EndBattle(WhyBattleEnded _why, BattleEntity _opponent)
    {
        switch(_why)
        {
            case WhyBattleEnded.Defeat:
                BattleSFXPlayer.Instance.PlayLoseSFX();
                await battleLogWriter.PrintTurnMessage($"<+move><+grow>You were defeated by <wave><color=#C988F0>{_opponent.Name}</color></wave><!delay=0.65><!delay=0.65 for=punct type=pct>...</+grow></+move>", 1.5f);
                break;

            case WhyBattleEnded.Victory:
                BattleSFXPlayer.Instance.PlayWinSFX();
                await battleLogWriter.PrintTurnMessage($"<+move><+grow>You won!</+grow></+move>", 1.5f);
                break;

            case WhyBattleEnded.Recruit:
                BattleSFXPlayer.Instance.PlayRecruitSFX();
                await battleLogWriter.PrintTurnMessage($"<+move><+grow><!delay=0.75><!delay=0.75 for=punct type=pct>...<!delay=0.05><!delay=0.05 for=punct type=pct><!wait=1>\nYou recruited <wave><color=#C988F0>{_opponent.Name}</color></wave>!</+grow></+move>", 1.5f);
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
        await battleLogWriter.PrintTurnMessage($"<+move><+grow>{_action.user.Name} attacked {_action.target.Name} </+grow></+move>", 1.5f);

        // 2. Compute
        _action.target.QueueDamage(_action.user.AttackDamage);
        // 3. Apply & Update
        BattleSFXPlayer.Instance.PlayAttackSFX();
        _action.target.ApplyDamage();
    }

    private async Awaitable ExecuteDefend(BattleAction _action)
    {
        _action.user.IsDefending = true;
        // 1. Describe
        await battleLogWriter.PrintTurnMessage($"<+move><+grow>{_action.user.Name} defended against {_action.target.Name} </+grow></+move>", 1.5f);
        // 2. Apply
        BattleSFXPlayer.Instance.PlayDefendSFX();
    }

    private async Awaitable ExecuteSpecialMove(BattleAction _action)
    {
        // 1. Describe
        await battleLogWriter.PrintTurnMessage($"<+move><+grow>{_action.user.Name} used {_action.move.moveName}</+grow></+move>", 1.5f);
        // 2. Apply
        bool moveSuccess = MoveResolver.UseMove(_action.user, _action.target, _action.move);
        if (moveSuccess && _action.target.statusCondition != null)
        {
            BattleSFXPlayer.Instance.PlaySpecialMoveSFX();
            await battleLogWriter.PrintTurnMessage($"<+move><+grow>{_action.target.Name} {_action.target.statusCondition.appliedMessage}</+grow></+move>", 1.5f);
        }
        else
            await battleLogWriter.PrintTurnMessage($"<+move><+grow><!delay=0.5><!delay=0.5 for=punct type=pct>...<!delay=0.05><!delay=0.05 for=punct type=pct><!wait=0.5>but it failed!</+grow></+move>", 1.5f);
       
        Debug.Log($"pending damage should be 0 after using special move. It is currently {_action.target.pendingDamage}");
    }

    private async Awaitable ExecuteIngratiate(BattleAction _action)
    {
        // 1. Describe
        await battleLogWriter.PrintTurnMessage($"<+move><+grow>{_action.user.Name} used {_action.ingratiate.ingratiateName} to try and boost their approval<!delay=0.65><!delay=0.65 for=punct type=pct>...</+grow></+move>", 1.5f);
        // 2. Apply
        BattleSFXPlayer.Instance.PlayIngratiateSFX();
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

            if (!StatusResolver.IsStatusStillActive(entity))
            {
                await battleLogWriter.PrintTurnMessage($"<+move><+grow>{entity.Name} {entity.statusCondition.clearedMessage}</+grow></+move>", 1.5f);
                entity.RemoveStatusEffect();
            }
            else
            {
                await battleLogWriter.PrintTurnMessage($"<+move><+grow>{entity.Name} {entity.statusCondition.recurringMessage}</+grow></+move>", 1.5f);
                entity.ApplyDamage();
            }
        }
    }
}
