using UnityEngine;
using UnityEngine.AI;

public class GhostAiController : MonoBehaviour
{
    [SerializeField] Transform player; // 플레이어 위치를 추적하기 위해 필요
    [SerializeField] float detectionRange = 10f; // 귀신 감지 거리
    [SerializeField] float fieldOfView = 100f; // 시야각 (100도)
    [SerializeField] float patrolWaitTime = 2f; // 순찰 포인트 도착 후 대기시간
    [SerializeField] float navMeshSearchRadius = 20f; // 랜덤 포인트를 찾을 범위
    [SerializeField] HeartbeatEffect heartbeatEffect;

    public Animator animator;
    private NavMeshAgent navMeshAgent;
    private bool isChasing = false; // 추적 중인지 상태를 체크
    private float waitTimer = 0; // 대기 시간

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetRandomPatrolPoint(); // 초기 순찰 포인트 지정
    }

    private void Update()
    {
        if (!isChasing)
        {
            CheckForPlayer(); // 플레이어 감지
        }

        if (isChasing)
        {
            navMeshAgent.SetDestination(player.position); // 플레이어 위치로 추격

            // 플레이어 위치에 도착했으면 순찰 상태로 전환
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                StopChase();
            }
        }
        else // 순찰 상태일 때
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

        // 감지 거리 안에 있는지 확인
        if (distanceToPlayer > detectionRange)
        {
            return; // 감지 거리 밖이면 리턴
        }

        // 시야각 안에 있는지 확인
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        if (angleToPlayer > fieldOfView / 2f)
        {
            return; // 시야각 밖이면 리턴
        }

        // Raycast로 장애물 여부 확인
        int layerMask = LayerMask.GetMask("Player", "LibraryObject"); // 플레이어와 장애물 레이어
        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, detectionRange, layerMask))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player")) // 장애물 없이 플레이어 보임
            {
                Debug.Log("플레이어 감지");
                StartChase();
                heartbeatEffect.isEffectActive = true;
                heartbeatEffect.heartbeatSound.Play();
            }
            else
            {
                Debug.Log("장애물 감지");
            }
        }
    }

    private void SetRandomPatrolPoint() // 순찰 포인트를 랜덤으로 지정하는 함수
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

    private void StartChase() // 추격 시작
    {
        isChasing = true;
        animator.ResetTrigger("Patrol");
        animator.SetTrigger("Chase");
        navMeshAgent.speed = 4;
        Debug.Log("추격 시작");
    }

    public void StopChase() // 추격 종료
    {
        isChasing = false;
        navMeshAgent.speed = 0.5f;
        SetRandomPatrolPoint(); // 순찰 상태로 전환
        animator.ResetTrigger("Chase");
        animator.SetTrigger("Patrol");
        Debug.Log("추격 종료, 순찰 시작");
    }
}
