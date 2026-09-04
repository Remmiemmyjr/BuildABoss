using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Build A Boss/Lair/Asset")]
public class LairAssetDatabase : ScriptableObject
{
    public List<LairAsset> lairAssets;
}

[Serializable]
public class LairAsset
{
    // can only be set in inspector, nothing outside of the class can change it
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public int ID { get; private set; }
    [field: SerializeField]
    public Vector2Int Size { get; private set; } = Vector2Int.one;
    [field: SerializeField]
    public GameObject Prefab { get; private set; }
}
