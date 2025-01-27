using UnityEngine;

public class Candle : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject Flame;
    [SerializeField] GameObject Smoke;
    [SerializeField] GameObject Light;

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
            Flame.SetActive(true);
            Smoke.SetActive(true);
            Light.SetActive(true);

            withItem.GetComponent<Cigarette_Lighter>().Fire();
        }
    }
}
