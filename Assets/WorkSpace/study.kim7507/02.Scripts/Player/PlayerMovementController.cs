using System.Collections;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
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

        // 캐릭터의 현재 머리 위치 계산
        Vector3 headPosition = new Vector3(transform.position.x, 0, transform.position.z) + Vector3.up * characterController.height;

        // RaycastHit hit;

        Debug.DrawRay(headPosition, Vector3.up * 0.1f, Color.red);
    }

    public void MoveTo(Vector3 direction)
    {
        // 이동 방향 = 캐릭터의 회전 값 * 방향 값
        direction = transform.rotation * new Vector3(direction.x, 0, direction.z);

        if (isCrouching) moveSpeed /= 2.0f; // 앉아 있는 경우, 이동 속도가 절반만 적용되도록

        // 이동 힘 = 이동 방향 * 속도
        moveForce = new Vector3(direction.x * moveSpeed, moveForce.y, direction.z * moveSpeed);
    }

    public void Idle() { moveSpeed = 0.0f; }
    public void SlowWalk() { moveSpeed = slowWalkSpeed; }
    public void Walk() { moveSpeed = walkSpeed; }
    public void Run() { moveSpeed = runSpeed; }
    

    public void Jump()
    {
        // 지면에 닿아있을 때 (점프 중인 상태가 아닐 때)만 점프가 가능하도록
        if (characterController.isGrounded)
        {
            moveForce.y = isCrouching ? jumpForce / 2.0f : jumpForce; // 앉아 있는 경우, 점프력이 절반만 적용되도록
        }
    }

    public void Crouch()
    {
        // 캐릭터 컨트롤러 조정
        characterController.height = originalHeight / 2.0f;
        characterController.center = new Vector3(characterController.center.x, -(characterController.height / 2.0f), characterController.center.z);

        // 콜라이더 조정
        capsuleCollider.height = originalHeight / 2.0f;
        capsuleCollider.center = new Vector3(capsuleCollider.center.x, -(capsuleCollider.height / 2.0f), capsuleCollider.center.z);

        // 카메라 위치 조정
        StartCoroutine(ChangeCameraPosition(new Vector3(0, characterController.center.y, 0), 0.15f));

        isCrouching = !isCrouching;
    }
    public void UnCrouch()
    {
        if (IsHeadClear())
        {
            // 캐릭터 컨트롤러 조정
            characterController.height = originalHeight;
            characterController.center = new Vector3(characterController.center.x, 0, characterController.center.z);

            // 콜라이더 조정
            capsuleCollider.height = originalHeight;
            capsuleCollider.center = new Vector3(capsuleCollider.center.x, 0, capsuleCollider.center.z);

            // 카메라 위치 조정
            StartCoroutine(ChangeCameraPosition(new Vector3(0, originalCameraYPos, 0), 0.15f));

            isCrouching = !isCrouching;
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

    // 앉아 있을 때, 머리위에 물체가 있는지 여부를 감지
    // 물체가 있는 경우 일어나지 못하도록
    private bool IsHeadClear()
    {
        // 캐릭터의 현재 머리 위치 계산
        Vector3 headPosition = new Vector3(transform.position.x, 0, transform.position.z) + Vector3.up * characterController.height;

        RaycastHit hit;

        // Raycast 실행
        if (Physics.Raycast(headPosition, Vector3.up, out hit, 0.5f))
        {
            // 머리 위에 물체가 있으면
            return false; // 물체가 있으므로 일어날 수 없음
        }

        return true; // 머리 위에 물체가 없으므로 일어날 수 있음
    }
}
