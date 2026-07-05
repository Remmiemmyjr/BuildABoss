using UnityEngine;

public enum BattleState
{
    Start,
    PlayerChoosingAction,
    OpponentChoosingAction,
    ResolveTurn,
    EndTurn,
    AfterEffects,
    Over
}

public enum WhyBattleEnded
{
    Victory,
    Defeat,
    Recruit
}

public enum BattleMenuState
{
    Startup,
    SelectionMenu,
    CombatActions,
    SpMoveChoices,
    Ingratiates,
    Items,
    Busy,
    Over
}
