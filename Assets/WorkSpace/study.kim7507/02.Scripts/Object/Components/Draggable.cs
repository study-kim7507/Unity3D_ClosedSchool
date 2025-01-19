using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Collider))] 
[RequireComponent(typeof(Rigidbody))]
public class Draggable : MonoBehaviour
{
    [HideInInspector] public bool isDragging;
    private Rigidbody rigidbody;
    private Collider collider;

    private Vector3 pivotOffset;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (isDragging) Dragging();
    }

    public void BeginDrag()
    {
        isDragging = true;

        pivotOffset = transform.position - collider.bounds.center;          // 오브젝트의 중심과 피봇의 차이를 계산

        collider.excludeLayers |= (1 << LayerMask.NameToLayer("Player"));
        
        rigidbody.isKinematic = true;                                       // 리지드 바디 비활성화
    }

    public void EndDrag()
    {
        isDragging = false;

        collider.excludeLayers &= (1 >> LayerMask.NameToLayer("Player"));

        rigidbody.isKinematic = false;                                      // 리지드 바디 활성화
    }

    

    public void Dragging()
    { 
        Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * 0.75f;
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetPosition + pivotOffset, Time.deltaTime * 50.0f);
    }
}
