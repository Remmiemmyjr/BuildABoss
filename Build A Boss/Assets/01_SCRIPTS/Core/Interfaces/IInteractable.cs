using UnityEngine;

public interface IInteractable
{
    string DisplayText { get; }
    bool CanInteract();
    void Interact();
    void FinishInteraction();
    void OnFocusGained();
    void OnFocusLost();
}
