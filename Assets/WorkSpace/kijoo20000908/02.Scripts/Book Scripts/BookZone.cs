using UnityEngine;
using System.Collections.Generic;

public class BookZone : MonoBehaviour
{
    [SerializeField] private int requiredBookCount = 4; // �ʿ��� å ����
    private int currentBookCount = 0;
    [SerializeField] private GameObject rewardPrefab; // �̼� Ŭ���� ���� ������
    [SerializeField] private Transform rewardSpawnPoint; // ���� ���� ��ġ
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
                Debug.Log($"å {book.GetBookName()}�� ������ ({currentBookCount}/{requiredBookCount})");

                Destroy(other.gameObject); // å ����

                puzzleManager.CheckPuzzleCompletion(currentBookCount, requiredBookCount);

                if (currentBookCount >= requiredBookCount)
                {
                    SpawnReward();
                    RemoveBookZone(); // ���� �Ϸ� �� BookZone ����
                }
            }
        }
    }

    private void SpawnReward()
    {
        if (rewardPrefab != null && rewardSpawnPoint != null)
        {
            Instantiate(rewardPrefab, rewardSpawnPoint.position, Quaternion.identity);
            Debug.Log("�̼� Ŭ����! ������ �����Ǿ����ϴ�.");
        }
    }

    private void RemoveBookZone()
    {
        Debug.Log("BookZone ����!");
        Destroy(gameObject); // BookZone ����
    }
}
