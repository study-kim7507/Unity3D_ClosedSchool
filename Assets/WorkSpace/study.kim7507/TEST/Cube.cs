using UnityEngine;

public class Cube : MonoBehaviour, IInteractable
{
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
        if (withItem != null && withItem.GetComponent<Cigarette_Lighter>() != null)
        {
            Debug.Log("�����ͷ� ���� �����.");
        }
    }
}
