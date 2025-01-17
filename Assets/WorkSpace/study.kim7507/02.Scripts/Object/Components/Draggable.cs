using UnityEngine;

public class Draggable : MonoBehaviour
{
    [HideInInspector] public bool isDragging;
    private Rigidbody rigidbody;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isDragging) Dragging();
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
        Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * 0.75f;

        // 오브젝트의 위치 이동
        gameObject.transform.position = targetPosition;
    }
}
