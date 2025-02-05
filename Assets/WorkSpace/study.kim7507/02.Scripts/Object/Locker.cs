using UnityEngine;

public class Locker : MonoBehaviour, IInteractable
{
    private GameObject player;
    [SerializeField] Transform locker;
    [SerializeField] private Door door;

    private PlayerController playerController;
    private CharacterController playerCharacterController;

    private Vector3 backupPos;
    private Quaternion backupRot;

    [SerializeField] GameObject interactionMessage;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");

        playerController = player.GetComponent<PlayerController>();
        playerCharacterController = player.GetComponent<CharacterController>();
    }


    public void BeginFocus(GameObject withItem = null)
    {
        if (interactionMessage != null && !playerController.isHide)
            interactionMessage.SetActive(true);

    }

    public void EndFocus(GameObject withItem = null)
    {
        if (interactionMessage != null)
            interactionMessage.SetActive(false);
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

        player.transform.rotation = locker.rotation;
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
}
