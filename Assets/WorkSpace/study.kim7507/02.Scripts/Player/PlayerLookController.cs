using UnityEngine;

public class PlayerLookController : MonoBehaviour
{
    [Header("Camera Rotation Speeds")]
    [SerializeField] private float rotCamXAxisSpeed = 3.0f; // ī�޶� x�� ȸ���ӵ�
    [SerializeField] private float rotCamYAxisSpeed = 3.0f; // ī�޶� y�� ȸ���ӵ�

    private float eulerAngleX = 0.0f;
    private float eulerAngleY = 0.0f;

    public void UpdateRotation(float mouseX, float mouseY)
    {
        eulerAngleY += mouseX * rotCamXAxisSpeed;   // ���콺 ��/�� �̵����� y�� ȸ��
        eulerAngleX -= mouseY * rotCamYAxisSpeed;   // ���콺 ��/�� �̵����� x�� ȸ��

        // ȸ�� ���� ����
        eulerAngleX = Mathf.Clamp(eulerAngleX, -90.0f, 90.0f);

        transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
    }
}
