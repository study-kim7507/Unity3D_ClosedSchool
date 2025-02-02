using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventorySlot : MonoBehaviour
{
    public bool isUsed;                              // 현재 해당 슬롯이 사용 중인지 여부를 저장

    [Header("Item Information")]
    public string itemName;
    public string itemDescription;
    public Image itemImage;
    public GameObject itemObjectPrefab;

    public Texture2D photoItemCapturedImage;         // 슬롯에 저장될 아이템이 사진인 경우 사용되는 변수
    public bool isInGhost;                           // 슬롯에 저장될 아이템이 사진인 경우 사용되는 변수

    private PlayerController ownerPlayer;
    private ItemDetailViewer itemDetailViewer;

    private void Start()
    {
        ownerPlayer = transform.root.gameObject.GetComponent<InventorySystem>().ownerPlayer;
        itemDetailViewer = ownerPlayer.itemDetailViewer;
    }

    public void SetSlot(GameObject item)
    {
        gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(255.0f, 255.0f, 255.0f, 255.0f);
        
        Pickable pickableItem = item.GetComponent<Pickable>();

        // 슬롯 세팅
        itemName = pickableItem.itemName;
        itemDescription = pickableItem.itemDescription;
        itemImage.sprite = pickableItem.itemImage;
        itemObjectPrefab = pickableItem.itemObjectPrefab;

        // 슬롯에 저장될 아이템이 사진인 경우
        if (item.TryGetComponent<Photo>(out Photo photo))
        {
            photoItemCapturedImage = photo.imageMeshRenderer.sharedMaterial.mainTexture as Texture2D;
            isInGhost = photo.isInGhost;
        }

        isUsed = true;  
    }

    public void ClearSlot()
    {
        itemName = string.Empty;            // 아이템 이름 초기화
        itemDescription = string.Empty;     // 아이템 설명 초기화
        itemImage.sprite = null;            // 아이템 이미지 초기화
        itemObjectPrefab = null;            // 아이템 프리팹 초기화
        photoItemCapturedImage = null;      // 사진 아이템 이미지 초기화

        gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        
        isUsed = false;                     // 슬롯 사용 중 상태 초기화
    }

    // 아이템 디테일 뷰를 보거나, 현재 플레이어가 손에 들고 있는 아이템과 바꾸는 기능을 수행
    public void ClickSlot()
    {
        if (!isUsed) return;         // 현재 해당 슬롯에 아이템이 저장되어 있지 않은 경우

        if (!Input.GetKey(KeyCode.G)&& !Input.GetKey(KeyCode.R)) itemDetailViewer.OpenItemDetailViewerBySlot(this);
        else if (Input.GetKey(KeyCode.G)) ChangeItem();
        else if (Input.GetKey(KeyCode.R)) DropCurrentItem();
    }

    public void DropCurrentItem()
    {
        if (!isUsed) return;

        Vector3 dropPosition = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;

        GameObject droppedItem = Instantiate(itemObjectPrefab, dropPosition, Random.rotation);

        droppedItem.GetComponent<Pickable>().itemName = itemName;
        droppedItem.GetComponent<Pickable>().itemDescription = itemDescription;
        droppedItem.GetComponent<Pickable>().itemImage = itemImage.sprite;
        droppedItem.GetComponent<Pickable>().itemObjectPrefab = itemObjectPrefab;

        // 버릴 아이템이 사진인 경우
        if (droppedItem.TryGetComponent<Photo>(out Photo photo))
        {
            photo.imageMeshRenderer.material.mainTexture = photoItemCapturedImage;
            photo.isInGhost = isInGhost;
        }

        ClearSlot();
    }

    private void ChangeItem()
    {
        if (ownerPlayer != null)
        {
            GameObject currentItem = Instantiate(itemObjectPrefab);

            if (currentItem.TryGetComponent<Pickable>(out Pickable pickable))
            {
                pickable.itemName = itemName;
                pickable.itemDescription = itemDescription;
                pickable.itemImage = itemImage.sprite;
                pickable.itemObjectPrefab = itemObjectPrefab;
            }

            // 현재 보여줄 아이템이 사진인 경우
            if (currentItem.TryGetComponent<Photo>(out Photo photo))
            {
                photo.SetPhotoImage(photoItemCapturedImage);
                photo.isInGhost = isInGhost;
            }

            if (ownerPlayer.rightHand.childCount <= 0) ClearSlot(); // 손에 현재 아이템이 없는 경우, 슬롯 클리어
            else
            {
                // 손에 현재 아이템을 가지고 있는 경우, 현재 슬롯을 손에 있는 아이템으로 변경 후 파괴
                GameObject inHandItem = ownerPlayer.rightHand.GetChild(0).gameObject;
                SetSlot(inHandItem);
                Destroy(inHandItem);
            }

            // 현재 슬롯의 아이템을 손에 장착
            ownerPlayer.inventory.inventoryPanel.SetActive(false);
            ownerPlayer.EquipItemInRightHand(currentItem);
        }
    }
}
