using UnityEngine;

public class Candle : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject flame;
    [SerializeField] GameObject smoke;
    [SerializeField] GameObject light;
    [SerializeField] GameObject interactionMessage;
    [HideInInspector] public bool isFired = false;

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
        if (withItem != null && withItem.GetComponent<Cigarette_Lighter>() != null)
        {
            flame.SetActive(true);
            smoke.SetActive(true);
            light.SetActive(true);

            isFired = true;
            withItem.GetComponent<Cigarette_Lighter>().Fire();
        }
        else
        {
            PlayerUI.instance.DisplayInteractionDescription("불을 붙이려면 라이터가 필요해보인다.");
        }
    }
}
