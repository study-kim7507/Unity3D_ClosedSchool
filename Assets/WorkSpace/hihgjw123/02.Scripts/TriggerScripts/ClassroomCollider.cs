using UnityEngine;

public class ClassroomCollider : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audioSource;





    private void Update()
    {
        if (animator)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0); // 애니메이션 상태 가져오기
            if (stateInfo.IsName("ClassRoomCollider") && stateInfo.normalizedTime >= 1f)
            {
                Invoke("DestroyCollider",2f); // 콜라이더 오브젝트 삭제
            }
        }
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            animator.SetTrigger("On");
            audioSource.Play();
        }

    }

    private void DestroyCollider()
    {
        Destroy(gameObject);
    }
}
