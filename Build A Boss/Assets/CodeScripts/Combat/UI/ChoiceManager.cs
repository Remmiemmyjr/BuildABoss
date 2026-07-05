using UnityEngine;
using System.Collections.Generic;

public class ChoiceManager : MonoBehaviour
{
    #region Variables
    [HideInInspector] public BattleMenuState menuState;
    
    [SerializeField] GameObject dialogueBox;
    [SerializeField] GameObject InitialMenu;
    [SerializeField] GameObject CombatChoices;
    [SerializeField] GameObject SpAtkChoices;
    [SerializeField] GameObject IngratiateChoices;
    [SerializeField] GameObject SpAtkInfo;
    [SerializeField] GameObject Recruit;

    List<SpecialMove> specialMovesToAssign;
    List<Ingratiate> ingratiatesToAssign;
    BattleEntity player;
    #endregion



    #region Setup Functions
    private void Start()
    {
        BattleEvents.BattleStarted += Setup;
    }
    
    private void Setup()
    {
        UpdateMenu(BattleMenuState.Startup);

        player = BattleManager.Instance.GetPlayerUnit();
        specialMovesToAssign = player.KnownMoves;
        ingratiatesToAssign = PlayerDataManager.Instance.Boss.KnownIngratiates;

        Recruit.SetActive(false);

        BattleManager.Instance.NewTurn += UpdateMenu;   
    }
    #endregion



    #region Initial Menu
    public void OnSelectActions()
    {
        UpdateMenu(BattleMenuState.CombatActions);
    }
    public void OnSelectItems()
    {
        UpdateMenu(BattleMenuState.Items);
    }
    public void OnSelectForfeit()
    {
        // Try to quit battle if allowed
    }
    #endregion



    #region Combat Button Choices
    public void OnSelectAttack()
    {
        UpdateMenu(BattleMenuState.Busy);
        BattleManager.Instance.ConstructAction(BattleActionType.Attack);
    }
    public void OnSelectDefend()
    {
        UpdateMenu(BattleMenuState.Busy);
        BattleManager.Instance.ConstructAction(BattleActionType.Defend);
    }
    public void OnSelectSpMove()
    {
        UpdateMenu(BattleMenuState.SpMoveChoices);
    }
    public void OnSelectIngratiate()
    {
        if (BattleManager.Instance.Context.Approval >= 100)
            return;
        UpdateMenu(BattleMenuState.Ingratiates);
    }
    public void OnRecruit()
    {
        UpdateMenu(BattleMenuState.Over);
        StartCoroutine(BattleManager.Instance.EndBattle(WhyBattleEnded.Recruit));
    }
    #endregion



    #region Special Attack Functions
    public void OnSelectSpMove_1()
    {
        if (player.currMana < specialMovesToAssign[0].manaCost)
            return;
        UpdateMenu(BattleMenuState.Busy);
        BattleManager.Instance.ConstructAction(BattleActionType.SpecialMove, specialMovesToAssign[0]);
    }
    public void OnSelectSpMove_2()
    {
        if (player.currMana < specialMovesToAssign[1].manaCost)
            return;
        UpdateMenu(BattleMenuState.Busy);
        BattleManager.Instance.ConstructAction(BattleActionType.SpecialMove, specialMovesToAssign[1]);
    }
    public void OnSelectSpMove_3()
    {
        if (player.currMana < specialMovesToAssign[2].manaCost)
            return;
        UpdateMenu(BattleMenuState.Busy);
        BattleManager.Instance.ConstructAction(BattleActionType.SpecialMove, specialMovesToAssign[2]);
    }
    public void OnSelectSpMove_4()
    {
        if (player.currMana < specialMovesToAssign[3].manaCost)
            return;
        UpdateMenu(BattleMenuState.Busy);
        BattleManager.Instance.ConstructAction(BattleActionType.SpecialMove, specialMovesToAssign[3]);
    }
    #endregion



