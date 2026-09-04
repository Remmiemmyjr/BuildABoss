using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using System;

public class GridPlacementSystem : MonoBehaviour
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
    private MouseInputGrid mouseOnGrid;

    private void Start()
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
        mouseOnGrid.OnClicked += PlaceStructure;
        mouseOnGrid.OnExit += StopPlacement;
    }

    private void PlaceStructure()
    {
        if (mouseOnGrid.IsPointerOverUI())
            return;

        if (SwitchToBuildMode.inBuildMode)
        {
            Vector3 mousePos = mouseOnGrid.GetSelectedGridPos();
            Vector3Int gridPos = grid.WorldToCell(mousePos);
            GameObject assetToPlace = Instantiate(database.lairAssets[selectedObjIndex].Prefab);
            assetToPlace.transform.position = grid.CellToWorld(gridPos);
        }
    }

    private void StopPlacement()
    {
        selectedObjIndex = -1;
        //gridVisualization.SetActive(false);
        cellIndicator.SetActive(false);
        mouseOnGrid.OnClicked -= PlaceStructure;
        mouseOnGrid.OnExit -= StopPlacement;
    }

    private void Update()
    {
        if (SwitchToBuildMode.inBuildMode && selectedObjIndex >= 0)
        {
            Vector3 mousePos = mouseOnGrid.GetSelectedGridPos();
            Vector3Int gridPos = grid.WorldToCell(mousePos);
            mouseIndicator.transform.position = mousePos;
            cellIndicator.transform.position = grid.CellToWorld(gridPos);
        }
    }
}
