using System;
using UnityEngine;

public class LairBuildTool : MonoBehaviour
{
    #region Mouse Input Variables
    [SerializeField] Camera gridCam;
    [SerializeField] GameObject mouseIndicator;
    [SerializeField] GameObject cellIndicator;
    [SerializeField] LayerMask gridLayer;
    [SerializeField] LayerMask placedObjectLayer;
    #endregion

    #region Placement Variables
    [SerializeField] private Grid grid;
    [SerializeField]  private LairAssetDatabase database;
    private int selectedObjIndex = -1;
    private Vector3 lastPos;
    private Action OnPlaceAsset, OnFinishPlacing;
    #endregion



    #region Unity Functions
    private void Update()
    {
        if (InputManager.Instance.mouseOverUI)
            return;

        if (InputManager.Instance.mouseHeld)
        {
            if (selectedObjIndex == -1)
                GetClickedAsset();
            else
                InputStart();
        }

        if (InputManager.Instance.mouseReleased)
            InputRelease();

        if (BuildToolActivator.inBuildMode)
        {
            Vector3 mousePos = GetSelectedGridPos();
            Vector3Int gridPos = grid.WorldToCell(mousePos);
            mouseIndicator.transform.position = mousePos;
            cellIndicator.transform.position = grid.CellToWorld(gridPos);
        }
    }
    #endregion



    #region Input Handling
    void InputStart()
    {
        OnPlaceAsset?.Invoke();
    }

    void InputRelease()
    {
        OnFinishPlacing?.Invoke();
    }
    #endregion



    #region Raycast Selections
    public Vector3 GetSelectedGridPos()
    {
        Vector3 mousePos = InputManager.Instance.ReturnMousePos();
        Ray ray = gridCam.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, gridLayer))
        {
            lastPos = hit.point;
        }
        return lastPos;
    }

    public void GetClickedAsset()
    {
        Vector3 mousePos = InputManager.Instance.ReturnMousePos();
        Ray ray = gridCam.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 600, placedObjectLayer))
        {
            Debug.Log("I should do something when you click me!");
        }
    }
    #endregion



    #region Placement Functions
    public void StartPlacement(int ID)
    {
        StopPlacement();
        selectedObjIndex = database.lairAssets.FindIndex(data => data.ID == ID);
        if (selectedObjIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }

        cellIndicator.SetActive(true);
        OnPlaceAsset += PlaceStructure;
        OnFinishPlacing += StopPlacement;
    }

    private void PlaceStructure()
    {
        if (BuildToolActivator.inBuildMode)
        {
            Vector3 mousePos = GetSelectedGridPos();
            Vector3Int gridPos = grid.WorldToCell(mousePos);
            GameObject assetToPlace = Instantiate(database.lairAssets[selectedObjIndex].lairAsset.Prefab);
            assetToPlace.layer = LayerMask.NameToLayer("PlacedLairObject");
            assetToPlace.AddComponent<BoxCollider>();
            assetToPlace.GetComponent<BoxCollider>().isTrigger = true;
            assetToPlace.transform.position = grid.CellToWorld(gridPos);
            StopPlacement();
        }
    }

    private void StopPlacement()
    {
        selectedObjIndex = -1;
        cellIndicator.SetActive(false);
        OnPlaceAsset -= PlaceStructure;
        OnFinishPlacing -= StopPlacement;
    }
    #endregion
}
