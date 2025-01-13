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
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // 이동 입력 (느리게 걷기, 걷기, 달리기)
        if (x != 0 || z != 0)
        {
            if (Input.GetKey(KeyCode.LeftControl)) movementController.SlowWalk();
            else if (Input.GetKey(KeyCode.LeftShift)) movementController.Run();
            else movementController.Walk();
        }
        else movementController.Idle();

        // 점프 입력
        if (Input.GetKey(KeyCode.Space)) movementController.Jump();

        // 앉기 입력
        if (Input.GetKey(KeyCode.C) && !movementController.isCrouching) movementController.Crouch();            
        else if (!Input.GetKey(KeyCode.C) && movementController.isCrouching) movementController.UnCrouch();     


        // 최종 이동 방향 설정
        movementController.MoveTo(new Vector3(x, 0, z));
    }
}
