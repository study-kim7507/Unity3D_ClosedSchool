using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovementController movementController;
    private PlayerLookController lookController;
    private PlayerStatus status;

    public InventorySystem inventory;
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
        ManageInventory();

        // TEST
        if (Input.GetKeyDown(KeyCode.R))
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
            if (hit.collider.gameObject.GetComponent<IPickable>() != null)
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    inventory.AddToInventory(hit.collider.gameObject);
                }
            }
        }
    }

    private void ManageFlashlight()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            flashlight.ToggleFlashlight();
        }
    }

    private void ManageInventory()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = CursorLockMode.None;
            inventory.ToggleInventory();
        }
    }
}
