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

        currentItem = Instantiate(slot.itemObjectPrefab, itemVisaul);

        if (currentItem.GetComponent<Rigidbody>() != null) currentItem.GetComponent<Rigidbody>().useGravity = false;
        SetLayerRecursivly(currentItem, "UI");

        currentItem.GetComponent<Pickable>().itemName = slot.itemName;
        currentItem.GetComponent<Pickable>().itemDescription = slot.itemDescription;
        currentItem.GetComponent<Pickable>().itemImage = slot.itemImage.sprite;
        currentItem.GetComponent<Pickable>().itemObjectPrefab = slot.itemObjectPrefab;

        // ���� ������ �������� ������ ���
        if (currentItem.GetComponent<Photo>() != null) currentItem.GetComponent<Photo>().SetCapturedImageUsingTexture2D(slot.photoItemCapturedImage);

        // ������Ʈ�� ȸ���ϸ鼭 ������ �� �ֵ���
        currentItem.AddComponent<ItemDetailViewerObjectRotation>();

        // ũ�� ����
        Vector3 currentItemSize = currentItem.GetComponentInChildren<Renderer>().bounds.size;
        float scaleFactor = 6.0f / currentItemSize.magnitude;
        currentItem.transform.localScale *= scaleFactor;

        // ĵ���� ����
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

    private void SetLayerRecursivly(GameObject obj, string layerName)
    {
        obj.layer = LayerMask.NameToLayer(layerName);

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursivly(child.gameObject, layerName);
        }
    }
}
