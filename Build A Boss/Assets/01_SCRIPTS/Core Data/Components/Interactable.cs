using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private bool isEnabled = true;
    [SerializeField] private UnityEvent onInteract;
    [SerializeField] private UnityEvent interactFinished;

    public string DisplayText => "Talk";

    public bool CanInteract() => isEnabled;

    public void Interact()
    {
        onInteract?.Invoke();
        Debug.Log("You interacted with me! :D");
        isEnabled = true;
    }

    public void FinishInteraction()
    {
        interactFinished?.Invoke();
    }

    public void OnFocusGained()
    {
        //throw new System.NotImplementedException();

        Debug.Log("Press [E] To Interact");
    }

    public void OnFocusLost()
    {
        //throw new System.NotImplementedException();

        Debug.Log("Walked away :(");
    }
}
