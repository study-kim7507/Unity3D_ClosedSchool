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
    public GameObject itemPrefab;
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
        itemPrefab = pickableItem.itemObjectPrefab;

        // ���Կ� ����� �������� ������ ���, �÷��̾ ���� ������ �����ǵ���
        if (item.GetComponent<Photo>() != null) photoItemCapturedImage = item.GetComponent<Photo>().capturedImage.texture as Texture2D;
        
        isUsed = true;  
    }

    public void ClearSlot()
    {
        itemName = string.Empty;            // ������ �̸� �ʱ�ȭ
        itemDescription = string.Empty;     // ������ ���� �ʱ�ȭ
        itemImage.sprite = null;            // ������ �̹��� �ʱ�ȭ
        itemPrefab = null;                  // ������ ������ �ʱ�ȭ
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

        Vector3 dropPosition;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if ((Physics.Raycast(ray, out hit, 1.5f))) dropPosition = hit.point + Camera.main.transform.forward + hit.normal * 1.0f;
        else dropPosition = Camera.main.transform.position + Camera.main.transform.forward * 1.5f;

        GameObject droppedItem = Instantiate(itemPrefab, dropPosition, Random.rotation);

        droppedItem.GetComponent<Pickable>().itemName = itemName;
        droppedItem.GetComponent<Pickable>().itemDescription = itemDescription;
        droppedItem.GetComponent<Pickable>().itemImage = itemImage.sprite;
        droppedItem.GetComponent<Pickable>().itemObjectPrefab = itemPrefab;

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
