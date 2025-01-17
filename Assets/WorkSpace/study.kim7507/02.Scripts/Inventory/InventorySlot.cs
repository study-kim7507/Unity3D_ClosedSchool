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
    public Texture2D photoItemCapturedImage;         // 슬롯에 저장될 아이템이 사진인 경우 사용되는 변수

    public bool isUsed;                              // 현재 해당 슬롯이 사용 중인지 여부를 저장

    [SerializeField] ItemDetailViewer itemDetailViewer;

    public void SetSlot(GameObject item)
    {
        Pickable pickableItem = item.GetComponent<Pickable>();

        // 슬롯 세팅
        itemName = pickableItem.itemName;
        itemDescription = pickableItem.itemDescription;
        itemImage.sprite = pickableItem.itemImage;
        itemObjectPrefab = pickableItem.itemObjectPrefab;

        // 슬롯에 저장될 아이템이 사진인 경우, 플레이어가 찍은 사진이 설정되도록
        if (item.GetComponent<Photo>() != null) photoItemCapturedImage = item.GetComponent<Photo>().capturedImage.texture as Texture2D;
        
        isUsed = true;  
    }

    public void ClearSlot()
    {
        itemName = string.Empty;            // 아이템 이름 초기화
        itemDescription = string.Empty;     // 아이템 설명 초기화
        itemImage.sprite = null;            // 아이템 이미지 초기화
        itemObjectPrefab = null;                  // 아이템 프리팹 초기화
        photoItemCapturedImage = null;      // 사진 아이템 이미지 초기화

        isUsed = false;                     // 슬롯 사용 중 상태 초기화
    }

    public void ShowItemDetail()
    {
        if (!isUsed) return;         // 현재 해당 슬롯에 아이템이 저장되어 있지 않은 경우
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

        // 버릴 아이템이 사진인 경우
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
