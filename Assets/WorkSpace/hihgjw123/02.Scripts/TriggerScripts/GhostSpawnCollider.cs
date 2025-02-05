using UnityEngine;

public class GhostSpawnCollider : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject ghostPrefab;
    [SerializeField] float spawnDistance = 5f; // 플레이어 뒤로 떨어질 거리
    [SerializeField] float followSpeed = 3f; // 플레이어 따라오는 속도
    [SerializeField] float followDistance = 3f; // 유지 거리
    [SerializeField] Animator ghostAnimator; // 귀신 애니메이터

    public GameObject ghost;
    public bool isSpawned = false;
    private bool isVisible = false;
    private float ghostPositionY;
    private int currentPoseIndex = -1; // 현재 포즈 인덱스

    void Update()
    {
        if (ghost != null)
        {
            if (IsInView()) // 플레이어가 귀신을 보고 있을 때
            {
                if (!isVisible)
                {
                    isVisible = true;
                    ghostAnimator.speed = 0; // 애니메이션 멈춤 (현재 포즈 유지)
                }
            }
            else // 플레이어가 귀신을 안 볼 때
            {
                if (isVisible)
                {
                    isVisible = false;
                    ChangePose(); // 새로운 포즈 변경
                }
                FollowPlayer();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isSpawned)
        {
            SpawnGhost();
            isSpawned = true;
        }
    }

    private void SpawnGhost()
    {
        Vector3 spawnPosition = player.position - player.forward * spawnDistance;
        spawnPosition.y = player.position.y;

        ghost = Instantiate(ghostPrefab, spawnPosition, Quaternion.identity);
        ghostPositionY = spawnPosition.y;

        ghostAnimator = ghost.GetComponentInChildren<Animator>(); // 귀신의 애니메이터 찾기
    }

    private void FollowPlayer()
    {
        float distance = Vector3.Distance(ghost.transform.position, player.position);

        if (distance > followDistance)
        {
            Vector3 direction = (player.position - ghost.transform.position).normalized;
            ghost.transform.position += direction * followSpeed * Time.deltaTime;

            // 플레이어를 바라보게 설정
            ghost.transform.LookAt(player.position);
        }

        // Y 위치 고정
        ghost.transform.position = new Vector3(
            ghost.transform.position.x,
            ghostPositionY,
            ghost.transform.position.z
        );
    }

    private bool IsInView()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(ghost.transform.position);
        return viewPos.x > 0 && viewPos.x < 1 && viewPos.y > 0 && viewPos.y < 1 && viewPos.z > 0;
    }

    private void ChangePose()
    {
        int newPoseIndex;

        // 이전과 다른 포즈 선택
        do
        {
            newPoseIndex = Random.Range(0, 12);
        } while (newPoseIndex == currentPoseIndex);

        currentPoseIndex = newPoseIndex;
        ghostAnimator.speed = 1; // 애니메이션 재생 속도 정상화
        ghostAnimator.Play("Pose" + currentPoseIndex); // 새 포즈 적용
    }
}
