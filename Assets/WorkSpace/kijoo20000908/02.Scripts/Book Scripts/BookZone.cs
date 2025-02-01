using UnityEngine;
using System.Collections.Generic;

public class BookZone : MonoBehaviour
{
    [SerializeField] private int requiredBookCount = 4; // �ʿ��� å ����
    private int currentBookCount = 0;
    private List<Book> placedBooks = new List<Book>(); // ��ġ�� å ���
    [SerializeField] private Transform[] bookSlots; // å�� ���� ���� (4��)
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

                    Debug.Log($" å {book.GetBookName()}�� ��ġ�� ({currentBookCount}/{requiredBookCount})");

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
            // å�� ���� ��ġ�� �̵�
            book.transform.position = bookSlots[slotIndex].position;
            book.transform.rotation = bookSlots[slotIndex].rotation;

            // å�� Rigidbody ���� ȿ�� ���� (������)
            Rigidbody rb = book.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }

            // å�� �������� �ʵ��� �浹 �ڽ� ��Ȱ��ȭ
            Collider bookCollider = book.GetComponent<Collider>();
            if (bookCollider != null)
            {
                bookCollider.enabled = false;
            }

            placedBooks.Add(book);
        }
    }
}
