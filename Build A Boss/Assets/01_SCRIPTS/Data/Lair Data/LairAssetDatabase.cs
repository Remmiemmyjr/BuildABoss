using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Build A Boss/Lair/Database")]
public class LairAssetDatabase : ScriptableObject
{
    public List<LairAssetEntry> lairAssets;
}

[Serializable]
public class LairAssetEntry
{
    // can only be set in inspector, nothing outside of the class can change it
    [field: SerializeField]
    public LairAsset lairAsset;
    [field: SerializeField]
    public int ID { get; private set; }
}
