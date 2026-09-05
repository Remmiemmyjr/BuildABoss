using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MouseInputGrid : MonoBehaviour
{
    [SerializeField]
    private Camera gridCam;

    [SerializeField]
    private LayerMask placementLayer;

    private Vector3 lastPos;

    public event Action OnClicked, OnExit;

    private void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            OnClicked?.Invoke();
        }    
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            OnExit?.Invoke();
        }
    }

    public bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();


    public Vector3 GetSelectedGridPos()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = gridCam.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100, placementLayer))
        {
            lastPos = hit.point;
        }

        return lastPos;
    }
}
