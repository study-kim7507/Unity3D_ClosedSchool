using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private PlayerMovementController movementController;
    private PlayerLookController lookController;

    // �巡�� ����
    [HideInInspector] public Draggable draggable = null;

    // ��ȣ�ۿ� ����
    private GameObject currFocusObject = null;

    // �÷��̾� �κ��丮 ��ȣ�ۿ� ����
    [Header("Inventory")]
    public InventorySystem inventory;
    public ItemDetailViewer itemDetailViewer;
    [HideInInspector] public bool isOpenInventory;
    [HideInInspector] public bool isOpenItemDetailViewer;

    // ������
    [Header("Flashlight")]
    public PlayerFlashlight flashlight;

    // ��
    [Header("Hand, Item Equip")]
    public Transform rightHand;
    public AudioSource forItemConsumeSound;

    // UI ����
    [Header("Player UI")]
    public PlayerUI playerUI;

    // ���¹̳�
    [Header("Player Stamina")]
    public float stamina = 100.0f;

    // ���� ����
    [HideInInspector] public bool isHide = false;

    // �÷��̾��� ���� 
    [Header("For Player Die")]
    [SerializeField] private GameObject libraryGhost;
    [SerializeField] private GameObject oneCorriDorGhost;

    // ���� ����
    [HideInInspector] public bool isPausedGame = false;

    private void Start()
    {
        // ���콺 Ŀ���� ������ �ʰ� ����
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        movementController = GetComponent<PlayerMovementController>(); 
        lookController = GetComponent<PlayerLookController>();

        // �÷��̾ �ͽŰ��� ������ �Ͼ �׾��� �� ������ �ͽŵ� ��Ȱ��ȭ
        libraryGhost.SetActive(false);
        oneCorriDorGhost.SetActive(false);
    }

    private void OnDisable()
    {
        // ������ ����
        movementController.Idle();
        movementController.MoveTo(new Vector3(0, 0, 0));
    }

    private void OnEnable()
    {
        // ī�޶� ���� ȸ���� �ʱ�ȭ
        Camera.main.transform.localRotation = Quaternion.identity;
    }

    private void Update()
    {
        if (isPausedGame) return;

        UpdateRotation();
        UpdateMove();
        PerformInteraction();
        ManageFlashlight();
        ManageInventory();
        TakeAPhoto();

        // ������ ����
        if (Input.GetKeyDown(KeyCode.R) && !isOpenInventory && !isOpenItemDetailViewer)
            DropItemInRightHand();
        if (rightHand.childCount > 0 && Input.GetMouseButtonDown(1))
            ConsumeItemInHand();

        // ���� �Ͻ� ����
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseGame();
    }

    // ���콺 �Է��� ���� ĳ���� ȸ���� ���
    private void UpdateRotation()
    {
        if (isOpenInventory || isHide) return;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        
        lookController.UpdateRotation(mouseX, mouseY);
    }

    // Ű���� �Է��� ���� ĳ���� �̵��� ���
    private void UpdateMove()
    {
        if (isOpenInventory || isHide)
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
            else if (Input.GetKey(KeyCode.LeftShift) && stamina > 0.0f)
            {
                stamina -= Time.deltaTime * 5.0f;
                movementController.Run();
            }
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
            if (hit.collider.gameObject.GetComponent<IInteractable>() != null)
            {
                currFocusObject = hit.collider.gameObject;
                currFocusObject.GetComponent<IInteractable>().BeginFocus();

                // ���� ��ȣ�ۿ� ������ ������Ʈ�� Ray�� ������ ��, �÷��̾�� EŰ�� ���� �ش� ������Ʈ�� ��ȣ�ۿ��� �����ϵ���
                if (Input.GetKeyDown(KeyCode.E))
                {
                    GameObject inHandItem = null;
                    if (rightHand.childCount > 0) inHandItem = rightHand.GetChild(0).gameObject;
                    hit.collider.gameObject.GetComponent<IInteractable>().Interact(inHandItem);
                }
            }
            else
            {
                if (currFocusObject != null)
                    currFocusObject.GetComponent<IInteractable>().EndFocus();
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
        else
        {
            // ���� Ray�� �ƹ��� ������Ʈ�� �������� �ʰ�, GŰ�� �Է� �� ���� �տ� �ִ� ������Ʈ�� �κ��丮�� ����
            if (Input.GetKeyDown(KeyCode.G) && rightHand.childCount > 0)
            {
                if (inventory.HasEmptySlot())
                {
                    inventory.AddToInventory(rightHand.GetChild(0).gameObject);
                }
                else
                {
                    PlayerUI.instance.DisplayInteractionDescription("�κ��丮�� �� ������ �����ϴ�. \n���� �տ���� �ִ� �������� �������� ���߽��ϴ�.");
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
        Vector3 currentItemSize = item.GetComponentInChildren<Collider>().bounds.size;
        float scaleFactor = 0.25f / currentItemSize.magnitude;
        item.transform.localScale *= scaleFactor;

        // ��ġ �� ���� ����
        Vector3 pivotOffset = item.transform.position - item.GetComponent<Collider>().bounds.center;
        pivotOffset *= scaleFactor;

        item.transform.localPosition = Vector3.zero + pivotOffset;
        item.transform.localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f) * item.transform.rotation;

        if (item.TryGetComponent<Rigidbody>(out Rigidbody rb)) rb.useGravity = false;
        if (item.TryGetComponent<Collider>(out Collider collider)) collider.enabled = false;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LibraryGhost"))
        {
            // �ͽŰ��� ������ �Ͼ ���, ������ �Ͼ �ͽ��� �����ϰ� ȭ�鿡 ������ �ͽ��� Ȱ��ȭ
            DeactiveGhost(other.gameObject);
            libraryGhost.SetActive(true);
            PlayerUI.instance.PlayerDie();
        }
        else if (other.gameObject.CompareTag("OneCorridorGhost"))
        {
            // �ͽŰ��� ������ �Ͼ ���, ������ �Ͼ �ͽ��� �����ϰ� ȭ�鿡 ������ �ͽ��� Ȱ��ȭ
            DeactiveGhost(other.gameObject);
            oneCorriDorGhost.SetActive(true);
            PlayerUI.instance.PlayerDie();
        }
    }

    private void DeactiveGhost(GameObject ghost)
    {
        Collider[] colliders = ghost.GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }

        Renderer[] renderers = ghost.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
            renderer.enabled = false;
    }

    private void ConsumeItemInHand()
    {
        if (rightHand.GetChild(0).TryGetComponent<IConsumable>(out IConsumable consumable))
        {
            consumable.Consume(this);
            forItemConsumeSound.PlayOneShot(consumable.ConsumeSound);
            Destroy(rightHand.GetChild(0).gameObject);
        }
    }

    private void PauseGame()
    {
        PlayerUI.instance.PauseGame();
    }
}
