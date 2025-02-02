using System;
using UnityEngine;


public class Door : MonoBehaviour, IInteractable
{
    [Serializable]
    public class AnimNames
    {
        public string OpeningAnim = "Door_open";
        public string LockedAnim = "Door_locked";
        public string ClosingAnim = "Door_close";
    }

    [Serializable]
    public class DoorSounds
    {
        public bool enabled = true;
        public AudioClip open;
        public AudioClip locked;
        public AudioClip unlock;      // Ű�� ������ ���� ��ȣ�ۿ� �� ����� Ǯ���� ����
        public AudioClip close;

        [Range(0f, 1f)]
        public float volume = 1.0f;
        [Range(0f, 0.4f)]
        public float pitchRandom = 0.2f;
    }

    public bool isOpened;              // ���� ���� ���� �ִ��� ����
    public bool isLocked;              // ���� ���� ��� �ִ��� ����

    public string where;

    public AnimNames animationNames = new AnimNames();
    public DoorSounds doorSounds = new DoorSounds();

    private Animation animation;
    private float animationStartTime;

    private AudioSource audioSource;

    [SerializeField] GameObject interactionMessage;  

    private void Start()
    {
        animation = GetComponent<Animation>();
        if (animation == null) animation = GetComponentInParent<Animation>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = GetComponentInParent<AudioSource>();
    }

    private void Update()
    {
        if (interactionMessage != null && !isLocked)
            EndFocus();
    }

    public void BeginFocus(GameObject withItem = null)
    {
        if (interactionMessage != null && isLocked)
            interactionMessage.SetActive(true);
    }


    public void EndFocus(GameObject withItem = null)
    {
        if (interactionMessage != null)
            interactionMessage.SetActive(false);
    }

    public void Interact(GameObject withItem = null)
    {
        if (!isOpened && !isLocked) OpenDoor();
        else if (!isOpened && isLocked && withItem == null) LockedDoor();
        else if (!isOpened && isLocked && withItem != null && withItem.GetComponent<Key>() != null) UnlockDoor(withItem);
        else if (isOpened) CloseDoor();
    }

    // �÷��̾ ���� ���� ���� ���� ��ȣ�ۿ��� �õ�
    public void OpenDoor()
    {
        if (doorSounds.open != null) audioSource.PlayOneShot(doorSounds.open);

        float curAnimationTime = 0.0f;
        
        if (animation.isPlaying) curAnimationTime = Time.time - animationStartTime;
        animationStartTime = Time.time;

        animation[animationNames.OpeningAnim].speed = 1.0f;
        animation[animationNames.OpeningAnim].time = curAnimationTime;
        animation.Play(animationNames.OpeningAnim);
        
        isOpened = true;
    }

    // �÷��̾ ���� ���� �ݱ� ���� ��ȣ�ۿ��� �õ�
    public void CloseDoor()
    {
        if (doorSounds.close != null) audioSource.PlayOneShot(doorSounds.close);

        float curAnimationTime = animation[animationNames.OpeningAnim].length * 0.35f;

        if (animation.isPlaying) curAnimationTime = Time.time - animationStartTime;
        animationStartTime = Time.time;

        animation[animationNames.OpeningAnim].speed = -1.0f;
        animation[animationNames.OpeningAnim].time = curAnimationTime;
        animation.Play(animationNames.OpeningAnim);
        
        isOpened = false;
    }

    private void LockedDoor()
    {
        if (doorSounds.locked != null)
            audioSource.PlayOneShot(doorSounds.locked);
        PlayerUI.instance.DisplayInteractionDescription("���� ����ִ�. ���谡 �ʿ��� �� ����.");
    }

    private void UnlockDoor(GameObject key)
    {
        if (key.GetComponent<Key>() != null)
        {
            if (key.GetComponent<Key>().where == this.where)
            {
                if (doorSounds.unlock != null)
                    audioSource.PlayOneShot(doorSounds.unlock);
                PlayerUI.instance.DisplayInteractionDescription("���� ���ȴ�.");
                isLocked = false;
                Destroy(key);
            }
        }
    }
}
