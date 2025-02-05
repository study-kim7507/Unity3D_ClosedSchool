using UnityEngine;
using UnityEngine.AI;

public class GhostAiController : MonoBehaviour
{
    [SerializeField] Transform player; // ?Œë ˆ?´ì–´ ?„ì¹˜ë¥?ì¶”ì ?˜ê¸° ?„í•´ ?„ìš”
    [SerializeField] float detectionRange = 10f; // ê·€??ê°ì? ê±°ë¦¬
    [SerializeField] float fieldOfView = 100f; // ?œì•¼ê°?(100??
    [SerializeField] float patrolWaitTime = 2f; // ?œì°° ?¬ì¸???„ì°© ???€ê¸°ì‹œê°?
    [SerializeField] float navMeshSearchRadius = 20f; // ?œë¤ ?¬ì¸?¸ë? ì°¾ì„ ë²”ìœ„
    [SerializeField] HeartbeatEffect heartbeatEffect;

    public Animator animator;
    private NavMeshAgent navMeshAgent;
    private bool isChasing = false; // ì¶”ì  ì¤‘ì¸ì§€ ?íƒœë¥?ì²´í¬
    private float waitTimer = 0; // ?€ê¸??œê°„

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetRandomPatrolPoint(); // ì´ˆê¸° ?œì°° ?¬ì¸??ì§€??
    }

    private void Update()
    {
        if (!isChasing)
        {
            CheckForPlayer(); // ?Œë ˆ?´ì–´ ê°ì?
        }

        if (isChasing)
        {
            navMeshAgent.SetDestination(player.position); // ?Œë ˆ?´ì–´ ?„ì¹˜ë¡?ì¶”ê²©

            // ?Œë ˆ?´ì–´ ?„ì¹˜???„ì°©?ˆìœ¼ë©??œì°° ?íƒœë¡??„í™˜
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                StopChase();
            }
        }
        else // ?œì°° ?íƒœ????
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

        // ê°ì? ê±°ë¦¬ ?ˆì— ?ˆëŠ”ì§€ ?•ì¸
        if (distanceToPlayer > detectionRange)
        {
            return; // ê°ì? ê±°ë¦¬ ë°–ì´ë©?ë¦¬í„´
        }

        // ?œì•¼ê°??ˆì— ?ˆëŠ”ì§€ ?•ì¸
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        if (angleToPlayer > fieldOfView / 2f)
        {
            return; // ?œì•¼ê°?ë°–ì´ë©?ë¦¬í„´
        }

        // Raycastë¡??¥ì• ë¬??¬ë? ?•ì¸
        int layerMask = LayerMask.GetMask("Player", "LibraryObject"); // ?Œë ˆ?´ì–´?€ ?¥ì• ë¬??ˆì´??
        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, detectionRange, layerMask))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player")) // ?¥ì• ë¬??†ì´ ?Œë ˆ?´ì–´ ë³´ì„
            {
                Debug.Log("?Œë ˆ?´ì–´ ê°ì?");
                StartChase();
                heartbeatEffect.isEffectActive = true;
                heartbeatEffect.heartbeatSound.Play();
            }
            else
            {
                Debug.Log("?¥ì• ë¬?ê°ì?");
            }
        }
    }

    private void SetRandomPatrolPoint() // ?œì°° ?¬ì¸?¸ë? ?œë¤?¼ë¡œ ì§€?•í•˜???¨ìˆ˜
    {
        Vector3 randomDirection = Random.insideUnitSphere * navMeshSearchRadius; // ?œë¤ ë°©í–¥
        randomDirection += transform.position;
        animator.SetTrigger("Patrol");
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, navMeshSearchRadius, NavMesh.AllAreas))
        {
            navMeshAgent.SetDestination(hit.position);
        }
        else
        {
            Debug.LogWarning("? íš¨??NavMesh ?¬ì¸?¸ë? ì°¾ì? ëª»í–ˆ?µë‹ˆ??");
        }
    }

    private void StartChase() // ì¶”ê²© ?œì‘
    {
        isChasing = true;
        animator.ResetTrigger("Patrol");
        animator.SetTrigger("Chase");
        navMeshAgent.speed = 2.5f;
        Debug.Log("ì¶”ê²© ?œì‘");
    }

    public void StopChase() // ì¶”ê²© ì¢…ë£Œ
    {
        isChasing = false;
        navMeshAgent.speed = 0.5f;
        SetRandomPatrolPoint(); // ?œì°° ?íƒœë¡??„í™˜
        animator.ResetTrigger("Chase");
        animator.SetTrigger("Patrol");
        Debug.Log("ì¶”ê²© ì¢…ë£Œ, ?œì°° ?œì‘");
    }
}
