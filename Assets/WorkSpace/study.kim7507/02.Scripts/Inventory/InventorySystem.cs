using System.Linq;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public GameObject inventoryPanel;

    [SerializeField] Transform inventorySlotHolder;
    private InventorySlot[] inventorySlots;
   
    [SerializeField] PlayerController ownerPlayer;

    void Start()
    {
        inventorySlots = inventorySlotHolder.GetComponentsInChildren<InventorySlot>();
        inventoryPanel.SetActive(ownerPlayer.isOpenInventory);    
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(ownerPlayer.isOpenInventory);
    }

    public void AddToInventory(GameObject item)
    {
        InventorySlot slot = ReturnFreeSlot();
        if (slot == null)
        {
            // TODO: 경고 메시지를 띄우는 방식으로 변경 필요
            Debug.Log("인벤토리가 가득 찼어요!");
            return;
        }

        slot.SetSlot(item);
        Destroy(item);
    }

    public bool HasEmptySlot()
    {
        InventorySlot freeSlot = null;
        freeSlot = inventorySlots.FirstOrDefault(i => i.isUsed == false);

        return freeSlot != null;
    }

    private InventorySlot ReturnFreeSlot()
    {
        InventorySlot freeSlot = null;
        freeSlot = inventorySlots.FirstOrDefault(i => i.isUsed == false);

        return freeSlot;
    }
}
