using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb; // 농구공의 Rigidbody
    private static BallController selectedBall; // 현재 선택된 공을 추적하는 static 변수
    private bool isCharging = false; // 충전 상태 확인
    private float chargePower = 0f; // 충전된 힘
    private float maxPower = 15f; // 최대 힘
    private float chargeRate = 10f; // 힘 증가 속도
    private bool isReleased = false; // 공이 던져졌는지 확인

    public Camera playerCamera; // 플레이어가 사용하는 카메라

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 카메라가 설정되지 않은 경우 기본적으로 메인 카메라를 사용
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    private void Update()
    {
        // 오른쪽 마우스 버튼으로 공 선택
        if (Input.GetMouseButtonDown(1))
        {
            SelectBall();
        }

        // 선택된 공만 충전 및 던지기 가능
        if (selectedBall == this)
        {
            if (Input.GetMouseButton(1) && isCharging)
            {
                ChargePower();
            }

            if (Input.GetMouseButtonUp(1) && isCharging && !isReleased)
            {
                ReleaseBall();
            }
        }
    }

    private void SelectBall()
    {
        // 마우스 클릭한 위치에서 Raycast를 통해 공을 선택
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject == gameObject) // 클릭한 오브젝트가 현재 공인지 확인
            {
                selectedBall = this; // 이 공을 선택
                isCharging = true;   // 공을 집었으므로 충전 가능
                isReleased = false; // 공이 아직 던져지지 않은 상태
                Debug.Log($"{gameObject.name} 공이 선택되었습니다.");
            }
        }
    }

    private void ChargePower()
    {
        chargePower += chargeRate * Time.deltaTime;
        chargePower = Mathf.Clamp(chargePower, 0f, maxPower);
        Debug.Log($"힘 충전 중: {chargePower}");
    }

    private void ReleaseBall()
    {
        if (chargePower <= 0f)
        {
            Debug.LogWarning("충전된 힘이 부족하여 공이 던져지지 않습니다.");
            return;
        }

        isCharging = false;
        isReleased = true;

        // 카메라가 바라보는 방향을 계산
        Vector3 cameraForward = playerCamera.transform.forward; // 카메라의 전방 벡터
        Vector3 launchDirection = (cameraForward + Vector3.up * 0.1f).normalized; // 약간 위로 던지기

        // 힘 적용
        rb.AddForce(launchDirection * chargePower, ForceMode.Impulse);

        Debug.Log($"{gameObject.name} 공을 던짐: 힘 = {chargePower}, 방향 = {launchDirection}");
        chargePower = 0f;

        // 선택 상태 초기화
        selectedBall = null;
    }
}
