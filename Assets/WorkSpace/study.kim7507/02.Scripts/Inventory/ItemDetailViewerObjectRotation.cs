using UnityEngine;

public class ItemDetailViewerObjectRotation : MonoBehaviour
{
    public float rotationSpeed = 10.0f;         // ȸ�� �ӵ�
    public float autoRotateSpeed = 50.0f;       // �ڵ� ȸ�� �ӵ�
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
            Vector3 origin = gameObject.GetComponent<Collider>().bounds.center;

            Vector3 delta = Input.mousePosition - lastMousePosition;         // ���콺 �̵��� ���
            float rotationY = -delta.x * rotationSpeed * Time.deltaTime;     // Y�� ȸ���� ���
            float rotationX = delta.y * rotationSpeed * Time.deltaTime;      // X�� ȸ���� ���

            // ������Ʈ�� origin�� �������� ȸ�� ���� (�¿� �� ���� ȸ��)
            transform.RotateAround(origin, Vector3.up, rotationY); // Y�� ȸ��
            transform.RotateAround(origin, Vector3.right, rotationX); // X�� ȸ��

            lastMousePosition = Input.mousePosition;                         // ������ ���콺 ��ġ ������Ʈ
        }
        else
        {
            Vector3 origin = gameObject.GetComponent<Collider>().bounds.center;

            // ���콺�� ������ ���� �� �ڵ� ȸ��
            transform.RotateAround(origin, Vector3.up, Time.deltaTime * autoRotateSpeed);
        }
    }
}
