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
        // 이동 방향 = 캐릭터의 회전 값 * 방향 값
        direction = transform.rotation * new Vector3(direction.x, 0, direction.z);

        // 이동 힘 = 이동 방향 * 속도
        moveForce = new Vector3(direction.x * moveSpeed, moveForce.y, direction.z * moveSpeed);
    }

    public void Jump(float jumpForce)
    {
        // 지면에 닿아있을 때 (점프 중인 상태가 아닐 때)만 점프가 가능하도록
        if (characterController.isGrounded)
        {
            moveForce.y = jumpForce;
        }
    }
}
