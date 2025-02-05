using UnityEngine;
using UnityEngine.AI;

public class GhostAiController : MonoBehaviour
{
    [SerializeField] Transform player; // ?�레?�어 ?�치�?추적?�기 ?�해 ?�요
    [SerializeField] float detectionRange = 10f; // 귀??감�? 거리
    [SerializeField] float fieldOfView = 100f; // ?�야�?(100??
    [SerializeField] float patrolWaitTime = 2f; // ?�찰 ?�인???�착 ???�기시�?
    [SerializeField] float navMeshSearchRadius = 20f; // ?�덤 ?�인?��? 찾을 범위
    [SerializeField] HeartbeatEffect heartbeatEffect;

    public Animator animator;
    private NavMeshAgent navMeshAgent;
    private bool isChasing = false; // 추적 중인지 ?�태�?체크
    private float waitTimer = 0; // ?��??�간

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetRandomPatrolPoint(); // 초기 ?�찰 ?�인??지??
    }

    private void Update()
    {
        if (!isChasing)
        {
            CheckForPlayer(); // ?�레?�어 감�?
        }

        if (isChasing)
        {
            navMeshAgent.SetDestination(player.position); // ?�레?�어 ?�치�?추격

            // ?�레?�어 ?�치???�착?�으�??�찰 ?�태�??�환
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                StopChase();
            }
        }
        else // ?�찰 ?�태????
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                waitTimer += Time.deltaTime;
                if (waitTimer >= patrolWaitTime)
                {
                    waitTimer = 0f;
                    SetRandomPatrolPoint();
                }
            }
        }
    }

    private void CheckForPlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 감�? 거리 ?�에 ?�는지 ?�인
        if (distanceToPlayer > detectionRange)
        {
            return; // 감�? 거리 밖이�?리턴
        }

        // ?�야�??�에 ?�는지 ?�인
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        if (angleToPlayer > fieldOfView / 2f)
        {
            return; // ?�야�?밖이�?리턴
        }

        // Raycast�??�애�??��? ?�인
        int layerMask = LayerMask.GetMask("Player", "LibraryObject"); // ?�레?�어?� ?�애�??�이??
        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, detectionRange, layerMask))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player")) // ?�애�??�이 ?�레?�어 보임
            {
                Debug.Log("?�레?�어 감�?");
                StartChase();
                heartbeatEffect.isEffectActive = true;
                heartbeatEffect.heartbeatSound.Play();
            }
            else
            {
                Debug.Log("?�애�?감�?");
            }
        }
    }

    private void SetRandomPatrolPoint() // ?�찰 ?�인?��? ?�덤?�로 지?�하???�수
    {
        Vector3 randomDirection = Random.insideUnitSphere * navMeshSearchRadius; // ?�덤 방향
        randomDirection += transform.position;
        animator.SetTrigger("Patrol");
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, navMeshSearchRadius, NavMesh.AllAreas))
        {
            navMeshAgent.SetDestination(hit.position);
        }
        else
        {
            Debug.LogWarning("?�효??NavMesh ?�인?��? 찾�? 못했?�니??");
        }
    }

    private void StartChase() // 추격 ?�작
    {
        isChasing = true;
        animator.ResetTrigger("Patrol");
        animator.SetTrigger("Chase");
        navMeshAgent.speed = 2.5f;
        Debug.Log("추격 ?�작");
    }

    public void StopChase() // 추격 종료
    {
        isChasing = false;
        navMeshAgent.speed = 0.5f;
        SetRandomPatrolPoint(); // ?�찰 ?�태�??�환
        animator.ResetTrigger("Chase");
        animator.SetTrigger("Patrol");
        Debug.Log("추격 종료, ?�찰 ?�작");
    }
}
