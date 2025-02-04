using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private PlayerMovementController movementController;
    private PlayerLookController lookController;

    // 드래그 관련
    [HideInInspector] public Draggable draggable = null;

    // 상호작용 관련
    private GameObject currFocusObject = null;

    // 플레이어 인벤토리 상호작용 관련
    [Header("Inventory")]
    public InventorySystem inventory;
    public ItemDetailViewer itemDetailViewer;
    [HideInInspector] public bool isOpenInventory;
    [HideInInspector] public bool isOpenItemDetailViewer;

    // 손전등
    [Header("Flashlight")]
    public PlayerFlashlight flashlight;

    // 손
    [Header("Hand, Item Equip")]
    public Transform rightHand;
    public AudioSource forItemConsumeSound;

    // UI 관련
    [Header("Player UI")]
    public PlayerUI playerUI;

    // 스태미너
    [Header("Player Stamina")]
    public float stamina = 100.0f;

    // 숨기 관련
    [HideInInspector] public bool isHide = false;

    // 플레이어의 죽음 
    [Header("For Player Die")]
    [SerializeField] private GameObject libraryGhost;
    [SerializeField] private GameObject oneCorriDorGhost;

    // 게임 정지
    [HideInInspector] public bool isPausedGame = false;

    private void Start()
    {
        // 마우스 커서를 보이지 않게 설정
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        movementController = GetComponent<PlayerMovementController>(); 
        lookController = GetComponent<PlayerLookController>();

        // 플레이어가 귀신과의 접촉이 일어나 죽었을 때 스폰될 귀신들 비활성화
        libraryGhost.SetActive(false);
        oneCorriDorGhost.SetActive(false);
    }

    private void OnDisable()
    {
        // 움직임 막기
        movementController.Idle();
        movementController.MoveTo(new Vector3(0, 0, 0));
    }

    private void OnEnable()
    {
        // 카메라 로컬 회전값 초기화
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

        // 아이템 관련
        if (Input.GetKeyDown(KeyCode.R) && !isOpenInventory && !isOpenItemDetailViewer)
            DropItemInRightHand();
        if (rightHand.childCount > 0 && Input.GetMouseButtonDown(1))
            ConsumeItemInHand();

        // 게임 일시 정지
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseGame();
    }

    // 마우스 입력을 통한 캐릭터 회전을 담당
    private void UpdateRotation()
    {
        if (isOpenInventory || isHide) return;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        
        lookController.UpdateRotation(mouseX, mouseY);
    }

    // 키보드 입력을 통한 캐릭터 이동을 담당
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

        // 이동 입력 (느리게 걷기, 걷기, 달리기)
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

        // 점프 입력
        if (Input.GetKey(KeyCode.Space)) movementController.Jump();

        // 앉기 입력
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (movementController.isCrouching) movementController.UnCrouch();
            else movementController.Crouch();
        }

        // 최종 이동 방향 설정
        movementController.MoveTo(new Vector3(x, 0, z));
    }

    private void PerformInteraction()
    {
        if (isOpenInventory) return;

        // 카메라에서 바라보는 방향으로 Ray를 쏘아 감지되는 오브젝트가 IIteractable 인터페이스가 구현되었는지 여부를 확인
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2.0f))
        {
            if (hit.collider.gameObject.GetComponent<IInteractable>() != null)
            {
                currFocusObject = hit.collider.gameObject;
                currFocusObject.GetComponent<IInteractable>().BeginFocus();

                // 만약 상호작용 가능한 오브젝트가 Ray에 감지될 시, 플레이어는 E키를 통해 해당 오브젝트와 상호작용이 가능하도록
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
                // 만약 인벤토리에 저장 가능한 오브젝트가 Ray에 감지될 시, 플레이어는 G키를 통해 해당 오브젝트를 인벤토리에 저장하도록
                if (Input.GetKeyDown(KeyCode.G))
                {
                    inventory.AddToInventory(hit.collider.gameObject);
                }
            }
            if (hit.collider.gameObject.GetComponent<Draggable>() != null)
            {   
                // 드래그하여 플레이어가 옮길 수 있는 오브젝트가 Ray에 감지될 시, 플레이어는 마우스 좌클릭을 통해 오브젝트를 옮길 수 있도록
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
            // 만약 Ray에 아무런 오브젝트가 감지되지 않고, G키를 입력 시 현재 손에 있는 오브젝트를 인벤토리에 보관
            if (Input.GetKeyDown(KeyCode.G) && rightHand.childCount > 0)
            {
                if (inventory.HasEmptySlot())
                {
                    inventory.AddToInventory(rightHand.GetChild(0).gameObject);
                }
                else
                {
                    PlayerUI.instance.DisplayInteractionDescription("인벤토리에 빈 공간이 없습니다. \n현재 손에들고 있는 아이템을 보관하지 못했습니다.");
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
     
        // 크기 조절
        Vector3 currentItemSize = item.GetComponentInChildren<Collider>().bounds.size;
        float scaleFactor = 0.25f / currentItemSize.magnitude;
        item.transform.localScale *= scaleFactor;

        // 위치 및 방향 조절
        Vector3 pivotOffset = item.transform.position - item.GetComponent<Collider>().bounds.center;
        pivotOffset *= scaleFactor;

        item.transform.localPosition = Vector3.zero + pivotOffset;
        item.transform.localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f) * item.transform.rotation;

        if (item.TryGetComponent<Rigidbody>(out Rigidbody rb)) rb.useGravity = false;
        if (item.TryGetComponent<Collider>(out Collider collider)) collider.enabled = false;
    }

    private void DropItemInRightHand()
    {
        // 손에 아무런 오브젝트가 없는 경우 바로 종료
        if (rightHand.childCount <= 0) return;

        Vector3 dropPosition = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;

        GameObject currentItem = rightHand.GetChild(0).gameObject;

        currentItem.transform.SetParent(null);
        currentItem.transform.position = dropPosition;

        // 원본 프리팹에서 크기를 가져와 복구 시킴.
        currentItem.transform.localScale = currentItem.GetComponent<Pickable>().itemObjectPrefab.gameObject.transform.localScale;
        
        currentItem.GetComponent<Collider>().enabled = true;
        currentItem.GetComponent<Rigidbody>().useGravity = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LibraryGhost"))
        {
            // 귀신과의 접촉이 일어난 경우, 접촉이 일어난 귀신은 삭제하고 화면에 보여질 귀신을 활성화
            DeactiveGhost(other.gameObject);
            libraryGhost.SetActive(true);
            PlayerUI.instance.PlayerDie();
        }
        else if (other.gameObject.CompareTag("OneCorridorGhost"))
        {
            // 귀신과의 접촉이 일어난 경우, 접촉이 일어난 귀신은 삭제하고 화면에 보여질 귀신을 활성화
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
