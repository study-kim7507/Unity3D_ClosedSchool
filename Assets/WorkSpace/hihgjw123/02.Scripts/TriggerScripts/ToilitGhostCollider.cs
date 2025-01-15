using UnityEngine;

public class ToilletGhostCollider : MonoBehaviour
{
    public GameObject ToilletGhost; // 화장실 귀신
    Animator toilletghostAnimator; 

    private void Start() 
    {
        toilletghostAnimator = ToilletGhost.GetComponent<Animator>(); //화장실 귀신 애니메이터
    }

    private void Update() 
    {
        if(toilletghostAnimator)
        {
            AnimatorStateInfo stateInfo = toilletghostAnimator.GetCurrentAnimatorStateInfo(0); // 애니메이션 상태 가져오기
            if(stateInfo.IsName("ClassicFemaleGhost_Jumpscare_Wall_LSide_Peekaboo01") && stateInfo.normalizedTime >= 1.0f) //까꿍 애니메이션이 끝나면
            {
                Destroy(ToilletGhost); //귀신 오브젝트 삭제
                Destroy(gameObject); // 콜라이더 오브젝트 삭제
            }
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player")) //플레이어가 화장실에 배치된 콜라이더에 감지되면
        {
            toilletghostAnimator.SetTrigger("Go"); //빼꼼 애니메이션
        }
           
    }

   
}
