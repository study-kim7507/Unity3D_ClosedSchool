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
        // ItemDetailViewer Ȱ��ȭ
        ownerPlayer.isOpenItemDetailViewer = true;
        itemDetailViewerCanvas.SetActive(true);

        // �÷��̾� UI, �κ��丮 ��Ȱ��ȭ
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

        // ���� ������ �������� ������ ���
        if (currentItem.TryGetComponent<Photo>(out Photo photo))
        {
            photo.SetPhotoImage(slot.photoItemCapturedImage);
            photo.isInGhost = slot.isInGhost;
        }
        // ������Ʈ�� ȸ���ϸ鼭 ������ �� �ֵ���
        currentItem.AddComponent<ItemDetailViewerObjectRotation>();

        // �ݶ��̴� ����
        SetTrigger(currentItem);

        // ũ�� ����
        Vector3 currentItemSize = currentItem.GetComponentInChildren<Collider>().bounds.size;
        float scaleFactor = 6.0f / currentItemSize.magnitude;
        currentItem.transform.localScale *= scaleFactor;

        Vector3 pivotOffset = currentItem.transform.position - currentItem.GetComponent<Collider>().bounds.center;
        pivotOffset *= scaleFactor;
       
        currentItem.transform.position += pivotOffset;
        currentItem.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f) * currentItem.transform.rotation; ;

        // ĵ���� ����
        itemName.text = slot.itemName;
        itemDescription.text = slot.itemDescription;
    }

    public void CloseItemDetailViewer()
    {
        // ItemDetailViewer ��Ȱ��ȭ
        ownerPlayer.isOpenItemDetailViewer = false;
        itemDetailViewerCanvas.SetActive(false);
       
        Destroy(currentItem);
        currentItem = null;

        // �÷��̾� UI, �κ��丮 Ȱ��ȭ
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
