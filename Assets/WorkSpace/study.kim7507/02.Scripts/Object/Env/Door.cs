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
        public AudioClip unlocked;      // 키를 가지고 문과 상호작용 시 잠금이 풀리는 사운드
        public AudioClip closed;

        [Range(0f, 1f)]
        public float volume = 1.0f;
        [Range(0f, 0.4f)]
        public float pitchRandom = 0.2f;
    }

    public bool isOpened;              // 현재 문이 열려 있는지 여부
    public bool isLocked;              // 현재 문이 잠겨 있는지 여부
                                       
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

    // 플레이어가 닫힌 문을 열기 위한 상호작용을 시도
    private void OpenDoor()
    {
        // TODO: 문 열리는 사운드

        float curAnimationTime = 0.0f;
        
        if (animation.isPlaying) curAnimationTime = Time.time - animationStartTime;
        animationStartTime = Time.time;
        
        animation[animationNames.OpeningAnim].speed = 1.0f;
        animation[animationNames.OpeningAnim].time = curAnimationTime;
        animation.Play(animationNames.OpeningAnim);
        
        isOpened = true;
    }

    // 플레이어가 열린 문을 닫기 위한 상호작용을 시도
    private void CloseDoor()
    {
        // TODO: 문 열리는 사운드
        
        float curAnimationTime = animation[animationNames.OpeningAnim].length;

        if (animation.isPlaying) curAnimationTime = Time.time - animationStartTime;
        animationStartTime = Time.time;

        animation[animationNames.OpeningAnim].speed = -1.0f;
        animation[animationNames.OpeningAnim].time = curAnimationTime;
        animation.Play(animationNames.OpeningAnim);
        
        isOpened = false;
    }

    // 잠겨있는 경우 손에 키가 있어야 함
    private void LockedDoor(GameObject key)
    {
        // TODO: 열쇠 로직
        // if (withItem != null && withItem.GetComponent<Key>() != null)

        // TODO: 문 열리는 사운드
        animation.Play(animationNames.LockedAnim);
    }
}
