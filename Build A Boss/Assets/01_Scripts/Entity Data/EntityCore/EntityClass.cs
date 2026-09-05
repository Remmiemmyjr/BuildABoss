using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityClass : ScriptableObject
{
    #region Variables
    [Header("Basic Info")]
    public string displayName;
    public int level;
    public Sprite sprite;

    [Header("Stats")]
    public StatBlockDefinition baseStats;

    [Header("Moves")] // could also be stored on the persistent profile
    public int maxKnownMoves;
    public List<SpecialMove> knownMoves = new();
    public List<SpecialMove> learnableMoves = new();
    #endregion
}
