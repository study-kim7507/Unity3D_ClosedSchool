using UnityEngine;
using System.Collections.Generic;

public class BookZone : MonoBehaviour
{
    [SerializeField] private int requiredBookCount = 4; // 필요한 책 개수
    private int currentBookCount = 0;
    [SerializeField] private GameObject rewardPrefab; // 미션 클리어 보상 프리팹
    [SerializeField] private Transform rewardSpawnPoint; // 보상 생성 위치
    private PuzzleManager puzzleManager;

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

                Destroy(other.gameObject); // 책 제거

                puzzleManager.CheckPuzzleCompletion(currentBookCount, requiredBookCount);

                if (currentBookCount >= requiredBookCount)
                {
                    SpawnReward();
                    RemoveBookZone(); // 퍼즐 완료 시 BookZone 제거
                }
            }
        }
    }

    private void SpawnReward()
    {
        if (rewardPrefab != null && rewardSpawnPoint != null)
        {
            Instantiate(rewardPrefab, rewardSpawnPoint.position, Quaternion.identity);
            Debug.Log("미션 클리어! 보상이 생성되었습니다.");
        }
    }

    private void RemoveBookZone()
    {
        Debug.Log("BookZone 제거!");
        Destroy(gameObject); // BookZone 제거
    }
}
