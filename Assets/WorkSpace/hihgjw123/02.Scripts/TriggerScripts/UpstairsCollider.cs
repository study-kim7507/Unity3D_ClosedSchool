using UnityEngine;

public class UpstairsCollider : MonoBehaviour
{
    
    public GameObject UpstarisGhost; //2층 귀신
    private Vector3 targetPosition; //귀신 타겟 위치    
    private bool isMoving= false; //움직임 제어변수
    private Vector3 targetCamraDirection = new Vector3(1, 0, 0); //카메라가 바라볼 방향
    private bool isCameraRotating = false; //카메라 회전 여부

    [SerializeField] Transform cameraTransform;
    [SerializeField] float speed = 0.2f; //귀신 속도
    [SerializeField] GameObject Light; //조명 애니메이터
    [SerializeField] MonoBehaviour playerScript;

    private void Start() 
    {
        targetPosition = UpstarisGhost.transform.position + new Vector3(0,0,5.2f); // z축으로 5.2만큼을 타겟 포지션으로
    }
     
    private void Update()
    {
        if(isMoving) // 플레이어가 트리거에 감지되면 귀신을 도서관으로 포지션 옮기기
        {
            UpstarisGhost.transform.position = Vector3.MoveTowards(
                UpstarisGhost.transform.position,
                targetPosition,
                speed * Time.deltaTime
            );
        }

        // 귀신이 타겟 포지션에 도착하면 귀신, 콜라이더 오브젝트 삭제
        if(Vector3.Distance(UpstarisGhost.transform.position, targetPosition) < 0.1f) 
        {
            Destroy(UpstarisGhost);
            Destroy(gameObject);
            ResumePlayer();
        }

        if (isCameraRotating) // 카메라 회전 처리
        {
            SmoothCameraRotation();
        }


    }



    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            isMoving = true;
            Animator Lightanimator = Light.GetComponent<Animator>();
            Lightanimator.SetTrigger("TurnOn");
            StopPlayer();
        }
    }

    private void StopPlayer()
    {
        if(playerScript != null)
        {
            playerScript.enabled = false; // 플레이어 스크립트 비활성화
            
        }
        isCameraRotating = true;


    }

    private void ResumePlayer()
    {
        if (playerScript != null)
        {
            playerScript.enabled = true; // 플레이어 스크립트 활성화
        }
        isCameraRotating = false;

    }

    private void SmoothCameraRotation()
    {
        // 카메라가 부드럽게 회전하도록 처리
        Quaternion targetRotation = Quaternion.LookRotation(targetCamraDirection);
        cameraTransform.rotation = Quaternion.Slerp(
            cameraTransform.rotation,
            targetRotation,
            Time.deltaTime * 2f
        );

        // 목표 각도에 거의 도달하면 회전 중지
        if (Quaternion.Angle(cameraTransform.rotation, targetRotation) < 0.1f)
        {
            cameraTransform.rotation = targetRotation;
            isCameraRotating = false; // 회전 완료
        }
    }


}
