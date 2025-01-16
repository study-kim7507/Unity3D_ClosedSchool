using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public string itemName;
    public string itemDescription;
    public Image itemImage;
    public GameObject itemPrefab;
    public Texture2D photoItemCapturedImage;         // ���Կ� ����� �������� ������ ��� ���Ǵ� ����

    public bool isUsed;            // ���� �ش� ������ ��� ������ ���θ� ����

    [SerializeField] ItemDetailViewer itemDetailViewer;

    public void SetSlot(GameObject item)
    {
        IPickable pickableItem = item.GetComponent<IPickable>();

        // ���� ����
        itemName = pickableItem.ItemName;
        itemDescription = pickableItem.ItemDescription;
        itemImage.sprite = pickableItem.ItemImage;
        itemPrefab = pickableItem.ItemObjectPrefab;

        // ���Կ� ����� �������� ������ ���, �÷��̾ ���� ������ �����ǵ���
        if (item.GetComponent<Photo>() != null) photoItemCapturedImage = item.GetComponent<Photo>().CapturedImage.texture as Texture2D;
        
        isUsed = true;  
    }

    public void ShowItemDetail()
    {
        if (!isUsed) return;         // ���� �ش� ���Կ� �������� ����Ǿ� ���� ���� ���
        itemDetailViewer.OpenItemDetailViewer(this);
    }
}
