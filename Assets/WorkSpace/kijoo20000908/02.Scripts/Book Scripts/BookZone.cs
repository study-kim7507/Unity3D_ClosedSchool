using UnityEngine;
using System.Collections.Generic;

public class BookZone : MonoBehaviour
{
    [SerializeField] private int requiredBookCount = 4; // 필요한 책 개수
    private HashSet<Book> booksInZone = new HashSet<Book>(); // 현재 영역 안에 있는 책 목록
    private PuzzleManager puzzleManager;

    private void Start()
    {
        puzzleManager = FindObjectOfType<PuzzleManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Book")) // 책이 들어오면
        {
            Book book = other.GetComponent<Book>();
            if (book != null && !booksInZone.Contains(book))
            {
                booksInZone.Add(book);
                Debug.Log($"책 {book.GetBookName()}이 들어옴 ({booksInZone.Count}/{requiredBookCount})");

                puzzleManager.CheckPuzzleCompletion(booksInZone.Count, requiredBookCount);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Book")) // 책이 나가면
        {
            Book book = other.GetComponent<Book>();
            if (book != null && booksInZone.Contains(book))
            {
                booksInZone.Remove(book);
                Debug.Log($"책 {book.GetBookName()}이 나감 ({booksInZone.Count}/{requiredBookCount})");

                puzzleManager.CheckPuzzleCompletion(booksInZone.Count, requiredBookCount);
            }
        }
    }

    public int GetCurrentBookCount()
    {
        return booksInZone.Count;
    }
}
