using UnityEditor.Analytics;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.AI;

public class GhostAiController : MonoBehaviour
{
    [SerializeField] Transform player; // 플레이어 위치를 추적하기 위해 필요
    [SerializeField] float detectionRange = 10f; //귀신 감지 거리
    [SerializeField] float patrolWaitTime = 2f; //순찰 포인트 도착 후 대기시간
    [SerializeField] float navMeshSearchRadius = 20f; //랜덤 포인트를 찾을 범위

   
    public Animator animator;
    private NavMeshAgent navMeshAgent;
    private bool isChasing = false; //추적 중인지 상태를 체크
    private float waitTimer = 0; // 대기 시간


    private void Start() 
    {
        animator = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetRandomPatrolPoint(); //초기 순찰 포인트 지정
    }

    private void Update() 
    {

        if(isChasing)
        {
            navMeshAgent.SetDestination(player.position); //플레이어 위치로 추격

            //플레이어 위치에 도착 했으면 순찰 상태로 전환
            if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                StopChase();
            }
        }
        else //순찰 상태일 때
        {
            if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                waitTimer += Time.deltaTime;
                if(waitTimer >= patrolWaitTime)
                {
                    waitTimer = 0f;
                    SetRandomPatrolPoint();
                }
            }
        }
        
    }

    private void OnTriggerEnter(Collider other) //플레이어 트리거 감지
    {
        if(other.CompareTag("Player"))

        {   //플레이어와 귀신 사이의 방향 벡터 계산
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            int layerMask = LayerMask.GetMask("Player", "LibraryObject"); //레이어마스크에 플레이어와 도서관 오브젝트를 저장
            if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, detectionRange, layerMask))
            {

                if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Player")) //귀신과 플레이어 사이에 장애물이 없을 때
                {
                    Debug.Log("플레이어 감지");
                    StartChase();
                }
                else
                {
                    Debug.Log("장애물 감지");
                }

            }
        }
        
    }

    private void SetRandomPatrolPoint() //순찰 포인트를 랜덤으로 지정하는 함수
    {
        Vector3 randomDirection = Random.insideUnitSphere * navMeshSearchRadius; // 랜덤 방향
        randomDirection += transform.position;
        animator.SetTrigger("Patrol");
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, navMeshSearchRadius, NavMesh.AllAreas))
        {
            navMeshAgent.SetDestination(hit.position);
        }
        else
        {
            Debug.LogWarning("유효한 NavMesh 포인트를 찾지 못했습니다.");
        }
    }

    private void StartChase() //추격 시작
    {
        isChasing = true;
        animator.ResetTrigger("Patrol");
        animator.SetTrigger("Chase");
        navMeshAgent.speed = 5;
        Debug.Log("추격 시작");
    }

    private void StopChase() //추격 종료
    {
        isChasing = false;
        navMeshAgent.speed = 0.5f;
        SetRandomPatrolPoint(); // 순찰 상태로 전환
        animator.ResetTrigger("Chase");
        animator.SetTrigger("Patrol");
        Debug.Log("추격 종료, 순찰 시작");

    }


}
