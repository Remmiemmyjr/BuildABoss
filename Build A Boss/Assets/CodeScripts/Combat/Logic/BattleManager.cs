using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    #region Variables
    public static BattleManager Instance;
    public BattleContext Context { get; private set; }
    public BattleState battleState { get; private set; }
    public int approval;
    public delegate void OnApprovalChanged();
    public OnApprovalChanged ApprovalChanged;

    [Header("Entity References")]
    [SerializeField] BattleEntity playerUnit; 
    [SerializeField] BattleEntity opponentUnit;

    [Header("Dialogue References")]
    [SerializeField] GameObject dialogueBox;
    public TMP_Text dialogue; // TODO: will probably have battle log manage this instead

    [Header("Choice & Action Management")]
    private List<BattleAction> pendingActions = new();
    private Queue<BattleAction> actionQueue = new();

    [Header("Turns")]
    public int turnNumber;
    public event Action<BattleMenuState> NewTurn;
    #endregion



    // ----------------------------------------------------------------------------------------------------------
    #region Initialize Battle
    private void Awake()
    {
        Instance = this;
    }

    private void HandleBattleRequest(OpponentProfileInstance _instance)
    {
        StartCoroutine(BeginBattle(_instance));
    }

    private void OnEnable()
    {
        BattleEvents.BattleRequested += HandleBattleRequest;
    }

    private void OnDisable()
    {
        BattleEvents.BattleRequested -= HandleBattleRequest;
    }

    // collide with entity calls InitiateBattle?
    // will want an EntityInstance param
    // Temporarily a coroutine, should not have to be if I can make the battle log help with state delays
    public IEnumerator BeginBattle(OpponentProfileInstance _opponentProfile)
    {
        turnNumber = 0;
        approval = 0;

        BattleEntity player = BattleEntityFactory.CreateFromPlayerBoss(PlayerDataManager.Instance.Boss);
        BattleEntity opponent = BattleEntityFactory.CreateFromOpponent(_opponentProfile);

        SetPlayerUnit(player);
        SetOpponentUnit(opponent);

        Context = new BattleContext(player, opponent);

        BattleEvents.BattleStarted.Invoke();

        transform.Find("BattleCanvas/PlayerHUD").GetComponent<UnitInfoHUD>().SetData(player);
        transform.Find("BattleCanvas/OpponentHUD").GetComponent<UnitInfoHUD>().SetData(opponent);
        transform.Find("BattleCanvas/CombatPanelHUD/ApprovalBar").GetComponent<ApprovalBarDisplay>().SetData();

        GetComponent<InitSpMoveHUD>().InitSpMoveButtons(player.KnownMoves);
        GetComponent<InitIngratiateHUD>().InitIngratiateButtons(PlayerDataManager.Instance.Boss.KnownIngratiates);

        yield return StartCoroutine(TextTyper.TypeText(dialogue, $"{opponent.Entity.displayName} challenges you to a fight!"));

        StartPlayerSelection();
    }
    #endregion



    // ----------------------------------------------------------------------------------------------------------
    #region Battle Turn Pipeline

    public void StartPlayerSelection()
    {
        NewTurn.Invoke(BattleMenuState.SelectionMenu);
        StartCoroutine(TextTyper.TypeText(dialogue, $"What will you do?"));
        battleState = BattleState.PlayerChoosingAction;
    }

    public void ConstructAction(BattleActionType _type, SpecialMove _move = null, Ingratiate _ingratiate = null)
    {
        BattleAction action = new BattleAction(_type, playerUnit, opponentUnit, _move, _ingratiate);

        SubmitPlayerAction(action);
    }

    public void SubmitPlayerAction(BattleAction _action)
    {
        if (battleState != BattleState.PlayerChoosingAction)
            return;

        pendingActions.Add(_action);

        battleState = BattleState.OpponentChoosingAction;

        ChooseOpponentAction();
        BuildActionQueue();
        ResolveActionQueue();
    }

    void ChooseOpponentAction()
    {
        // TODO: Need to change this to an opponenet AI evaluation
        BattleAction action = new BattleAction(BattleActionType.Attack, opponentUnit, playerUnit);

        pendingActions.Add(action);
    }

    void BuildActionQueue()
    {
        // TODO: Should Defend always go first, or fail if it's last in order?
        pendingActions.Sort((a, b) => a.user.Speed);

        pendingActions.Reverse();
        actionQueue = new Queue<BattleAction>(pendingActions);
    }

    private void ResolveActionQueue()
    {
        battleState = BattleState.ResolveTurn;

        if (actionQueue.Count <= 0)
            return;

        BattleAction action;

        if (actionQueue.Count > 0)
        {
            action = actionQueue.Dequeue();

            if (!action.user.IsAlive)
                return;

            ResolveAction(action);
        }
    }

    void ResolveAction(BattleAction _action)
    {
        Context.SetActionExecuted(_action);
        switch (_action.type)
        {
            case BattleActionType.Attack:
                StartCoroutine(UseAttack(_action.user, _action.target));
                break;
            case BattleActionType.Defend:
                StartCoroutine(UseDefend(_action.user, _action.target));
                break;
            case BattleActionType.SpecialMove:
                Context.SetSpecialMoveExecuted(_action.move);
                StartCoroutine(UseSpecialMove(_action.user, _action.target, _action.move));
                break;
            case BattleActionType.Ingratiate:
                Context.SetIngratiateExecuted(_action.ingratiate);
                StartCoroutine(UseIngratiate(_action.user, _action.target, _action.ingratiate));
                break;
        }
    }

    void EndTurn()
    {
        // Proceed to Next Turn
        if (opponentUnit.IsAlive && playerUnit.IsAlive)
        {
            turnNumber++;

            pendingActions.Clear();
            actionQueue.Clear();

            playerUnit.IsDefending = false;
            opponentUnit.IsDefending = false;

            StartCoroutine(HandleAfterEffects());
        }
        // Otherwise battle is over
        else
        {
            battleState = BattleState.Over;

            if (!playerUnit.IsAlive)
            {
                StartCoroutine(EndBattle(WhyBattleEnded.Defeat));
            }

            if (!opponentUnit.IsAlive)
            {
                StartCoroutine(EndBattle(WhyBattleEnded.Victory));
            }
        }
    }

    private IEnumerator HandleAfterEffects()
    {
        if (playerUnit.statusCondition != null || opponentUnit.statusCondition != null)
        {
            battleState = BattleState.AfterEffects;
            // TODO: get rid of this
            yield return TextTyper.TypeText(dialogue, $"{opponentUnit.Entity.name} was burned!");

            StatusResolver.OnAfterTurn(playerUnit, Context);
            StatusResolver.OnAfterTurn(opponentUnit, Context);

            yield return new WaitForSeconds(0.5f);

        }
        if (!playerUnit.IsAlive || !opponentUnit.IsAlive)
            EndTurn();
        else
            StartPlayerSelection();
    }
    #endregion



    // ----------------------------------------------------------------------------------------------------------
    #region Use Action
    private IEnumerator UseAttack(BattleEntity _user, BattleEntity _reciever)
    {
        yield return TextTyper.TypeText(dialogue, $"{_user.Entity.displayName} attacked {_reciever.Entity.name}!");
        // TODO: temporary stupid logic for recieving defend
        _reciever.TakeDamage(_reciever.IsDefending ? _user.Entity.baseStats.attackDamage / 2 : _user.Entity.baseStats.attackDamage);

        if (actionQueue.Count <= 0 || !_reciever.IsAlive)
            EndTurn();
        else
        {
            ResolveActionQueue();
        }
    }

    private IEnumerator UseDefend(BattleEntity _user, BattleEntity _reciever)
    {
        // TODO: need better defend method later
        _user.IsDefending = true;
        yield return TextTyper.TypeText(dialogue, $"{_user.Entity.displayName} defended against {_reciever.Entity.name}!");
        
        if (actionQueue.Count <= 0)
            EndTurn();
        else
            ResolveActionQueue();
    }

    private IEnumerator UseSpecialMove(BattleEntity _user, BattleEntity _reciever, SpecialMove _move)
    {
        yield return TextTyper.TypeText(dialogue, $"{_user.Entity.displayName} used {_move.moveName}!");
 
        bool moveSuccess = MoveResolver.UseMove(_user, _reciever, _move);

        yield return TextTyper.TypeText(dialogue, $"{_reciever.Entity.name} was inflicted with burning!");

        if (actionQueue.Count <= 0 || !_reciever.IsAlive)
            EndTurn();
        else
            ResolveActionQueue();
    }

    private IEnumerator UseIngratiate(BattleEntity _user, BattleEntity _reciever, Ingratiate _ingratiate)
    {
        yield return TextTyper.TypeText(dialogue, $"{_user.Entity.displayName} tried to boost their Approval!");

        // evaluate if reciever liked it or not
        MinionClass minion = (MinionClass)_reciever.Entity;
        if(minion)
        {
            IngratiateResolver.UseIngratiate(_ingratiate, minion);
        }

        if (actionQueue.Count <= 0 || !_reciever.IsAlive)
            EndTurn();
        else
            ResolveActionQueue();
    }    
    #endregion



    // ----------------------------------------------------------------------------------------------------------
    #region End Battle
    public IEnumerator EndBattle(WhyBattleEnded _why)
    {
        SyncBattleProfileChanges.SaveBackToPlayerBoss(playerUnit);

        switch (_why)
        {
            case WhyBattleEnded.Defeat:
                yield return StartCoroutine(TextTyper.TypeText(dialogue, $"You lost to {GetOpponentUnit().Entity.displayName} :("));
                break;

            case WhyBattleEnded.Victory:
                yield return StartCoroutine(TextTyper.TypeText(dialogue, $"You won!"));
                break;

            case WhyBattleEnded.Recruit:
                yield return StartCoroutine(TextTyper.TypeText(dialogue, $"You recruited {GetOpponentUnit().Entity.displayName}!"));
                break;
        }

        BattleEvents.BattleEnded?.Invoke();
        ResetBattleManager();
    }

    private void ResetBattleManager()
    {
        playerUnit = null;
        opponentUnit = null;
        turnNumber = 0;
        pendingActions.Clear();
        actionQueue.Clear();
    }
    #endregion



    // ----------------------------------------------------------------------------------------------------------
    #region External Helpers
    public BattleEntity GetPlayerUnit()
    {
        return playerUnit;
    }

    public void SetPlayerUnit(BattleEntity _unit)
    {
        playerUnit = _unit;
    }

    public BattleEntity GetOpponentUnit()
    {
        return opponentUnit;
    }

    public void SetOpponentUnit(BattleEntity _unit)
    {
        opponentUnit = _unit;
    }

    public BattleAction GetAction()
    {
        return actionQueue.Peek();
    }

    public void AddApproval(int amount)
    {
        approval += amount;
        ApprovalChanged?.Invoke();
    }

    public void RemoveApproval(int amount)
    {
        approval -= amount;
        ApprovalChanged?.Invoke();
    }
    #endregion

}
