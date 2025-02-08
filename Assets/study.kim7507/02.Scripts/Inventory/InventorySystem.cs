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
            ownerPlayer.playerUI.DisplayInteractionDescription("�κ��丮�� ���� á���ϴ�. " + item.GetComponent<Pickable>().itemName + "�� ������ �� �����ϴ�.");

            Vector3 dropPosition = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;

            item.transform.position = dropPosition;
            item.transform.rotation = Random.rotation;

            return;
        }
        else
        {
            ownerPlayer.playerUI.DisplayInteractionDescription(item.GetComponent<Pickable>().itemName + "�� ȹ���Ͽ����ϴ�. �κ��丮�� �����˴ϴ�.");
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
