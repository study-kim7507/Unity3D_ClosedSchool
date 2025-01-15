using System.Linq;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public GameObject inventoryPanel;

    [SerializeField] Transform inventorySlotHolder;
    private InventorySlot[] inventorySlots;
    
    private bool activeInventory = false;

    void Start()
    {
        inventorySlots = inventorySlotHolder.GetComponentsInChildren<InventorySlot>();
        inventoryPanel.SetActive(activeInventory);    
    }

    public void ToggleInventory()
    {
        activeInventory = !activeInventory;
        inventoryPanel.SetActive(activeInventory);
    }

    public void AddToInventory(GameObject item)
    {
        InventorySlot slot = ReturnFreeSlot();
        if (slot == null)
        {
            Debug.Log("ÀÎº¥Åä¸®°¡ °¡µæ Ã¡¾î¿ä!");
            return;
        }

        slot.SetSlot(item);
        Destroy(item);
    }

    private InventorySlot ReturnFreeSlot()
    {
        InventorySlot freeSlot = null;
        freeSlot = inventorySlots.FirstOrDefault(i => i.isUsed == false);

        return freeSlot;
    }
}
