using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventorySlot : MonoBehaviour
{
    public bool isUsed;                              // ���� �ش� ������ ��� ������ ���θ� ����

    [Header("Item Information")]
    public string itemName;
    public string itemDescription;
    public Image itemImage;
    public GameObject itemObjectPrefab;

    public Texture2D photoItemCapturedImage;         // ���Կ� ����� �������� ������ ��� ���Ǵ� ����
    public bool isInGhost;                           // ���Կ� ����� �������� ������ ��� ���Ǵ� ����

    private PlayerController ownerPlayer;
    private ItemDetailViewer itemDetailViewer;

    private void Start()
    {
        ownerPlayer = transform.root.gameObject.GetComponent<InventorySystem>().ownerPlayer;
        itemDetailViewer = ownerPlayer.itemDetailViewer;
    }

    public void SetSlot(GameObject item)
    {
        gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(255.0f, 255.0f, 255.0f, 255.0f);
        
        Pickable pickableItem = item.GetComponent<Pickable>();

        // ���� ����
        itemName = pickableItem.itemName;
        itemDescription = pickableItem.itemDescription;
        itemImage.sprite = pickableItem.itemImage;
        itemObjectPrefab = pickableItem.itemObjectPrefab;

        // ���Կ� ����� �������� ������ ���
        if (item.TryGetComponent<Photo>(out Photo photo))
        {
            photoItemCapturedImage = photo.imageMeshRenderer.sharedMaterial.mainTexture as Texture2D;
            isInGhost = photo.isInGhost;
        }

        isUsed = true;  
    }

    public void ClearSlot()
    {
        itemName = string.Empty;            // ������ �̸� �ʱ�ȭ
        itemDescription = string.Empty;     // ������ ���� �ʱ�ȭ
        itemImage.sprite = null;            // ������ �̹��� �ʱ�ȭ
        itemObjectPrefab = null;            // ������ ������ �ʱ�ȭ
        photoItemCapturedImage = null;      // ���� ������ �̹��� �ʱ�ȭ

        gameObject.transform.GetChild(0).GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        
        isUsed = false;                     // ���� ��� �� ���� �ʱ�ȭ
    }

    // ������ ������ �並 ���ų�, ���� �÷��̾ �տ� ��� �ִ� �����۰� �ٲٴ� ����� ����
    public void ClickSlot()
    {
        if (!isUsed) return;         // ���� �ش� ���Կ� �������� ����Ǿ� ���� ���� ���

        if (!Input.GetKey(KeyCode.G)&& !Input.GetKey(KeyCode.R)) itemDetailViewer.OpenItemDetailViewerBySlot(this);
        else if (Input.GetKey(KeyCode.G)) ChangeItem();
        else if (Input.GetKey(KeyCode.R)) DropCurrentItem();
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
        if (droppedItem.TryGetComponent<Photo>(out Photo photo))
        {
            photo.imageMeshRenderer.material.mainTexture = photoItemCapturedImage;
            photo.isInGhost = isInGhost;
        }

        ClearSlot();
    }

    private void ChangeItem()
    {
        if (ownerPlayer != null)
        {
            GameObject currentItem = Instantiate(itemObjectPrefab);

            if (currentItem.TryGetComponent<Pickable>(out Pickable pickable))
            {
                pickable.itemName = itemName;
                pickable.itemDescription = itemDescription;
                pickable.itemImage = itemImage.sprite;
                pickable.itemObjectPrefab = itemObjectPrefab;
            }

            // ���� ������ �������� ������ ���
            if (currentItem.TryGetComponent<Photo>(out Photo photo))
            {
                photo.SetPhotoImage(photoItemCapturedImage);
                photo.isInGhost = isInGhost;
            }

            if (ownerPlayer.rightHand.childCount <= 0) ClearSlot(); // �տ� ���� �������� ���� ���, ���� Ŭ����
            else
            {
                // �տ� ���� �������� ������ �ִ� ���, ���� ������ �տ� �ִ� ���������� ���� �� �ı�
                GameObject inHandItem = ownerPlayer.rightHand.GetChild(0).gameObject;
                SetSlot(inHandItem);
                Destroy(inHandItem);
            }

            // ���� ������ �������� �տ� ����
            ownerPlayer.inventory.inventoryPanel.SetActive(false);
            ownerPlayer.EquipItemInRightHand(currentItem);
        }
    }
}
