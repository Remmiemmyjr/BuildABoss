using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    // The bool indicates if the input ctx was canceled or not
    public event Action<Vector2> OnPlayerMovementInput;
    public event Action<bool> OnPlayerSprintInput, OnPlayerInteractInput, OnMouseClickUI;

    public bool mouseHeld => Mouse.current.leftButton.wasPressedThisFrame;
    public bool mouseReleased => Mouse.current.leftButton.wasReleasedThisFrame;
    public bool mouseOverUI => EventSystem.current.IsPointerOverGameObject();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public Vector2 ReturnMousePos()
    {
        return Mouse.current.position.ReadValue();
    }

    public void ChangeInputToUI()
    {
        GetComponent<PlayerInput>().SwitchCurrentActionMap("LairBuilding");
    }

    public void ChangeInputToPlayer()
    {
        GetComponent<PlayerInput>().SwitchCurrentActionMap("Gameplay");
    }

    public void PlayerMovementInput(InputAction.CallbackContext ctx)
    {
        OnPlayerMovementInput.Invoke(ctx.ReadValue<Vector2>());
    }

    public void PlayerSprintInput(InputAction.CallbackContext ctx)
    {
        OnPlayerSprintInput.Invoke(ctx.performed);
    }

    public void PlayerInteractInput(InputAction.CallbackContext ctx)
    {
        OnPlayerInteractInput.Invoke(ctx.performed);
    }

    public void MouseClickUI(InputAction.CallbackContext ctx)
    {
        //if (!EventSystem.current.IsPointerOverGameObject())
        //    OnMouseClickUI.Invoke(ctx.performed);
    }

}
