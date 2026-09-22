using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    private IInteractable interactableInRange = null;

    private void Start()
    {
        InputManager.Instance.OnPlayerInteractInput += OnInteract;
    }

    private void OnDisable()
    {
        InputManager.Instance.OnPlayerInteractInput -= OnInteract;
    }

    public void OnInteract(bool performed)
    {
        if(interactableInRange != null && performed && interactableInRange.CanInteract())
        {
            interactableInRange?.Interact();
        }    
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            interactableInRange = interactable;
            interactableInRange?.OnFocusGained();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange?.OnFocusLost();
            interactableInRange = null;
        }
    }
}
