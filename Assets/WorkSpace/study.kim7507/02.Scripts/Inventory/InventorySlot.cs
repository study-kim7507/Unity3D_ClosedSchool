using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private string itemName;
    private string itemDescription;
    private Image itemImage;
    private GameObject itemPrefab;

    public bool isUsed;            // 현재 해당 슬롯이 사용 중인지 여부를 저장

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
