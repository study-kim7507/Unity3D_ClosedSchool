using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovementController movementController;
    private PlayerLookController lookController;
    private PlayerStatus status;

    // �÷��̾� �κ��丮 ��ȣ�ۿ� ����
    public InventorySystem inventory;
    [HideInInspector] public bool isOpenInventory;
    [HideInInspector] public bool isOpenItemDetailViewer;

    // ������
    [SerializeField] PlayerFlashlight flashlight;

    // �巡�� ����
    private Draggable draggable = null;

    // ��
    public Transform rightHand;

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
        TakeAPhoto();
        DropItemInRightHand();
    }

    // ���콺 �Է��� ���� ĳ���� ȸ���� ���
    private void UpdateRotation()
    {
        if (isOpenInventory) return;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        lookController.UpdateRotation(mouseX, mouseY);
    }

    // Ű���� �Է��� ���� ĳ���� �̵��� ���
    private void UpdateMove()
    {
        if (isOpenInventory)
        {
            movementController.Idle();
            movementController.MoveTo(new Vector3(0, 0, 0));
            return;
        }

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
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (movementController.isCrouching) movementController.UnCrouch();
            else movementController.Crouch();
        }

        // ���� �̵� ���� ����
        movementController.MoveTo(new Vector3(x, 0, z));
    }

    private void PerformInteraction()
    {
        if (isOpenInventory) return;

        // ī�޶󿡼� �ٶ󺸴� �������� Ray�� ��� �����Ǵ� ������Ʈ�� IIteractable �������̽��� �����Ǿ����� ���θ� Ȯ��
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2.0f))
        {
            // ������ �������� �浹 ���� ���̿� ����� ���� �׸���
            Debug.DrawLine(ray.origin, hit.point, Color.red);

            if (hit.collider.gameObject.GetComponent<IInteractable>() != null)
            {
                // ���� ��ȣ�ۿ� ������ ������Ʈ�� Ray�� ������ ��, �÷��̾�� EŰ�� ���� �ش� ������Ʈ�� ��ȣ�ۿ��� �����ϵ���
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.gameObject.GetComponent<IInteractable>().Interact();
                }
            }
            if (hit.collider.gameObject.GetComponent<Pickable>() != null)
            {
                // ���� �κ��丮�� ���� ������ ������Ʈ�� Ray�� ������ ��, �÷��̾�� GŰ�� ���� �ش� ������Ʈ�� �κ��丮�� �����ϵ���
                if (Input.GetKeyDown(KeyCode.G))
                {
                    inventory.AddToInventory(hit.collider.gameObject);
                }
            }
            if (hit.collider.gameObject.GetComponent<Draggable>() != null)
            {   
                // �巡���Ͽ� �÷��̾ �ű� �� �ִ� ������Ʈ�� Ray�� ������ ��, �÷��̾�� ���콺 ��Ŭ���� ���� ������Ʈ�� �ű� �� �ֵ���
                if (draggable == null)
                {
                    Draggable currentDraggable = hit.collider.gameObject.GetComponent<Draggable>();

                    if (!currentDraggable.isDragging && Input.GetMouseButton(0))
                    {
                        currentDraggable.BeginDrag();
                        draggable = currentDraggable;
                    }
                }
                else
                {
                    if (draggable.isDragging && !Input.GetMouseButton(0))
                    {
                        draggable.EndDrag();
                        draggable = null;
                    }
                }
            }
        }
    }

    private void ManageFlashlight()
    {
        if (isOpenInventory) return;

        if (Input.GetKeyDown(KeyCode.V))
        {
            flashlight.ToggleFlashlight();
        }
    }

    private void ManageInventory()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isOpenItemDetailViewer) return;

            if (!isOpenInventory) Cursor.lockState = CursorLockMode.None;
            else Cursor.lockState = CursorLockMode.Locked;

            isOpenInventory = !isOpenInventory;
            Cursor.visible = !Cursor.visible;

            inventory.ToggleInventory();
        }
    }
    
    private void TakeAPhoto()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            gameObject.GetComponent<TakePhoto>().Capture();
        }
    }

    public void EquipItemInRightHand(GameObject item)
    {
        isOpenItemDetailViewer = !isOpenItemDetailViewer;
        isOpenInventory = !isOpenInventory;

        if (!isOpenInventory) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = !Cursor.visible;

        // ���� �����տ� ������ �ִ� ������Ʈ ������
        DropItemInRightHand();
        item.transform.SetParent(rightHand);
        item.transform.localPosition = Vector3.zero;
        item.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        item.transform.localRotation = Quaternion.identity;
    }

    private void DropItemInRightHand()
    {
        // �տ� �ƹ��� ������Ʈ�� ���� ��� �ٷ� ����
        if (rightHand.childCount <= 0) return;
        if (Input.GetKeyDown(KeyCode.R))
        {
            // TODO: ������ ������ ���� ���� �ʿ�
            Debug.Log("�������� �����ϴ�.");

            Vector3 dropPosition = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;

            rightHand.GetChild(0).transform.position = dropPosition;
            rightHand.GetChild(0).transform.SetParent(null);
        }
    }
}
