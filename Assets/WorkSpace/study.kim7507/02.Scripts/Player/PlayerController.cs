using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerController : MonoBehaviour
{
    private PlayerMovementController movementController;
    private PlayerLookController lookController;

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

    // UI ����
    public GameObject playerUI;

    private void Start()
    {
        // ���콺 Ŀ���� ������ �ʰ� ����
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        movementController = GetComponent<PlayerMovementController>(); 
        lookController = GetComponent<PlayerLookController>();
    }

    private void Update()
    {
        UpdateRotation();
        UpdateMove();
        PerformInteraction();
        ManageFlashlight();
        ManageInventory();
        TakeAPhoto();

        if (Input.GetKeyDown(KeyCode.R) && !isOpenInventory && !isOpenItemDetailViewer)
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
                // TODO: ���� �տ� ��� �ִ� ������Ʈ�� ������ ��ȣ�ۿ� ���� (ex. �����Ϳ� ����)
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
        if (isOpenInventory) return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            gameObject.GetComponent<TakePhoto>().Capture();
        }
    }

    public void EquipItemInRightHand(GameObject item)
    {
        if (isOpenItemDetailViewer) isOpenItemDetailViewer = false;
        if (isOpenInventory) isOpenInventory = false;
        
        Cursor.lockState = CursorLockMode.Locked;
        if(Cursor.visible) Cursor.visible = false;

        item.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        item.transform.SetParent(rightHand);
     
        // ũ�� ����
        Vector3 currentItemSize = item.GetComponentInChildren<Renderer>().bounds.size;
        float scaleFactor = 0.25f / currentItemSize.magnitude;
        item.transform.localScale *= scaleFactor;

        // ��ġ �� ���� ����
        Vector3 pivotOffset = item.transform.position - item.GetComponent<Collider>().bounds.center;
        pivotOffset *= scaleFactor;

        item.transform.localPosition = Vector3.zero + pivotOffset;
        item.transform.localRotation = Quaternion.identity;

        if (item.GetComponent<Rigidbody>() != null) item.GetComponent<Rigidbody>().useGravity = false;
        if (item.GetComponent<Collider>() != null) item.GetComponent<Collider>().enabled = false;
    }

    private void DropItemInRightHand()
    {
        // �տ� �ƹ��� ������Ʈ�� ���� ��� �ٷ� ����
        if (rightHand.childCount <= 0) return;

        Vector3 dropPosition = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;

        GameObject currentItem = rightHand.GetChild(0).gameObject;

        currentItem.transform.SetParent(null);
        currentItem.transform.position = dropPosition;

        // ���� �����տ��� ũ�⸦ ������ ���� ��Ŵ.
        currentItem.transform.localScale = currentItem.GetComponent<Pickable>().itemObjectPrefab.gameObject.transform.localScale;
        
        currentItem.GetComponent<Collider>().enabled = true;
        currentItem.GetComponent<Rigidbody>().useGravity = true;
    }
}
