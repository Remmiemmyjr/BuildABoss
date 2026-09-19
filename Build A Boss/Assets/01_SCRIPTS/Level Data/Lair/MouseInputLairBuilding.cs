using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MouseInputLairBuilding : MonoBehaviour
{
    [SerializeField]
    private Camera gridCam;

    [SerializeField]
    private LayerMask gridLayer;
    [SerializeField]
    private LayerMask placedObjectLayer;

    private Vector3 lastPos;

    public event Action OnMouseHeld, OnMouseRelease;
    public InputActionReference mouseInputAction;
    public bool isMouseHeld;

    public void ToolActivated()
    {
        mouseInputAction.action.started += BeginDrag;
        mouseInputAction.action.canceled += EndDrag;
    }

    public void ToolDeactivated()
    {
        mouseInputAction.action.started -= BeginDrag;
        mouseInputAction.action.canceled -= EndDrag;
    }

    public void BeginDrag(InputAction.CallbackContext context)
    {
        OnMouseHeld?.Invoke();
        isMouseHeld = true;
    }

    public void EndDrag(InputAction.CallbackContext context)
    {
        OnMouseRelease?.Invoke();
        isMouseHeld = false;
    }    


    public Vector3 GetSelectedGridOrObject()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = gridCam.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100, gridLayer))
        {
            lastPos = hit.point;
        }
        if(Physics.Raycast(ray, out hit, 600, placedObjectLayer))
        {
            Debug.Log("I should do something when you click me!");
        }

        return lastPos;
    }

}
