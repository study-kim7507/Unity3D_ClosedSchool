using UnityEngine;

public class Locker : MonoBehaviour, IInteractable
{
    private GameObject player;
    [SerializeField] private Door door;

    private PlayerController playerController;
    private CharacterController playerCharacterController;

    private Vector3 backupPos;
    private Quaternion backupRot;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");

        playerController = player.GetComponent<PlayerController>();
        playerCharacterController = player.GetComponent<CharacterController>();
    }


    public void BeginFocus(GameObject withItem = null)
    {
        
    }

    public void BeginInteract(GameObject withItem = null)
    {
        
    }

    public void EndFocus(GameObject withItem = null)
    {
        
    }

    public void EndInteract(GameObject withItem = null)
    {
        
    }

    public void Interact(GameObject withItem = null)
    {
        if (!playerController.isHide) PlayerHideInLocker();
        else PlayerComeOutOfLocker();
    }

    private void PlayerHideInLocker()
    {
        backupPos = player.transform.position;
        backupRot = player.transform.rotation;

        Transform rootObjectTransform = GetRootObjectTransform();

        player.transform.rotation = rootObjectTransform.rotation;
        player.transform.position = transform.position;

        playerCharacterController.enabled = false;
        playerController.isHide = true;

        door.CloseDoor();
    }

    private void PlayerComeOutOfLocker()
    {
        door.OpenDoor();

        player.transform.rotation = backupRot;
        player.transform.position = backupPos;

        playerCharacterController.enabled = true;
        playerController.isHide = false;
    }

    private Transform GetRootObjectTransform()
    {
        Transform currentTransform = transform;
        while(currentTransform.parent != null)
            currentTransform = currentTransform.parent;
        return currentTransform;
    }
}
