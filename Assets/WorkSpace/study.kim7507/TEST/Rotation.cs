using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float rotationSpeed = 100f; // ȸ�� �ӵ�

    void Update()
    {
        // �� �����Ӹ��� Y���� �������� ȸ��
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
