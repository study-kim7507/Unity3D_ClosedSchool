using UnityEngine;
using UnityEngine.AI;

public class GhostAiController : MonoBehaviour
{
    [SerializeField] Transform player; // 플레이어 위치를 추적하기 위해 필요
    [SerializeField] float viewAngle = 60f; // 귀신 시야각
    [SerializeField] float detectionRange = 10f; //귀신 감지 거리



    private void Update() 
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        Debug.DrawRay(transform.position, directionToPlayer * detectionRange, Color.red);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))

        {   //플레이어와 귀신 사이의 방향 벡터 계산
            Vector3 directionToPlayer = (player.position - transform.position).normalized; 

            //귀신의 정면과 플레이어 방향 벡터 사이의 각도 계산
            float angle = Vector3.Angle(transform.forward, directionToPlayer);
            if(angle < viewAngle / 2)
            {
                //귀신과 플레이어 사이에 장애물이 있는지 체크
                if(Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, detectionRange))
                {
                    if(hit.collider.CompareTag("Player"))
                    {
                        Debug.Log("플레이어 감지");
                    }
                }
            }
        }
    }
}
