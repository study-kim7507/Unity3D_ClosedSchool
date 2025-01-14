using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovementController movementController;
    private PlayerLookController lookController;
    private PlayerStatus status;

    public PlayerFlashlight flashlight;

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
        PerformInteraction();
        ManageFlashlight();


        // TEST
        if (Input.GetKey(KeyCode.R))
        {
            Camera.main.GetComponent<TakePhoto>().Capture();
        }
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
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // �̵� �Է� (������ �ȱ�, �ȱ�, �޸���)
        if (x != 0 || z != 0)
        {
            if (Input.GetKey(KeyCode.LeftControl)) movementController.SlowWalk();
            else if (Input.GetKey(KeyCode.LeftShift)) movementController.Run();
            else movementController.Walk();
        }
        else movementController.Idle();

        // ���� �Է�
        if (Input.GetKey(KeyCode.Space)) movementController.Jump();

        // �ɱ� �Է�
        if (Input.GetKey(KeyCode.C) && !movementController.isCrouching) movementController.Crouch();            
        else if (!Input.GetKey(KeyCode.C) && movementController.isCrouching) movementController.UnCrouch();     


        // ���� �̵� ���� ����
        movementController.MoveTo(new Vector3(x, 0, z));
    }

    private void PerformInteraction()
    {
        // ī�޶󿡼� �ٶ󺸴� �������� Ray�� ��� �����Ǵ� ������Ʈ�� IIteractable �������̽��� �����Ǿ����� ���θ� Ȯ��
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1.0f))
        {
            if (hit.collider.gameObject.GetComponent<IInteractable>() != null)
            {
                // ���� ��ȣ�ۿ� ������ ������Ʈ�� Ray�� ������ ��, �÷��̾�� EŰ�� ���� �ش� ������Ʈ�� ��ȣ�ۿ��� �����ϵ���
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.gameObject.GetComponent<IInteractable>().Interact();
                }
            }
        }

        // For Debugging
        Debug.DrawRay(ray.origin, ray.direction * 1.0f, Color.red);
    }

    private void ManageFlashlight()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            flashlight.ToggleFlashlight();
        }
    }
}
