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

    public void OpenItemDetailViewer(InventorySlot slot)
    {
        ownerPlayer.isOpenItemDetailViewer = !ownerPlayer.isOpenItemDetailViewer;
        itemDetailViewerCanvas.SetActive(ownerPlayer.isOpenItemDetailViewer);
        ownerPlayer.playerUI.playerUIPanel.SetActive(false);
        ownerPlayer.inventory.inventoryPanel.SetActive(false);

        currentItem = Instantiate(slot.itemObjectPrefab, itemVisaul);

        if (currentItem.GetComponent<Rigidbody>() != null) currentItem.GetComponent<Rigidbody>().useGravity = false;
        SetLayerRecursivly(currentItem, "ItemDetailViewer");

        currentItem.GetComponent<Pickable>().itemName = slot.itemName;
        currentItem.GetComponent<Pickable>().itemDescription = slot.itemDescription;
        currentItem.GetComponent<Pickable>().itemImage = slot.itemImage.sprite;
        currentItem.GetComponent<Pickable>().itemObjectPrefab = slot.itemObjectPrefab;

        // ���� ������ �������� ������ ���
        if (currentItem.GetComponent<Photo>() != null) currentItem.GetComponent<Photo>().SetCapturedImageUsingTexture2D(slot.photoItemCapturedImage);

        // ������Ʈ�� ȸ���ϸ鼭 ������ �� �ֵ���
        currentItem.AddComponent<ItemDetailViewerObjectRotation>();

        // �ݶ��̴� �� Shader ����
        SetTrigger(currentItem);
        SetUnlitShader(currentItem);

        // ũ�� ����
        Vector3 currentItemSize = currentItem.GetComponentInChildren<Renderer>().bounds.size;
        float scaleFactor = 6.0f / currentItemSize.magnitude;
        currentItem.transform.localScale *= scaleFactor;

        Vector3 pivotOffset = currentItem.transform.position - currentItem.GetComponent<Collider>().bounds.center;
        pivotOffset *= scaleFactor;
       
        currentItem.transform.position += pivotOffset;

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

    private void SetUnlitShader(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            Material[] materials = renderer.materials;
            foreach (Material material in materials)
            {
                material.shader = Shader.Find("Universal Render Pipeline/Unlit");
            }
        }
    }
}
