using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using System;

public class PlaceOnGrid : MonoBehaviour
{
    // NOTE: ALL PIVOT POINTS OF GRID OBJECTS NEED TO BE BOTTOM LEFT CORNER
    [SerializeField]
    GameObject mouseIndicator;
    [SerializeField]
    GameObject cellIndicator;
    [SerializeField]
    private Grid grid;
    [SerializeField]
    private LairAssetDatabase database;
    private int selectedObjIndex = -1;

    //[SerializeField]
    //private GameObject gridVisualization;

    [SerializeField]
    private MouseInputLairBuilding mouseInputLair;

    private void ToolActivated()
    {
        StopPlacement();
    }

    private void ToolDeactivated()
    {
        StopPlacement();
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        selectedObjIndex = database.lairAssets.FindIndex(data => data.ID == ID);
        if(selectedObjIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }

        //gridVisualization.SetActive(true);
        cellIndicator.SetActive(true);
        mouseInputLair.OnMouseHeld += PlaceStructure;
        mouseInputLair.OnMouseRelease += StopPlacement;
    }

    private void PlaceStructure()
    {
        if (BuildToolActivator.inBuildMode)
        {
            Vector3 mousePos = mouseInputLair.GetSelectedGridOrObject();
            Vector3Int gridPos = grid.WorldToCell(mousePos);
            GameObject assetToPlace = Instantiate(database.lairAssets[selectedObjIndex].Prefab);
            assetToPlace.transform.position = grid.CellToWorld(gridPos);
            StopPlacement();
        }
    }

    private void StopPlacement()
    {
        selectedObjIndex = -1;
        //gridVisualization.SetActive(false);
        cellIndicator.SetActive(false);
        mouseInputLair.OnMouseHeld -= PlaceStructure;
        mouseInputLair.OnMouseRelease -= StopPlacement;
    }

    private void Update()
    {
        if (BuildToolActivator.inBuildMode)
        {
            // not the right way to do this, need to bind an event to on clicked for this one
            if (selectedObjIndex < 0)
                mouseInputLair.GetSelectedGridOrObject();

            Vector3 mousePos = mouseInputLair.GetSelectedGridOrObject();
            Vector3Int gridPos = grid.WorldToCell(mousePos);
            mouseIndicator.transform.position = mousePos;
            cellIndicator.transform.position = grid.CellToWorld(gridPos);
        }
    }
}
