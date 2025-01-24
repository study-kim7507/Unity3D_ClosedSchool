using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private void Start()
    {
        
    }
    public void BeginFocus(GameObject withItem = null)
    {
        throw new System.NotImplementedException();
    }

    public void BeginInteract(GameObject withItem = null)
    {
        throw new System.NotImplementedException();
    }

    public void EndFocus(GameObject withItem = null)
    {
        throw new System.NotImplementedException();
    }

    public void EndInteract(GameObject withItem = null)
    {
        throw new System.NotImplementedException();
    }

    public void Interact(GameObject withItem = null)
    {
        // TODO: 열쇠 로직
        // if (withItem != null && withItem.GetComponent<Key>() != null)

        // TODO: 문 열리는 사운드
        GetComponent<Animation>().Play();
    }
}
