using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
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
    [SerializeField] private BattleSequencer sequencer;

    [Header("Choice & Action Management")]
    private List<BattleAction> pendingActions = new();
    private Queue<BattleAction> actionQueue = new();
    public MinionProfileInstance temporaryRecruit;

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

    private void HandleBattleRequest(OpponentProfileInstance _instance, bool _isHero)
    {
        //StartCoroutine(BeginBattle(_instance, _isHero));
        BeginBattle(_instance, _isHero);
    }

    private void OnEnable()
    {
        BattleEvents.BattleRequested += HandleBattleRequest;
    }

    private void OnDisable()
    {
        BattleEvents.BattleRequested -= HandleBattleRequest;
        BattleEvents.ClearAllEventSubscribers();
    }

    // Temporarily a coroutine, should not have to be if I can make the battle log help with state delays
    public async void BeginBattle(OpponentProfileInstance _opponentProfile, bool _isHero)
    {
        turnNumber = 0;
        approval = 0;

        BattleEntity player = BattleEntityFactory.CreateFromPlayerBoss(PlayerRefGetter.Instance.PlayerInstance);
        BattleEntity opponent = BattleEntityFactory.CreateFromOpponent(_opponentProfile);

        SetPlayerUnit(player);
        SetOpponentUnit(opponent);

        BattleEvents.BattleStarted.Invoke();

        transform.Find("BattleCanvas/PlayerHUD").GetComponent<UnitInfoHUD>().SetData(player);
        transform.Find("BattleCanvas/OpponentHUD").GetComponent<UnitInfoHUD>().SetData(opponent);
        transform.Find("BattleCanvas/CombatPanelHUD/ApprovalBar").GetComponent<ApprovalBarDisplay>().SetData();

        GetComponent<InitSpMoveHUD>().InitSpMoveButtons(player.KnownMoves);
        GetComponent<InitIngratiateHUD>().InitIngratiateButtons(PlayerRefGetter.Instance.PlayerInstance.KnownIngratiates);

        //yield return StartCoroutine(TextTyper.TypeText(dialogue, $"{opponent.EntityProfile.Entity.displayName} challenges you to a fight!"));
        await sequencer.StartBattle(opponentUnit);

        await StartPlayerSelection();
    }
    #endregion



    // ----------------------------------------------------------------------------------------------------------
    #region Battle Turn Pipeline

    public async Task StartPlayerSelection()
    {
        NewTurn.Invoke(BattleMenuState.SelectionMenu);
        //StartCoroutine(TextTyper.TypeText(dialogue, $"What will you do?"));
        battleState = BattleState.PlayerChoosingAction;
        await sequencer.PromptSelection();
    }

    public void ConstructAction(BattleActionType _type, SpecialMove _move = null, Ingratiate _ingratiate = null)
    {
        BattleAction action = new BattleAction(_type, playerUnit, opponentUnit, _move, _ingratiate);

        SubmitAction(action);
    }

    public async void SubmitAction(BattleAction _action)
    {
        if (battleState != BattleState.PlayerChoosingAction)
            return;

        pendingActions.Add(_action);

        battleState = BattleState.OpponentChoosingAction;

        ChooseOpponentAction();
        BuildActionQueue();

        await sequencer.RunTurn(actionQueue);
        EvaluateNextState();
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
                                   // a.user.Speed
        pendingActions.Sort((a, b) => b.user.Speed.CompareTo(a.user.Speed));

        pendingActions.Reverse();
        actionQueue = new Queue<BattleAction>(pendingActions);
    }

    private void EvaluateNextState()
    {
        if (opponentUnit.IsAlive && playerUnit.IsAlive)
        {
            turnNumber++;

            pendingActions.Clear();
            actionQueue.Clear();

            playerUnit.IsDefending = false;
            opponentUnit.IsDefending = false;

            StartPlayerSelection();
        }

        if (!playerUnit.IsAlive) EndBattle(WhyBattleEnded.Defeat);
        else if (!opponentUnit.IsAlive) EndBattle(WhyBattleEnded.Victory);
    }
    #endregion



    // ----------------------------------------------------------------------------------------------------------
    #region End Battle

    public async void EndBattle(WhyBattleEnded _why)
    {
        await sequencer.EndBattle(_why, opponentUnit);
        switch (_why)
        {
            case WhyBattleEnded.Defeat:
                // killll
                break;

            case WhyBattleEnded.Victory:
                Destroy(GetOpponentUnit().EntityProfile.GameObjectInstance);
                break;

            case WhyBattleEnded.Recruit:
                temporaryRecruit = GetOpponentUnit().EntityProfile as MinionProfileInstance;
                RecruitListManager.Instance.AddNewRecruit(temporaryRecruit);
                Destroy(GetOpponentUnit().EntityProfile.GameObjectInstance);
                break;
        }

        SyncBattleProfileChanges.SaveBackToPlayerBoss(playerUnit);
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