    #region Ingratiate Functions
    public void OnSelectIngratiateMove_1()
    {
        UpdateMenu(BattleMenuState.Busy);
        BattleManager.Instance.ConstructAction(BattleActionType.Ingratiate, null, ingratiatesToAssign[0]);
    }
    public void OnSelectIngratiateMove_2()
    {
        UpdateMenu(BattleMenuState.Busy);
        BattleManager.Instance.ConstructAction(BattleActionType.Ingratiate, null, ingratiatesToAssign[1]);
    }
    public void OnSelectIngratiateMove_3()
    {
        UpdateMenu(BattleMenuState.Busy);
        BattleManager.Instance.ConstructAction(BattleActionType.Ingratiate, null, ingratiatesToAssign[2]);
    }
    public void OnSelectIngratiateMove_4()
    {
        UpdateMenu(BattleMenuState.Busy);
        BattleManager.Instance.ConstructAction(BattleActionType.Ingratiate, null, ingratiatesToAssign[3]);
    }
    #endregion



    #region Update Functions
    public void UpdateMenu(BattleMenuState state)
    {
        menuState = state;

            switch (state)
            {
                case BattleMenuState.Startup:
                    dialogueBox.gameObject.SetActive(true);
                    InitialMenu.SetActive(false);
                    CombatChoices.SetActive(false);
                    SpAtkChoices.SetActive(false);
                    IngratiateChoices.SetActive(false);
                    SpAtkInfo.SetActive(false);
                    break;

                case BattleMenuState.SelectionMenu:
                    dialogueBox.gameObject.SetActive(true);
                    InitialMenu.SetActive(true);
                    CombatChoices.SetActive(false);
                    SpAtkChoices.SetActive(false);
                    IngratiateChoices.SetActive(false);
                    SpAtkInfo.SetActive(false);

                    if (BattleManager.Instance.approval >= 100)
                    {
                        Recruit.SetActive(true);
                    }
                    else
                    {
                        Recruit.SetActive(false);
                    }
                    break;

                case BattleMenuState.CombatActions:
                    dialogueBox.gameObject.SetActive(true);
                    InitialMenu.SetActive(false);
                    CombatChoices.SetActive(true);
                    SpAtkChoices.SetActive(false);
                    IngratiateChoices.SetActive(false);
                    SpAtkInfo.SetActive(false);
                    Recruit.SetActive(false);
                    break;

                case BattleMenuState.SpMoveChoices:
                    dialogueBox.gameObject.SetActive(false);
                    InitialMenu.SetActive(false);
                    CombatChoices.SetActive(false);
                    SpAtkChoices.SetActive(true);
                    IngratiateChoices.SetActive(false);
                    SpAtkInfo.SetActive(true);
                    Recruit.SetActive(false);
                    break;

                case BattleMenuState.Ingratiates:
                    dialogueBox.gameObject.SetActive(false);
                    InitialMenu.SetActive(false);
                    CombatChoices.SetActive(false);
                    SpAtkChoices.SetActive(false);
                    IngratiateChoices.SetActive(true);
                    SpAtkInfo.SetActive(false);
                    Recruit.SetActive(false);
                    break;
                case BattleMenuState.Items:
                    dialogueBox.gameObject.SetActive(false);
                    InitialMenu.SetActive(false);
                    CombatChoices.SetActive(false);
                    SpAtkChoices.SetActive(false);
                    IngratiateChoices.SetActive(false);
                    SpAtkInfo.SetActive(false);
                    Recruit.SetActive(false);
                    break;

                case BattleMenuState.Busy:
                    dialogueBox.gameObject.SetActive(true);
                    InitialMenu.SetActive(false);
                    CombatChoices.SetActive(false);
                    SpAtkChoices.SetActive(false);
                    IngratiateChoices.SetActive(false);
                    SpAtkInfo.SetActive(false);
                    Recruit.SetActive(false);
                    break;

                case BattleMenuState.Over:
                    dialogueBox.gameObject.SetActive(true);
                    InitialMenu.SetActive(false);
                    CombatChoices.SetActive(false);
                    SpAtkChoices.SetActive(false);
                    IngratiateChoices.SetActive(false);
                    SpAtkInfo.SetActive(false);
                    Recruit.SetActive(false);
                    break;
        }
    }
    #endregion
}
