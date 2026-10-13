using System.Collections.Generic;
using UnityEngine;

public class GridCellData
{
    Dictionary<Vector3Int, ObjectPlacementData> placedObjects = new();
}


public class ObjectPlacementData
{
    public List<Vector3Int> occupiedPositions;
    public int ID { get; private set; }
    public int placedObjectIndex { get; private set; }
}