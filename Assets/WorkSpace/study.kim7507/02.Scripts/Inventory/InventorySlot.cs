using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public string itemName;
    public string itemDescription;
    public Image itemImage;
    public GameObject itemPrefab;

    public bool isUsed;            // 현재 해당 슬롯이 사용 중인지 여부를 저장

    [SerializeField] ItemDetailViewer itemDetailViewer;

    public void SetSlot(GameObject item)
    {
        IPickable pickableItem = item.GetComponent<IPickable>();

        // 슬롯 세팅
        itemName = pickableItem.ItemName;
        itemDescription = pickableItem.ItemDescription;
        itemImage.sprite = pickableItem.ItemImage;
        itemPrefab = pickableItem.ItemObjectPrefab;
       
        isUsed = true;  
    }

    public void ShowItemDetail()
    {
        if (!isUsed) return;         // 현재 해당 슬롯에 아이템이 저장되어 있지 않은 경우
        itemDetailViewer.OpenItemDetailViewer(this);
    }
}
