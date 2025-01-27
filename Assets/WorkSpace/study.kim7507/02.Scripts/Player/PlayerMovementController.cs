using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
public class PlayerMovementController : MonoBehaviour
{
    [Serializable]
    public class PlayerMovementSounds
    {
        public bool enabled = true;

        // 이동
        public AudioClip slowWalk;
        public AudioClip walk;
        public AudioClip run;
        public AudioClip breathingNormal;
        public AudioClip breathingFast;

        // 점프, 앉기
        public AudioClip jump;
        public AudioClip crouch;
    }

    // 이동 관련
    public float slowWalkSpeed = 1.5f;
    public float walkSpeed = 3.0f;
    public float runSpeed = 5.0f;
    private float moveSpeed;
    private Vector3 moveForce;
   
    // 점프 관련
    public float jumpForce;

    // 앉기 관련
    [HideInInspector] public bool isCrouching;
    private float originalHeight;
    private float originalCameraYPos;

    private CharacterController characterController;
    private CapsuleCollider capsuleCollider;
    public AudioSource movementAudioSource;


    // 사운드 관련
    public PlayerMovementSounds playerMovementSounds = new PlayerMovementSounds();

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        
        originalHeight = characterController.height;
        originalCameraYPos = Camera.main.transform.localPosition.y;
    }

    private void Update()
    {
        if (!characterController.isGrounded) moveForce.y -= 9.81f * Time.deltaTime;
        characterController.Move(moveForce * Time.deltaTime);
    }

    public void MoveTo(Vector3 direction)
    {
        // 이동 방향 = 캐릭터의 회전 값 * 방향 값
        direction = transform.rotation * new Vector3(direction.x, 0, direction.z);

        if (isCrouching) moveSpeed /= 2.0f; // 앉아 있는 경우, 이동 속도가 절반만 적용되도록

        // 이동 힘 = 이동 방향 * 속도
        moveForce = new Vector3(direction.x * moveSpeed, moveForce.y, direction.z * moveSpeed);
    }

    public void Idle() 
    { 
        moveSpeed = 0.0f;
        if (GetComponent<PlayerController>().stamina <= 0.0f) PlaySound(playerMovementSounds.breathingNormal, 1.25f, 0.0125f);
        else PlaySound(playerMovementSounds.breathingNormal, 1.0f, 0.0125f);
    }
    public void SlowWalk()
    { 
        moveSpeed = slowWalkSpeed;
        PlaySound(playerMovementSounds.slowWalk, 0.75f, 0.25f);
    }
    public void Walk() 
    {
        moveSpeed = walkSpeed;
        PlaySound(playerMovementSounds.walk, 1.0f, 0.25f);
    }

    public void Run() 
    { 
        moveSpeed = runSpeed;
        PlaySound(playerMovementSounds.run, 1.5f, 0.25f);
    }
    

    public void Jump()
    {
        // 지면에 닿아있을 때 (점프 중인 상태가 아닐 때)만 점프가 가능하도록
        if (characterController.isGrounded)
        {
            moveForce.y = isCrouching ? jumpForce / 2.0f : jumpForce; // 앉아 있는 경우, 점프력이 절반만 적용되도록
            PlayJumpOrCrouchSound(playerMovementSounds.jump, 1.0f, 1.0f);
        }
    }

    public void Crouch()
    {
        // 캐릭터 컨트롤러 조정
        characterController.height = originalHeight / 2.0f;

        // 콜라이더 조정
        capsuleCollider.height = originalHeight / 2.0f;

        // 카메라 위치 조정
        StartCoroutine(ChangeCameraPosition(new Vector3(0, characterController.center.y, 0), 0.15f));

        isCrouching = !isCrouching;
        PlayJumpOrCrouchSound(playerMovementSounds.crouch, 1.0f, 0.25f);
    }
    public void UnCrouch()
    {
        if (IsHeadClear())
        {
            // 캐릭터 컨트롤러 조정
            characterController.height = originalHeight;

            // 콜라이더 조정
            capsuleCollider.height = originalHeight;

            // 카메라 위치 조정
            StartCoroutine(ChangeCameraPosition(new Vector3(0, originalCameraYPos, 0), 0.15f));

            isCrouching = !isCrouching;
            PlayJumpOrCrouchSound(playerMovementSounds.crouch, 1.0f, 1.0f);
        }
    }

    private IEnumerator ChangeCameraPosition(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = Camera.main.transform.localPosition;
        float time = 0.0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            Camera.main.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, time / duration);
            yield return null;
        }

        Camera.main.transform.localPosition = targetPosition;
    }

    // 앉아 있을 때, 머리 위에 물체가 있는지 여부를 감지
    // 물체가 있는 경우 일어나지 못하도록
    private bool IsHeadClear()
    {
        // 캐릭터의 현재 머리 위치 계산
        Vector3 headPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z) + Vector3.up * (characterController.height / 2.0f);

        RaycastHit hit;

        // Raycast 실행
        if (Physics.Raycast(headPosition, Vector3.up, out hit, 0.5f))
        {
            // 머리 위에 물체가 있으면
            return false; // 물체가 있으므로 일어날 수 없음
        }

        return true; // 머리 위에 물체가 없으므로 일어날 수 있음
    }

    private void PlaySound(AudioClip clip, float speed, float volume)
    {
        if (movementAudioSource.clip != clip || !movementAudioSource.isPlaying)
        {
            movementAudioSource.Stop();
            movementAudioSource.volume = volume;
            movementAudioSource.pitch = speed;
            movementAudioSource.loop = true;
            movementAudioSource.clip = clip;
            movementAudioSource.Play();
        }
    }
    
    private void PlayJumpOrCrouchSound(AudioClip clip, float speed, float volume)
    {
        if (movementAudioSource.clip != clip || !movementAudioSource.isPlaying)
        {
            movementAudioSource.Stop();
            movementAudioSource.volume = volume;
            movementAudioSource.pitch = speed;
            movementAudioSource.PlayOneShot(clip);
        }
    }
}
