using UnityEngine;

public class UpstairsCollider : MonoBehaviour
{
    
    public GameObject UpstarisGhost; //2층 귀신
    private Vector3 targetPosition; //귀신 타겟 위치    

    private bool isMoving= false;
    [SerializeField] float speed = 0.2f;

    private void Start() 
    {
        targetPosition = UpstarisGhost.transform.position + new Vector3(0,0,8f);
    }
     
    private void Update() 
    {
        if(isMoving)
        {
            UpstarisGhost.transform.position = Vector3.MoveTowards(
                UpstarisGhost.transform.position,
                targetPosition,
                speed * Time.deltaTime
            );
        }
        if(Vector3.Distance(UpstarisGhost.transform.position, targetPosition)<0.1f)
        {
            Destroy(UpstarisGhost);
            Destroy(gameObject);
        }    
    }


   

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            isMoving = true; 
        }
    }

   
}
