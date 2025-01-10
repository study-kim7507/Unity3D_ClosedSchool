using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovementController movementController;
    private PlayerLookController lookController;
    private PlayerStatus status;

    private void Start()
    {
        // ���콺 Ŀ���� ������ �ʰ� ����
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

    // ���콺 �Է��� ���� ĳ���� ȸ���� ���
    private void UpdateRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        lookController.UpdateRotation(mouseX, mouseY);
    }

    // Ű���� �Է��� ���� ĳ���� �̵��� ���
    private void UpdateMove()
    {
        // TODO: �ɱ� ����

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

    // Ű���� �Է��� ���� ĳ���� ������ ���
    private void UpdateJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movementController.Jump(status.jumpForce);
        }
    }
}
