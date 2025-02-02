using System.Linq;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public GameObject inventoryPanel;

    [SerializeField] Transform inventorySlotHolder;
    private InventorySlot[] inventorySlots;
   
    public PlayerController ownerPlayer;
    private AudioSource audioSource;
    void Start()
    {
        inventorySlots = inventorySlotHolder.GetComponentsInChildren<InventorySlot>();
        inventoryPanel.SetActive(ownerPlayer.isOpenInventory);    
        audioSource = GetComponent<AudioSource>();
    }

    public void ToggleInventory()
    {
        audioSource.PlayOneShot(audioSource.clip);
        inventoryPanel.SetActive(ownerPlayer.isOpenInventory);
    }

    public void AddToInventory(GameObject item)
    {
        if (!HasEmptySlot())
        {
            ownerPlayer.playerUI.DisplayInteractionDescription("인벤토리가 가득 찼습니다. " + item.GetComponent<Pickable>().itemName + "을 보관할 수 없습니다.");

            Vector3 dropPosition = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;

            item.transform.position = dropPosition;
            item.transform.rotation = Random.rotation;

            return;
        }
        else
        {
            ownerPlayer.playerUI.DisplayInteractionDescription(item.GetComponent<Pickable>().itemName + "을 획득하였습니다. 인벤토리에 보관됩니다.");
        }

        InventorySlot slot = ReturnFreeSlot();
       
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
