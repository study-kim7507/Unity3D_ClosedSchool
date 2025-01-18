using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventorySlot : MonoBehaviour
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
        itemObjectPrefab = null;            // ������ ������ �ʱ�ȭ
        photoItemCapturedImage = null;      // ���� ������ �̹��� �ʱ�ȭ

        isUsed = false;                     // ���� ��� �� ���� �ʱ�ȭ
    }

    // ������ ������ �並 ���ų�, ���� �÷��̾ �տ� ��� �ִ� �����۰� �ٲٴ� ����� ����
    public void ClickSlot()
    {
        if (!isUsed) return;         // ���� �ش� ���Կ� �������� ����Ǿ� ���� ���� ���

        if (!Input.GetKey(KeyCode.G)&& !Input.GetKey(KeyCode.R)) itemDetailViewer.OpenItemDetailViewer(this);
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
        if (droppedItem.GetComponent<Photo>() != null) droppedItem.GetComponent<Photo>().capturedImage.texture = photoItemCapturedImage;
        ClearSlot();
    }

    private void ChangeItem()
    {
        PlayerController ownerPlayer = transform.root.gameObject.GetComponent<InventorySystem>().ownerPlayer;
        if (ownerPlayer != null)
        {
            GameObject currentItem = Instantiate(itemObjectPrefab);

            if (currentItem.GetComponent<Rigidbody>() != null) currentItem.GetComponent<Rigidbody>().useGravity = false;
            if (currentItem.GetComponent<Collider>() != null) currentItem.GetComponent<Collider>().enabled = false;

            currentItem.GetComponent<Pickable>().itemName = itemName;
            currentItem.GetComponent<Pickable>().itemDescription = itemDescription;
            currentItem.GetComponent<Pickable>().itemImage = itemImage.sprite;
            currentItem.GetComponent<Pickable>().itemObjectPrefab = itemObjectPrefab;

            // ���� ������ �������� ������ ���
            if (currentItem.GetComponent<Photo>() != null) currentItem.GetComponent<Photo>().SetCapturedImageUsingTexture2D(photoItemCapturedImage);

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


    private void SetLayerRecursivly(GameObject obj, string layerName)
    {
        obj.layer = LayerMask.NameToLayer(layerName);

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursivly(child.gameObject, layerName);
        }
    }

    
}
