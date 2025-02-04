using UnityEngine;

public class BookInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject interactionMessage;
    public void BeginFocus(GameObject withItem = null)
    {
        if (interactionMessage != null)
            interactionMessage.SetActive(true);
    }

    public void EndFocus(GameObject withItem = null)
    {
        if (interactionMessage != null)
            interactionMessage.SetActive(false);
    }

    public void Interact(GameObject withItem = null)
    {
        
    }
}
