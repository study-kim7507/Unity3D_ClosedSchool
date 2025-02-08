using UnityEngine;
public enum Type
{
    Candle,
    Photo
};

public class ObjectPlacement : MonoBehaviour, IInteractable
{
    public Type type;
    [HideInInspector] public bool isComplete = false;
    [HideInInspector] public GameObject currObject = null;

    [SerializeField] GameObject interactionMessage;

    public void BeginFocus(GameObject withItem = null)
    {
        if (interactionMessage != null && !isComplete)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Draggable>() != null && other.GetComponent<Draggable>().isDragging) return;

        if (other.TryGetComponent<Candle>(out var candle) && type == Type.Candle)
        {
            other.gameObject.transform.position = transform.position;
            other.transform.rotation = Quaternion.identity;
          
            other.GetComponent<Draggable>().enabled = false;
            other.GetComponent<Pickable>().enabled = false;
          
            GetComponent<Collider>().enabled = false;

            isComplete = true;
            EndFocus();
            currObject = other.gameObject;
        }
        else if (other.TryGetComponent<Photo>(out var photo) && type == Type.Photo)
        {
            if (photo.ghostType == GhostType.None)
            {
                PlayerUI.instance.DisplayInteractionDescription("퇴마에 사용할 수 없는 사진인 것 같다.\n귀신이 찍힌 사진이 필요하다.");
                return;
            }
            other.gameObject.transform.position = transform.position;

            other.GetComponent<Draggable>().enabled = false;
            other.GetComponent<Pickable>().enabled = false;

            GetComponent<Collider>().enabled = false;

            isComplete = true;
            EndFocus();
            currObject = other.gameObject;
        }
        else
        {
            PlayerUI.instance.DisplayInteractionDescription("퇴마 방법이 잘못된 것 같다.\n올바른 위치에 올바른 퇴마용품이 놓여야 한다.");
        }
    }
}
