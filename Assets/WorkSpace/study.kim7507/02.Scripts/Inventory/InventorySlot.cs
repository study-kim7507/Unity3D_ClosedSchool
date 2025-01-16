using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public string itemName;
    public string itemDescription;
    public Image itemImage;
    public GameObject itemPrefab;
    public Texture2D photoItemCapturedImage;         // 슬롯에 저장될 아이템이 사진인 경우 사용되는 변수

    public bool isUsed;            // 현재 해당 슬롯이 사용 중인지 여부를 저장

    [SerializeField] ItemDetailViewer itemDetailViewer;

    public void SetSlot(GameObject item)
    {
        Pickable pickableItem = item.GetComponent<Pickable>();

        // 슬롯 세팅
        itemName = pickableItem.itemName;
        itemDescription = pickableItem.itemDescription;
        itemImage.sprite = pickableItem.itemImage;
        itemPrefab = pickableItem.itemObjectPrefab;

        // 슬롯에 저장될 아이템이 사진인 경우, 플레이어가 찍은 사진이 설정되도록
        if (item.GetComponent<Photo>() != null) photoItemCapturedImage = item.GetComponent<Photo>().capturedImage.texture as Texture2D;
        
        isUsed = true;  
    }

    public void ShowItemDetail()
    {
        if (!isUsed) return;         // 현재 해당 슬롯에 아이템이 저장되어 있지 않은 경우
        itemDetailViewer.OpenItemDetailViewer(this);
    }
}
