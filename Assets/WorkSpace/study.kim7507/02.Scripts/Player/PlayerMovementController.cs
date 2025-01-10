using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{ 
    private Vector3 moveForce;

    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!characterController.isGrounded) moveForce.y -= 9.81f * Time.deltaTime;
        characterController.Move(moveForce * Time.deltaTime);
    }

    public void MoveTo(Vector3 direction, float moveSpeed)
    {
        // �̵� ���� = ĳ������ ȸ�� �� * ���� ��
        direction = transform.rotation * new Vector3(direction.x, 0, direction.z);

        // �̵� �� = �̵� ���� * �ӵ�
        moveForce = new Vector3(direction.x * moveSpeed, moveForce.y, direction.z * moveSpeed);
    }

    public void Jump(float jumpForce)
    {
        // ���鿡 ������� �� (���� ���� ���°� �ƴ� ��)�� ������ �����ϵ���
        if (characterController.isGrounded)
        {
            moveForce.y = jumpForce;
        }
    }
}
