using UnityEngine;
using System.Collections.Generic;

public class BookZone : MonoBehaviour
{
    [SerializeField] private int requiredBookCount = 4; // 필요한 책 개수
    private int currentBookCount = 0;
    private List<Book> placedBooks = new List<Book>(); // 배치된 책 목록
    [SerializeField] private Transform[] bookSlots; // 책을 놓을 슬롯 (4개)
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
            if (book != null && !placedBooks.Contains(book))
            {
                if (currentBookCount < bookSlots.Length)
                {
                    PlaceBookInSlot(book);
                    currentBookCount++;

                    Debug.Log($" 책 {book.GetBookName()}이 배치됨 ({currentBookCount}/{requiredBookCount})");

                    puzzleManager.CheckPuzzleCompletion(currentBookCount, requiredBookCount);
                }
            }
        }
    }

    private void PlaceBookInSlot(Book book)
    {
        int slotIndex = placedBooks.Count;
        if (slotIndex < bookSlots.Length)
        {
            // 책을 슬롯 위치로 이동
            book.transform.position = bookSlots[slotIndex].position;
            book.transform.rotation = bookSlots[slotIndex].rotation;

            // 책의 Rigidbody 물리 효과 제거 (고정됨)
            Rigidbody rb = book.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }

            // 책이 움직이지 않도록 충돌 박스 비활성화
            Collider bookCollider = book.GetComponent<Collider>();
            if (bookCollider != null)
            {
                bookCollider.enabled = false;
            }

            placedBooks.Add(book);
        }
    }
}
