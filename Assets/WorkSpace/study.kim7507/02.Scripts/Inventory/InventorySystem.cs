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
