using UnityEngine;
using System.Collections.Generic;

public class BookZone : MonoBehaviour
{
    [SerializeField] private int requiredBookCount = 4; // �ʿ��� å ����
    private HashSet<Book> booksInZone = new HashSet<Book>(); // ���� ���� �ȿ� �ִ� å ���
    private PuzzleManager puzzleManager;

    private void Start()
    {
        puzzleManager = FindObjectOfType<PuzzleManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Book")) // å�� ������
        {
            Book book = other.GetComponent<Book>();
            if (book != null && !booksInZone.Contains(book))
            {
                booksInZone.Add(book);
                Debug.Log($"å {book.GetBookName()}�� ���� ({booksInZone.Count}/{requiredBookCount})");

                puzzleManager.CheckPuzzleCompletion(booksInZone.Count, requiredBookCount);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Book")) // å�� ������
        {
            Book book = other.GetComponent<Book>();
            if (book != null && booksInZone.Contains(book))
            {
                booksInZone.Remove(book);
                Debug.Log($"å {book.GetBookName()}�� ���� ({booksInZone.Count}/{requiredBookCount})");

                puzzleManager.CheckPuzzleCompletion(booksInZone.Count, requiredBookCount);
            }
        }
    }

    public int GetCurrentBookCount()
    {
        return booksInZone.Count;
    }
}
