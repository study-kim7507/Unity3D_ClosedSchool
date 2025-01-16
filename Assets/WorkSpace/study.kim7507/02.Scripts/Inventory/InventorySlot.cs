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

    public void ShowItemDetail()
    {
        if (!isUsed) return;         // ���� �ش� ���Կ� �������� ����Ǿ� ���� ���� ���
        itemDetailViewer.OpenItemDetailViewer(this);
    }
}
