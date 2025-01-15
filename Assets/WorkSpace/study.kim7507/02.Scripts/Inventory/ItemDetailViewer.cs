using TMPro;
using UnityEngine;

public class ItemDetailViewer : MonoBehaviour
{
    public GameObject itemDetailViewerCanvas;
    public Transform itemVisaul;
    public TMP_Text itemName;
    public TMP_Text itemDescription;

    GameObject currentItem;

    [SerializeField] PlayerController ownerPlayer;

    private void Start()
    {
        itemDetailViewerCanvas.SetActive(ownerPlayer.isOpenItemDetailViewer);
    }

    public void OpenItemDetailViewer(InventorySlot slot)
    {
        ownerPlayer.isOpenItemDetailViewer = !ownerPlayer.isOpenItemDetailViewer;
        itemDetailViewerCanvas.SetActive(ownerPlayer.isOpenItemDetailViewer);
        ownerPlayer.inventory.inventoryPanel.SetActive(false);

        currentItem = Instantiate(slot.itemPrefab, itemVisaul);
        currentItem.layer = LayerMask.NameToLayer("UI");

        // TODO: 오브젝트마다 크기가 달라서 캔버스에 보여지는 모습이 다른 문제를 해결해야 함.
        currentItem.AddComponent<ItemDetailViewerObjectRotation>();
        currentItem.transform.localScale *= 1000.0f;

        itemName.text = slot.itemName;
        itemDescription.text = slot.itemDescription;
    }

    public void CloseItemDetailViewer()
    {
        ownerPlayer.isOpenItemDetailViewer = !ownerPlayer.isOpenItemDetailViewer;
        itemDetailViewerCanvas.SetActive(ownerPlayer.isOpenItemDetailViewer);  
       
        Destroy(currentItem);
        currentItem = null;

        ownerPlayer.inventory.inventoryPanel.SetActive(true);
    }
}
