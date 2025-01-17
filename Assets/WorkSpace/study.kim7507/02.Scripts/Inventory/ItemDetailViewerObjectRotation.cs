using UnityEngine;

public class ItemDetailViewerObjectRotation : MonoBehaviour
{
    public float rotationSpeed = 10.0f;         // 회전 속도
    public float autoRotateSpeed = 100.0f;      // 자동 회전 속도
    private bool isDragging = false;            // 드래그 여부
    private Vector3 lastMousePosition;

    void Update()
    {
        // 마우스 버튼이 눌린 경우
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;                        // 현재 마우스 위치 저장
        }

        // 마우스 버튼이 떼어진 경우
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // 드래그 중일 때
        if (isDragging)
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;        // 마우스 이동량 계산
            float rotationY = -delta.x * rotationSpeed * Time.deltaTime;     // Y축 회전량 계산

            // 오브젝트 회전 적용 (좌우 회전만)
            transform.Rotate(Vector3.up, rotationY, Space.World);

            lastMousePosition = Input.mousePosition;                        // 마지막 마우스 위치 업데이트
        }
        else
        {
            // 마우스가 눌리지 않을 때 자동 회전
            transform.Rotate(Vector3.up, autoRotateSpeed * Time.deltaTime, Space.World);
        }
    }
}
