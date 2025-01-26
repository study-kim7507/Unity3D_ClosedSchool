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
        public AudioClip unlocked;      // Ű�� ������ ���� ��ȣ�ۿ� �� ����� Ǯ���� ����
        public AudioClip closed;

        [Range(0f, 1f)]
        public float volume = 1.0f;
        [Range(0f, 0.4f)]
        public float pitchRandom = 0.2f;
    }

    public bool isOpened;              // ���� ���� ���� �ִ��� ����
    public bool isLocked;              // ���� ���� ��� �ִ��� ����
                                       
    public AnimNames animationNames = new AnimNames();
    public DoorSounds doorSounds = new DoorSounds();

    private Animation animation;
    private float animationStartTime;

    private void Start()
    {
        animation = GetComponent<Animation>();
        if (animation == null) animation = GetComponentInParent<Animation>();
    }


    public void BeginFocus(GameObject withItem = null)
    {
        throw new System.NotImplementedException();
    }

    public void BeginInteract(GameObject withItem = null)
    {
        throw new System.NotImplementedException();
    }

    public void EndFocus(GameObject withItem = null)
    {
        throw new System.NotImplementedException();
    }

    public void EndInteract(GameObject withItem = null)
    {
        throw new System.NotImplementedException();
    }

    public void Interact(GameObject withItem = null)
    {
        if (!isOpened && !isLocked) OpenDoor();
        else if (!isOpened && isLocked) LockedDoor(withItem);
        else if (isOpened) CloseDoor();
    }

    // �÷��̾ ���� ���� ���� ���� ��ȣ�ۿ��� �õ�
    private void OpenDoor()
    {
        // TODO: �� ������ ����

        float curAnimationTime = 0.0f;
        
        if (animation.isPlaying) curAnimationTime = Time.time - animationStartTime;
        animationStartTime = Time.time;
        
        animation[animationNames.OpeningAnim].speed = 1.0f;
        animation[animationNames.OpeningAnim].time = curAnimationTime;
        animation.Play(animationNames.OpeningAnim);
        
        isOpened = true;
    }

    // �÷��̾ ���� ���� �ݱ� ���� ��ȣ�ۿ��� �õ�
    private void CloseDoor()
    {
        // TODO: �� ������ ����
        
        float curAnimationTime = animation[animationNames.OpeningAnim].length;

        if (animation.isPlaying) curAnimationTime = Time.time - animationStartTime;
        animationStartTime = Time.time;

        animation[animationNames.OpeningAnim].speed = -1.0f;
        animation[animationNames.OpeningAnim].time = curAnimationTime;
        animation.Play(animationNames.OpeningAnim);
        
        isOpened = false;
    }

    // ����ִ� ��� �տ� Ű�� �־�� ��
    private void LockedDoor(GameObject key)
    {
        // TODO: ���� ����
        // if (withItem != null && withItem.GetComponent<Key>() != null)

        // TODO: �� ������ ����
        animation.Play(animationNames.LockedAnim);
    }
}
