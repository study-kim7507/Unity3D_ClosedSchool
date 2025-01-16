using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovementController movementController;
    private PlayerLookController lookController;
    private PlayerStatus status;

    // 플레이어 인벤토리 상호작용 관련
    public InventorySystem inventory;
    [HideInInspector] public bool isOpenInventory;
    [HideInInspector] public bool isOpenItemDetailViewer;

    // 손전등
    [SerializeField] PlayerFlashlight flashlight;

    // 드래그 중인 오브젝트
    private Draggable currentDragging = null;
    
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
        PerformInteraction();
        ManageFlashlight();
        ManageInventory();

        if (Input.GetKeyDown(KeyCode.R))
        {
            gameObject.GetComponent<TakePhoto>().Capture();
        }
    }

    // 마우스 입력을 통한 캐릭터 회전을 담당
    private void UpdateRotation()
    {
        if (isOpenInventory) return;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        lookController.UpdateRotation(mouseX, mouseY);
    }

    // 키보드 입력을 통한 캐릭터 이동을 담당
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

        if (Physics.Raycast(ray, out hit, 1.5f))
        {
            // 레이의 시작점과 충돌 지점 사이에 디버그 라인 그리기
            Debug.DrawLine(ray.origin, hit.point, Color.red);

            if (hit.collider.gameObject.GetComponent<IInteractable>() != null)
            {
                // 만약 상호작용 가능한 오브젝트가 Ray에 감지될 시, 플레이어는 E키를 통해 해당 오브젝트와 상호작용이 가능하도록
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.gameObject.GetComponent<IInteractable>().Interact();
                }
            }
            if (hit.collider.gameObject.GetComponent<Pickable>() != null)
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    inventory.AddToInventory(hit.collider.gameObject);
                }
            }
            if (hit.collider.gameObject.GetComponent<Draggable>() != null)
            {
                Draggable draggable = hit.collider.gameObject.GetComponent<Draggable>();

                if (!draggable.isDragging && Input.GetMouseButtonDown(0))
                {
                    draggable.BeginDrag();
                    currentDragging = draggable;
                }
                else if (draggable.isDragging && Input.GetMouseButtonUp(0))
                {
                    draggable.EndDrag();
                    currentDragging = null;
                }
                if (Input.GetMouseButton(0) && draggable.isDragging) draggable.Dragging();
            }
        }

        // 마우스를 빠르게 움직여 드래그 중인 오브젝트가 레이의 감지에서 벗어난 경우
        else
        {
            if (currentDragging != null)
            {
                currentDragging.Dragging();
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
}
