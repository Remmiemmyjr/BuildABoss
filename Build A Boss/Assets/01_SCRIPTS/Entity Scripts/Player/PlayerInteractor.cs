using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    private IInteractable interactableInRange = null;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(interactableInRange != null && context.performed && interactableInRange.CanInteract())
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
