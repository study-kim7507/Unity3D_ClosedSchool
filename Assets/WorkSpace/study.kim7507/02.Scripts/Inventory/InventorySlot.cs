using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private string itemName;
    private string itemDescription;
    private Image itemImage;
    private GameObject itemPrefab;

    public bool isUsed;            // ���� �ش� ������ ��� ������ ���θ� ����

    [SerializeField] Item3DViewer item3DViewer;

    public void SetSlot(GameObject item)
    {
        IPickable pickableItem = item.GetComponent<IPickable>();

        itemName = pickableItem.ItemName;
        itemDescription = pickableItem.ItemDescription;
        itemImage.sprite = pickableItem.ItemImage;
        itemPrefab = pickableItem.ItemObjectPrefab;
        isUsed = true;  
    }

    public void ShowItemDetails()
    {
        item3DViewer.OpenItem3DViewer(itemPrefab);
    }
}
