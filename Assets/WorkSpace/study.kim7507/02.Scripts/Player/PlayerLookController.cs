using UnityEngine;

public class PlayerLookController : MonoBehaviour
{
    [Header("Camera Rotation Speeds")]
    [SerializeField] private float rotCamXAxisSpeed = 3.0f; // 카메라 x축 회전속도
    [SerializeField] private float rotCamYAxisSpeed = 3.0f; // 카메라 y축 회전속도

    private float eulerAngleX = 0.0f;
    private float eulerAngleY = 0.0f;

    public void UpdateRotation(float mouseX, float mouseY)
    {
        eulerAngleY += mouseX * rotCamXAxisSpeed;   // 마우스 좌/우 이동으로 y축 회전
        eulerAngleX -= mouseY * rotCamYAxisSpeed;   // 마우스 상/하 이동으로 x축 회전

        // 회전 범위 설정
        eulerAngleX = Mathf.Clamp(eulerAngleX, -90.0f, 90.0f);

        transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
    }
}
