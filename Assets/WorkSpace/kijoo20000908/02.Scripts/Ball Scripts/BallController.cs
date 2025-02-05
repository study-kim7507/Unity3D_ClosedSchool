using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    public Camera playerCamera;
    public float throwForce = 18f; // ?�질 ??
    public float spinForce = 5f; // ?�전 ??
    private bool isHolding = false; // 공을 ?�고 ?�는지 ?�인
    private static BallController selectedBall = null; // ?�재 ?�택??�?(?�나�??�성??

    private bool isAlreadySelected = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    private void Update()
    {
        // 마우?�로 ?�정 �??�택
        if (Input.GetMouseButtonDown(0)) // ?�쪽 ?�릭?�로 �??�택
        {
            SelectBall();
        }

        if (selectedBall == this) // ?�재 ?�택??공만 ?�질 ???�음
        {
            if (!isAlreadySelected)
            {
                PlayerUI.instance.DisplayInteractionDescription("EŰ�� ���� ��븦 ���� ���� ���� �־��");
                isAlreadySelected = true;
            }
            if (Input.GetKeyDown(KeyCode.E)) // E ?�로 ?��?�?
            {
                ThrowBall();
                selectedBall = null; // ?�택 ?�제
            }
        }
    }

    private void SelectBall()
    {
        // 마우?�로 ?�릭??공만 ?�택 (Raycast ?�용)
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject == gameObject) // ?�릭??공만 ?�택
            {
                selectedBall = this;
            }
        }
    }

    private void ThrowBall()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
        Destroy(GetComponent<Draggable>());

        Vector3 throwDirection = (playerCamera.transform.forward + Vector3.up * 0.1f).normalized;

        transform.position += throwDirection * 0.3f;
        rb.linearVelocity = throwDirection * throwForce;

        rb.angularVelocity = Random.insideUnitSphere * spinForce;

        StartCoroutine(ReattachDraggableAfterDelay(0.5f));
    }

    private IEnumerator ReattachDraggableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Draggable 컴포?�트�??�시 추�?
        gameObject.AddComponent<Draggable>();
    }
}
