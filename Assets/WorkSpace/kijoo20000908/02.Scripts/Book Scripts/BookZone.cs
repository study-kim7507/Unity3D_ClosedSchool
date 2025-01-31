using UnityEngine;
using System.Collections.Generic;

public class BookZone : MonoBehaviour
{
    [SerializeField] private int requiredBookCount = 4; // �ʿ��� å ����
    private int currentBookCount = 0;
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

                // å ���ֱ� (���� ������Ʈ ����)
                Destroy(other.gameObject);

                // ���� ���� üũ
                puzzleManager.CheckPuzzleCompletion(currentBookCount, requiredBookCount);
            }
        }
    }
}
