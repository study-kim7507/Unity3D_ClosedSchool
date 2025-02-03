using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    public Camera playerCamera;
    public float throwForce = 18f; // 던질 힘
    public float spinForce = 5f; // 회전 힘
    private bool isHolding = false; // 우클릭을 누르고 있는지 확인
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
            if (Input.GetMouseButtonDown(1)) // 우클릭을 누르면 준비 상태
            {
                isHolding = true;
            }

            if (Input.GetMouseButtonUp(1) && isHolding) // 우클릭을 떼면 던지기
            {
                ThrowBall();
                isHolding = false;
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
        // 던지기 전에 물리 활성화
        rb.isKinematic = false;
        rb.useGravity = true;

        // 카메라 방향으로 공 던지기
        Vector3 throwDirection = (playerCamera.transform.forward + Vector3.up * 0.1f).normalized;

        // 공을 손에서 약간 앞으로 밀어줌 (충돌 방지)
        transform.position += throwDirection * 0.3f;

        // 즉시 속도를 설정하여 공이 던져지는 느낌을 강화
        rb.linearVelocity = throwDirection * throwForce;

        // 회전 효과 추가 (자연스러운 움직임을 위해)
        rb.angularVelocity = Random.insideUnitSphere * spinForce;

        Debug.Log($"{gameObject.name} 공을 던졌습니다! 힘 = {throwForce}, 방향 = {throwDirection}");
    }
}
