using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    public Camera playerCamera;
    public float throwForce = 18f; // 던질 힘
    public float spinForce = 5f; // 회전 힘
    private bool isHolding = false; // 공을 들고 있는지 확인
    private static BallController selectedBall = null; // 현재 선택된 공 (하나만 활성화)

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
        // 마우스로 특정 공 선택
        if (Input.GetMouseButtonDown(0)) // 왼쪽 클릭으로 공 선택
        {
            SelectBall();
        }

        if (selectedBall == this) // 현재 선택된 공만 던질 수 있음
        {
            if (Input.GetKeyDown(KeyCode.E)) // E 키로 던지기
            {
                ThrowBall();
                selectedBall = null; // 선택 해제
            }
        }
    }

    private void SelectBall()
    {
        // 마우스로 클릭한 공만 선택 (Raycast 사용)
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject == gameObject) // 클릭한 공만 선택
            {
                selectedBall = this;
                Debug.Log($"{gameObject.name} 선택됨");
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

        // Draggable 컴포넌트를 다시 추가
        gameObject.AddComponent<Draggable>();
    }
}
