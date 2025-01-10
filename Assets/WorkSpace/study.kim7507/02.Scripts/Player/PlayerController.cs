using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovementController movementController;
    private PlayerLookController lookController;
    private PlayerStatus status;

    private void Start()
    {
        // 마우스 커서를 보이지 않게 설정
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        movementController = GetComponent<PlayerMovementController>(); 
        lookController = GetComponent<PlayerLookController>();
        status = GetComponent<PlayerStatus>();
    }

    private void Update()
    {
        UpdateRotation();
        UpdateMove();
        UpdateJump();
    }

    // 마우스 입력을 통한 캐릭터 회전을 담당
    private void UpdateRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        lookController.UpdateRotation(mouseX, mouseY);
    }

    // 키보드 입력을 통한 캐릭터 이동을 담당
    private void UpdateMove()
    {
        // TODO: 앉기 구현

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (x != 0 || z != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                movementController.MoveTo(new Vector3(x, 0, z), status.runSpeed);
            }
            else
            {
                movementController.MoveTo(new Vector3(x, 0, z), status.walkSpeed);
            }

        }
        else
        {
            movementController.MoveTo(new Vector3(), 0.0f);
        }
    }

    // 키보드 입력을 통한 캐릭터 점프를 담당
    private void UpdateJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movementController.Jump(status.jumpForce);
        }
    }
}
