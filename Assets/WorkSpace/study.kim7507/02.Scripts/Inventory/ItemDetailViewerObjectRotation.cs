using UnityEngine;

public class ItemDetailViewerObjectRotation : MonoBehaviour
{
    public float rotationSpeed = 10.0f;         // ȸ�� �ӵ�
    public float autoRotateSpeed = 100.0f;      // �ڵ� ȸ�� �ӵ�
    private bool isDragging = false;            // �巡�� ����
    private Vector3 lastMousePosition;

    void Update()
    {
        // ���콺 ��ư�� ���� ���
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;                        // ���� ���콺 ��ġ ����
        }

        // ���콺 ��ư�� ������ ���
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // �巡�� ���� ��
        if (isDragging)
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;        // ���콺 �̵��� ���
            float rotationY = -delta.x * rotationSpeed * Time.deltaTime;     // Y�� ȸ���� ���

            // ������Ʈ ȸ�� ���� (�¿� ȸ����)
            transform.Rotate(Vector3.up, rotationY, Space.World);

            lastMousePosition = Input.mousePosition;                        // ������ ���콺 ��ġ ������Ʈ
        }
        else
        {
            // ���콺�� ������ ���� �� �ڵ� ȸ��
            transform.Rotate(Vector3.up, autoRotateSpeed * Time.deltaTime, Space.World);
        }
    }
}
