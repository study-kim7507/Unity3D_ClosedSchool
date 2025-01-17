using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public string itemName;
    public string itemDescription;
    public Image itemImage;
    public GameObject itemObjectPrefab;
    public Texture2D photoItemCapturedImage;         // ���Կ� ����� �������� ������ ��� ���Ǵ� ����

    public bool isUsed;                              // ���� �ش� ������ ��� ������ ���θ� ����

    [SerializeField] ItemDetailViewer itemDetailViewer;

    public void SetSlot(GameObject item)
    {
        Pickable pickableItem = item.GetComponent<Pickable>();

        // ���� ����
        itemName = pickableItem.itemName;
        itemDescription = pickableItem.itemDescription;
        itemImage.sprite = pickableItem.itemImage;
        itemObjectPrefab = pickableItem.itemObjectPrefab;

        // ���Կ� ����� �������� ������ ���, �÷��̾ ���� ������ �����ǵ���
        if (item.GetComponent<Photo>() != null) photoItemCapturedImage = item.GetComponent<Photo>().capturedImage.texture as Texture2D;
        
        isUsed = true;  
    }

    public void ClearSlot()
    {
        itemName = string.Empty;            // ������ �̸� �ʱ�ȭ
        itemDescription = string.Empty;     // ������ ���� �ʱ�ȭ
        itemImage.sprite = null;            // ������ �̹��� �ʱ�ȭ
        itemObjectPrefab = null;                  // ������ ������ �ʱ�ȭ
        photoItemCapturedImage = null;      // ���� ������ �̹��� �ʱ�ȭ

        isUsed = false;                     // ���� ��� �� ���� �ʱ�ȭ
    }

    public void ShowItemDetail()
    {
        if (!isUsed) return;         // ���� �ش� ���Կ� �������� ����Ǿ� ���� ���� ���
        itemDetailViewer.OpenItemDetailViewer(this);
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

        // ���� �������� ������ ���
        if (droppedItem.GetComponent<Photo>() != null) droppedItem.GetComponent<Photo>().capturedImage.texture = photoItemCapturedImage;
        ClearSlot();
    }

    private void SetLayerRecursivly(GameObject obj, string layerName)
    {
        obj.layer = LayerMask.NameToLayer(layerName);

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursivly(child.gameObject, layerName);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            DropCurrentItem();
    }
}
