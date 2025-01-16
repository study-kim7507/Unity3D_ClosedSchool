using UnityEngine;

public class Draggable : MonoBehaviour
{
    [HideInInspector] public bool isDragging;
    private Rigidbody rigidbody;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void BeginDrag()
    {
        isDragging = true;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        rigidbody.useGravity = false;
    }

    public void EndDrag()
    {
        isDragging = false;
        rigidbody.constraints = RigidbodyConstraints.None;
        rigidbody.useGravity = true;
    }


    public void Dragging()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        Vector3 targetPosition = ray.GetPoint(1.0f);        // 레이의 거리

        gameObject.transform.position = targetPosition;
    }
}
