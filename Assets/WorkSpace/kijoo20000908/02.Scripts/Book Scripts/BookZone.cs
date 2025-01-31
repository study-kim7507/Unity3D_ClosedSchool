using UnityEngine;
using System.Collections.Generic;

public class BookZone : MonoBehaviour
{
    [SerializeField] private int requiredBookCount = 4; // 필요한 책 개수
    private int currentBookCount = 0;
    private PuzzleManager puzzleManager;
    [SerializeField] private GameObject rewardPrefab; // 미션 클리어 보상 프리팹
    [SerializeField] private Transform rewardSpawnPoint; // 보상을 생성할 위치

    private void Start()
    {
        puzzleManager = FindObjectOfType<PuzzleManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Book"))
        {
            Book book = other.GetComponent<Book>();
            if (book != null)
            {
                currentBookCount++;
                Debug.Log($"책 {book.GetBookName()}이 놓여짐 ({currentBookCount}/{requiredBookCount})");

                // 책 없애기
                Destroy(other.gameObject);

                // 퍼즐 상태 체크
                puzzleManager.CheckPuzzleCompletion(currentBookCount, requiredBookCount);

                // 보상 체크
                if (currentBookCount >= requiredBookCount)
                {
                    SpawnReward();
                }
            }
        }
    }

    private void SpawnReward()
    {
        if (rewardPrefab != null && rewardSpawnPoint != null)
        {
            Instantiate(rewardPrefab, rewardSpawnPoint.position, Quaternion.identity);
            Debug.Log("?? 미션 클리어! 보상이 생성되었습니다.");
        }
    }
}
