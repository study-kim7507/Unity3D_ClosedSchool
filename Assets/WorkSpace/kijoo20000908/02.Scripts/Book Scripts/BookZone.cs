using UnityEngine;
using System.Collections.Generic;

public class BookZone : MonoBehaviour
{
    [SerializeField] private int requiredBookCount = 4; // �ʿ��� å ����
    private int currentBookCount = 0;
    private PuzzleManager puzzleManager;
    [SerializeField] private GameObject rewardPrefab; // �̼� Ŭ���� ���� ������
    [SerializeField] private Transform rewardSpawnPoint; // ������ ������ ��ġ

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
                Debug.Log($"å {book.GetBookName()}�� ������ ({currentBookCount}/{requiredBookCount})");

                // å ���ֱ�
                Destroy(other.gameObject);

                // ���� ���� üũ
                puzzleManager.CheckPuzzleCompletion(currentBookCount, requiredBookCount);

                // ���� üũ
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
            Debug.Log("?? �̼� Ŭ����! ������ �����Ǿ����ϴ�.");
        }
    }
}
