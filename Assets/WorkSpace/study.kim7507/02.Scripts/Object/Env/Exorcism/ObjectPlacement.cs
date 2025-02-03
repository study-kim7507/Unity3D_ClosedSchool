using UnityEngine;
using static UnityEditor.Progress;

public enum Type
{
    Candle,
    Photo
};

public class ObjectPlacement : MonoBehaviour
{
    public Type type;
    [HideInInspector] public bool isComplete = false;
    [HideInInspector] public GameObject currObject = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Draggable>() != null && other.GetComponent<Draggable>().isDragging) return;

        if (other.TryGetComponent<Candle>(out var candle) || other.TryGetComponent<Photo>(out var photo))
        {
            other.gameObject.transform.position = transform.position;
            if (candle != null) other.transform.rotation = Quaternion.identity;
          
            other.GetComponent<Draggable>().enabled = false;
            other.GetComponent<Pickable>().enabled = false;
          
            GetComponent<Collider>().enabled = false;

            isComplete = true;
            currObject = other.gameObject;
        }
    }
}
