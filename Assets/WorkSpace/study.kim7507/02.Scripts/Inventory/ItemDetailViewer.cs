using TMPro;
using UnityEngine;

public class ItemDetailViewer : MonoBehaviour
{
    public GameObject itemDetailViewerCanvas;
    public Transform itemVisaul;
    public TMP_Text itemName;
    public TMP_Text itemDescription;

    GameObject currentItem;
    public PlayerController ownerPlayer;

    public void OpenItemDetailViewerBySlot(InventorySlot slot)
    {
        // ItemDetailViewer 활성화
        ownerPlayer.isOpenItemDetailViewer = true;
        itemDetailViewerCanvas.SetActive(true);

        // 플레이어 UI, 인벤토리 비활성화
        ownerPlayer.playerUI.playerUIPanel.SetActive(false);
        ownerPlayer.inventory.inventoryPanel.SetActive(false);

        currentItem = Instantiate(slot.itemObjectPrefab, itemVisaul);

        if (currentItem.TryGetComponent<Rigidbody>(out Rigidbody rb)) rb.GetComponent<Rigidbody>().useGravity = false;
        SetLayerRecursivly(currentItem, "ItemDetailViewer");

        if (currentItem.TryGetComponent<Pickable>(out Pickable pickable))
        {
            pickable.itemName = slot.itemName;
            pickable.itemDescription = slot.itemDescription;
            pickable.itemImage = slot.itemImage.sprite;
            pickable.itemObjectPrefab = slot.itemObjectPrefab;
        }

        // 현재 보여줄 아이템이 사진인 경우
        if (currentItem.TryGetComponent<Photo>(out Photo photo))
        {
            photo.SetPhotoImage(slot.photoItemCapturedImage);
            photo.isInGhost = slot.isInGhost;
        }
        // 오브젝트가 회전하면서 보여질 수 있도록
        currentItem.AddComponent<ItemDetailViewerObjectRotation>();

        // 콜라이더 설정
        SetTrigger(currentItem);

        // 크기 조절
        Vector3 currentItemSize = currentItem.GetComponentInChildren<Collider>().bounds.size;
        float scaleFactor = 6.0f / currentItemSize.magnitude;
        currentItem.transform.localScale *= scaleFactor;

        Vector3 pivotOffset = currentItem.transform.position - currentItem.GetComponent<Collider>().bounds.center;
        pivotOffset *= scaleFactor;
       
        currentItem.transform.position += pivotOffset;
        currentItem.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f) * currentItem.transform.rotation; ;

        // 캔버스 설정
        itemName.text = slot.itemName;
        itemDescription.text = slot.itemDescription;
    }

    public void CloseItemDetailViewer()
    {
        // ItemDetailViewer 비활성화
        ownerPlayer.isOpenItemDetailViewer = false;
        itemDetailViewerCanvas.SetActive(false);
       
        Destroy(currentItem);
        currentItem = null;

        // 플레이어 UI, 인벤토리 활성화
        ownerPlayer.inventory.inventoryPanel.SetActive(true);
        ownerPlayer.playerUI.playerUIPanel.SetActive(true);
    }

    private void SetLayerRecursivly(GameObject obj, string layerName)
    {
        obj.layer = LayerMask.NameToLayer(layerName);

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursivly(child.gameObject, layerName);
        }
    }


    private void SetTrigger(GameObject obj)
    {
        Collider[] colliders = obj.GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            collider.isTrigger = true;
        }
    }
}
