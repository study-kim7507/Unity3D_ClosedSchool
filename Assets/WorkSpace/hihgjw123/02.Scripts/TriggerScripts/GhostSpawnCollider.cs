using Unity.VisualScripting;
using UnityEngine;

public class GhostSpawnCollider : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject ghostPrefab;
    [SerializeField] float spawnDistance = 5f; //플레이어 뒤로 떨어질 거리
    [SerializeField] float followSpeed = 3f; // 플레이어 따라오는 속도
    [SerializeField] float chaseSpeed = 10f; // 달려드는 속도
    [SerializeField] float followDistance = 3f; // 유지거리
    [SerializeField] float detectionDelay = 0.5f; //카메라 감지 후 달려들기까지 쿨 타임

    private GameObject ghost;
    private bool isChasing = false;
    private bool isSpawned = false;
    private float ghostPositionY;


    // Update is called once per frame
    void Update()
    {

        if (ghost != null)
        {
            if (isChasing)
            {
                ChasePlayer();
            }
            else
            {
                FollowPlayer();

                if (IsInView())
                {
                    // 감지된 후 일정 시간 뒤에 추격
                    Invoke(nameof(StartChasing), detectionDelay);
                }
            }
        }
    }


    private void OnTriggerEnter(Collider other) 
    {
        if(!isSpawned)
        {
            SpawnGhost();
            isSpawned = true;
        }
    }

    private void SpawnGhost() //귀신 플레이어 뒤에 생성
    {
        Vector3 spawnPosition = player.position - player.forward * spawnDistance;
        spawnPosition.y = player.position.y - 0.8f;

        ghost = Instantiate(ghostPrefab, spawnPosition, Quaternion.identity);

        ghostPositionY = spawnPosition.y;
    }

    private void FollowPlayer() //플레이어 따라다니기
    {
        
        // 플레이어와의 거리 계산
        float distance = Vector3.Distance(ghost.transform.position, player.position);

        ghost.transform.position = new Vector3(
        ghost.transform.position.x,
        ghostPositionY,
        ghost.transform.position.z
    );
        if (distance > followDistance)
        {
            Debug.Log("플레이어 따라가기");
            // 귀신이 플레이어를 따라 이동
            Vector3 direction = (player.position - ghost.transform.position).normalized;
            ghost.transform.position += direction * followSpeed * Time.deltaTime;

            ghost.transform.LookAt(player.position);
        }
    }

    private void ChasePlayer() // 플레이어한테 달려들기
    {
        Animator animator = ghost.GetComponentInChildren<Animator>();
        animator.SetTrigger("Chase");
        
        Vector3 direction = (player.position - ghost.transform.position).normalized;
        ghost.transform.position += direction * chaseSpeed * Time.deltaTime;

        

        ghost.transform.position = new Vector3(
       ghost.transform.position.x,
       ghostPositionY,
       ghost.transform.position.z
        );
    }

    bool IsInView() //귀신이 카메라 안에 들어왔는지 체크크
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(ghost.transform.position);
        return viewPos.x > 0 && viewPos.x < 1 && viewPos.y > 0 && viewPos.y < 1 && viewPos.z > 0;
    }

    void StartChasing()
    {
        isChasing = true;
    }

}
